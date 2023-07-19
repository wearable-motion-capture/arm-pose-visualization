using UnityEngine;

namespace SkeletonMapper
{
    public class WearableToLeftArmPose : Basis
    {
        public GameObject leftUpperArm;
        public GameObject leftLowerArm;
        public GameObject leftHand;

        private void LateUpdate()
        {
            var uat = BoneMap["LeftUpperArm"].transform;
            var lat = BoneMap["LeftLowerArm"].transform;
            var ht = BoneMap["LeftHand"].transform;

            var leftRot = Quaternion.Euler(0, 90, 90);
            leftUpperArm.transform.SetPositionAndRotation(uat.position, uat.rotation * leftRot);
            leftLowerArm.transform.SetPositionAndRotation(lat.position, lat.rotation * leftRot);
            leftHand.transform.SetPositionAndRotation(ht.position, ht.rotation * leftRot);

            // leftUpperArm.transform.rotation = uat.rotation * Quaternion.Euler(0, 0, 90);
            // leftLowerArm.transform.rotation = lat.rotation * Quaternion.Euler(0, 0, 90);
            // leftHand.transform.rotation = ht.rotation * Quaternion.Euler(0, 0, 90);
        }
    }
}