using UnityEngine;

namespace SkeletonMapper
{
    public class WearableToBothArms : Basis
    {
        [SerializeField] private GameObject leftUpperArm;
        [SerializeField] private GameObject leftLowerArm;
        [SerializeField] private GameObject leftHand;


        [SerializeField] private GameObject rightUpperArm;
        [SerializeField] private GameObject rightLowerArm;
        [SerializeField] private GameObject rightHand;

        private void LateUpdate()
        {
            var luat = BoneMap["LeftUpperArm"].transform;
            var llat = BoneMap["LeftLowerArm"].transform;
            var lht = BoneMap["LeftHand"].transform;

            var ruat = BoneMap["RightUpperArm"].transform;
            var rlat = BoneMap["RightLowerArm"].transform;
            var rht = BoneMap["RightHand"].transform;

            // bring arms into forward position. Which, is the calibration pose
            // local coord system of joints is Xup, Yforward, Zright
            var leftRot = Quaternion.Euler(0, 90, 90);
            leftUpperArm.transform.SetPositionAndRotation(luat.position, luat.rotation * leftRot);
            leftLowerArm.transform.SetPositionAndRotation(llat.position, llat.rotation * leftRot);
            leftHand.transform.SetPositionAndRotation(lht.position, lht.rotation * leftRot);

            // local coord system of joints is Xdown, Ybackward, Zright
            var rightRot = Quaternion.Euler(0, 90, -90);
            rightUpperArm.transform.SetPositionAndRotation(ruat.position, ruat.rotation * rightRot);
            rightLowerArm.transform.SetPositionAndRotation(rlat.position, rlat.rotation * rightRot);
            rightHand.transform.SetPositionAndRotation(rht.position, rht.rotation * rightRot);
        }
    }
}