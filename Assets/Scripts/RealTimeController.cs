using System.Collections.Generic;
using UnityEngine;


public class RealTimeController : MonoBehaviour
{
    [SerializeField] private SkeletonMapper.Basis skeletonMapper;
    [SerializeField] private List<Listener.Basis> listener;

    // Update is called once per frame
    private void Update()
    {
        for (var i = 0; i < listener.Count; i++) listener[i].MoveBoneMap(skeletonMapper.GetBoneMap());
    }
}