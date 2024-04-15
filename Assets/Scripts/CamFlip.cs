using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFlip : MonoBehaviour
{
    private Camera cam;

    public bool flipHorizontal = true;

    void Awake () {
        cam = GetComponent<Camera>();
    }

    void OnPreCull() {
        cam.ResetWorldToCameraMatrix();
        cam.ResetProjectionMatrix();
        Vector3 scale = new Vector3(flipHorizontal ? -1 : 1, 1, 1);
        cam.projectionMatrix *= Matrix4x4.Scale(scale);
    }

    void OnPreRender () {
        GL.invertCulling = flipHorizontal;
    }
	
    void OnPostRender () {
        GL.invertCulling = false;
    }
}
