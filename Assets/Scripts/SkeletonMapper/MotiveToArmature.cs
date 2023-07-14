using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMapper
{
    public class MotiveToArmature : Basis
    {
        public Avatar destAvatar;
        
        /// Used in LateUpdate when retrieving and retargeting source pose. Cached and reused for efficiency
        private HumanPose _humanPose;

        // Applies a human pose to an avatar created from Motive XML and CSV GameObject transform hierarchy
        private Avatar _generatedAvatar; // Generated from XML file structure
        private HumanPoseHandler _sourcePoseHandler;

        // The destination of mapping the motive avatar to another Unity model
        private HumanPoseHandler _destPoseHandler;

        // Start is called before the first frame update
        private void Start()
        {
            // First, parse the XML file to get the Motive Skeleton XML structure into Unity ...
            ParseXMLToUnityHierarchy();
            CreatePoseHandlers();
        }

        private void LateUpdate()
        {
            // other managers move the generated skeleton. This late update method maps the positions and rotations
            // of the generated skeleton to the destination avatar.
            if (_sourcePoseHandler != null && _destPoseHandler != null)
            {
                // Interpret the streamed pose into Mecanim muscle space representation.
                _sourcePoseHandler.GetHumanPose(ref _humanPose);

                // Re-target that muscle space pose to the destination avatar.
                _destPoseHandler.SetHumanPose(ref _humanPose);
            }
        }

        public void CreatePoseHandlers()
        {
            // We begin setting up the mapping between Mecanim human anatomy in Unity and the
            // Motive skeleton representation. HumanTrait, SkeletonBone and HumanBone are Unity classes.
            var humanBones = new List<HumanBone>(BoneMap.Count);
            // Set up the T-pose and game object name mappings.
            var skeletonBones = new List<SkeletonBone>(BoneMap.Count + 1);

            // Special case: Create the skeleton root bone
            var rootBone = new SkeletonBone();
            rootBone.name = RootObj.name;
            rootBone.position = Vector3.zero;
            rootBone.rotation = Quaternion.identity;
            rootBone.scale = Vector3.one;
            skeletonBones.Add(rootBone);

            // now add human bones (Mecanim) and skeleton bones (GameObjects) to their lists
            foreach (var (k, v) in BoneMap)
            {
                // Double-check if mecanim bone name is legit
                if (Array.Find(HumanTrait.BoneName, s => s == k) == "")
                    Debug.Log(k + " is an invalid Mecanim name!");

                // create a Unity HumanBone object
                var humanBone = new HumanBone();
                humanBone.humanName = k;
                humanBone.boneName = v.name;
                humanBone.limit.useDefaultValues = true;
                humanBones.Add(humanBone);

                // create a Unity SkeletonBone object
                var skelBone = new SkeletonBone();
                skelBone.name = v.name;
                skelBone.position = v.transform.localPosition;
                skelBone.rotation = Quaternion.identity;
                skelBone.scale = Vector3.one;
                skeletonBones.Add(skelBone);
            }

            // Now, set up the HumanDescription for the retargeting source Avatar.
            var humanDesc = new HumanDescription();
            // human readable name (humanName) to a skeleton bone name (boneName)
            humanDesc.human = humanBones.ToArray();
            // the skeleton stores the transforms assigned to each bone in T-Pose
            humanDesc.skeleton = skeletonBones.ToArray();

            // These all correspond to default values.
            humanDesc.upperArmTwist = 0.5f;
            humanDesc.lowerArmTwist = 0.5f;
            humanDesc.upperLegTwist = 0.5f;
            humanDesc.lowerLegTwist = 0.5f;
            humanDesc.armStretch = 0.05f;
            humanDesc.legStretch = 0.05f;
            humanDesc.feetSpacing = 0.0f;
            humanDesc.hasTranslationDoF = false;

            // Finally, take the description and build the Avatar for the Motive PoseHandler
            var generatedAvatar = AvatarBuilder.BuildHumanAvatar(RootObj, humanDesc);

            // confirm everything is correct
            if (generatedAvatar.isValid == false || generatedAvatar.isHuman == false)
            {
                Debug.LogError("Unable to create source Avatar for retargeting. Check that your Skeleton Asset Name " +
                               "and Bone Naming Convention are configured correctly.");
                return;
            }

            // create the HumanPose handlers to map from the Motive Avatar to the Destination Avatar.
            _sourcePoseHandler = new HumanPoseHandler(generatedAvatar, RootObj.transform);
            _destPoseHandler = new HumanPoseHandler(destAvatar, destGameObject.transform);
        }


        public Transform GetTransformOfMecanimName(string mecanimName)
        {
            if (BoneMap.ContainsKey(mecanimName))
                return BoneMap[mecanimName].transform;
            return null;
        }
    }
}