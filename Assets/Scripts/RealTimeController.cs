using UnityEngine;


public class RealTimeController : MonoBehaviour
{
    [SerializeField] private SkeletonMapper.Basis skeletonMapper;
    [SerializeField] private DualAppListener swListener;

    // Update is called once per frame
    private void Update()
    {
        swListener.MoveBoneMap(skeletonMapper.GetBoneMap());
    }
}