using System;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField] private Transform followMe;
    [SerializeField] private bool posAndRot;

    private void Update()
    {
        transform.position = followMe.position;
        if (posAndRot) transform.rotation = followMe.rotation;
    }
}