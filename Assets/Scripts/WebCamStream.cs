using UnityEngine;


public class WebcamStream : MonoBehaviour
{
    private WebCamTexture _webcamTexture;
    
    void Start()
    {
        _webcamTexture = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = _webcamTexture;
        _webcamTexture.Play();
    }
}