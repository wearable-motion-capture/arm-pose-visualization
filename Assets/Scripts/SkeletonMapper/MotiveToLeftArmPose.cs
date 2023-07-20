using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace SkeletonMapper
{
    public class MotiveToLeftArmPose : Basis
    {
        public GameObject leftUpperArm;
        public GameObject leftLowerArm;
        public GameObject leftHand;

        private void LateUpdate()
        {
            var uat = BoneMap["LeftUpperArm"].transform;
            var lat = BoneMap["LeftLowerArm"].transform;
            var ht = BoneMap["LeftHand"].transform;

            var rot = Quaternion.Euler(0, 0, 90);
            leftUpperArm.transform.SetPositionAndRotation(uat.position, uat.rotation * rot);
            leftLowerArm.transform.SetPositionAndRotation(lat.position, lat.rotation * rot);
            leftHand.transform.SetPositionAndRotation(ht.position, ht.rotation * rot);

            // leftUpperArm.transform.rotation = uat.rotation * Quaternion.Euler(0, 0, 90);
            // leftLowerArm.transform.rotation = lat.rotation * Quaternion.Euler(0, 0, 90);
            // leftHand.transform.rotation = ht.rotation * Quaternion.Euler(0, 0, 90);
        }
    }
}