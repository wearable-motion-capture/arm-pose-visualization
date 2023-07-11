using System;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField] private Transform followMe;
    [SerializeField] private bool both;

    private void Update()
    {
        transform.position = followMe.position;
        if (both) transform.rotation = followMe.rotation;
    }
}