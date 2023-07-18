using System.Collections.Generic;
using UnityEngine;


public class RealTimeController : MonoBehaviour
{
    [SerializeField] private SkeletonMapper.Basis skeletonMapper;
    [SerializeField] private List<DualAppListener> swListener;

    // Update is called once per frame
    private void Update()
    {
        for (var i = 0; i < swListener.Count; i++) swListener[i].MoveBoneMap(skeletonMapper.GetBoneMap());
    }
}