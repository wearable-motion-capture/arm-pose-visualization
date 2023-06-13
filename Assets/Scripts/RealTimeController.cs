using UnityEngine;

namespace Scripts
{
    public class RealTimeController : MonoBehaviour
    {
        [SerializeField] private DollAnimator2 dollAnimator; // the handler to animate the robot
        [SerializeField] private DualAppListener swListener;

        // Update is called once per frame
        private void Update()
        {
            swListener.MoveBoneMap(dollAnimator.GetBoneMap());
        }
    }
}
