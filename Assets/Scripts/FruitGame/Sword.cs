using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Sword : MonoBehaviour
{
    public GameObject followMe;
    public float ampAmp = 700f;
    public float rotAmp = 1000f;
    public float vecAmp = 4f;

    private DateTime startTime = DateTime.Now;
    
    // Update is called once per frame
    private void Update()
    {
        if (DateTime.Now.Subtract(startTime).Seconds < 2)
        {
            transform.SetPositionAndRotation(
                followMe.transform.position,
                followMe.transform.rotation
            );
        }
        var amp = Vector3.Distance(transform.position, followMe.transform.position);
        amp = amp * amp * ampAmp;
        
        var rot = Quaternion.RotateTowards(
            transform.rotation, followMe.transform.rotation, Time.deltaTime * amp * rotAmp
        );
        var pos = Vector3.MoveTowards(
            transform.position, followMe.transform.position, Time.deltaTime * amp * vecAmp
        );
        transform.SetPositionAndRotation(pos, rot); 
    }
}