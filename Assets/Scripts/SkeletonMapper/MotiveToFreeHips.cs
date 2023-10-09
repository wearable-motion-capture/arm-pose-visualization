using UnityEngine;

namespace SkeletonMapper
{
    public class MotiveToFreeHips : Basis
    {
        [SerializeField] private GameObject leftUpperArm;
        [SerializeField] private GameObject leftLowerArm;
        [SerializeField] private GameObject leftHand;

        [SerializeField] private GameObject rightUpperArm;
        [SerializeField] private GameObject rightLowerArm;
        [SerializeField] private GameObject rightHand;

        [SerializeField] private GameObject hips;

        private void LateUpdate()
        {
            hips.transform.rotation = BoneMap["Hips"].transform.rotation;

            var luat = BoneMap["LeftUpperArm"].transform;
            var llat = BoneMap["LeftLowerArm"].transform;
            var lht = BoneMap["LeftHand"].transform;

            // this check is only to make the default position prettier
            if (luat.localRotation != Quaternion.identity && llat.localRotation != Quaternion.identity)
            {
                // Aligns arms with X axis. Negative for left, positive for right. This corresponds to the default T-pose,
                // which is the reference frame for Motive rotations 
                // local coord system of joints is Xup, Yforward, Zright
                var leftRot = Quaternion.Euler(0, 0, 90);
                leftUpperArm.transform.rotation = luat.rotation * leftRot;
                leftLowerArm.transform.rotation = llat.rotation * leftRot;
                leftHand.transform.rotation = lht.rotation * leftRot;
                // leftUpperArm.transform.SetPositionAndRotation(luat.position, luat.rotation * leftRot);
                // leftLowerArm.transform.SetPositionAndRotation(llat.position, llat.rotation * leftRot);
                // leftHand.transform.SetPositionAndRotation(lht.position, lht.rotation * leftRot);
            }


            var ruat = BoneMap["RightUpperArm"].transform;
            var rlat = BoneMap["RightLowerArm"].transform;
            var rht = BoneMap["RightHand"].transform;

            if (ruat.localRotation != Quaternion.identity && rlat.localRotation != Quaternion.identity)
            {
                // local coord system of joints is Xdown, Ybackward, Zright
                var rightRot = Quaternion.Euler(0, 180, -90);
                rightUpperArm.transform.rotation = ruat.rotation * rightRot;
                rightLowerArm.transform.rotation = rlat.rotation * rightRot;
                rightHand.transform.rotation = rht.rotation * rightRot;
                // rightUpperArm.transform.SetPositionAndRotation(ruat.position, ruat.rotation * rightRot);
                // rightLowerArm.transform.SetPositionAndRotation(rlat.position, rlat.rotation * rightRot);
                // rightHand.transform.SetPositionAndRotation(rht.position, rht.rotation * rightRot);
            }
        }
    }
}