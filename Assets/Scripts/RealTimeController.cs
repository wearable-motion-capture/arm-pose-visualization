using UnityEngine;

namespace Scripts
{
    public class RealTimeController : MonoBehaviour
    {
        [SerializeField] private MotiveToUnityMapper skeletonMapper;
        [SerializeField] private DualAppListener swListener;

        // Update is called once per frame
        private void Update()
        {
            swListener.MoveBoneMap(skeletonMapper.GetBoneMap());
        }
    }
}