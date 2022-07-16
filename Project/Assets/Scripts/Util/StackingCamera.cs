using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; 

public class StackingCamera : MonoBehaviour
{
    private new Camera camera;
    void Start()
    {
        camera = GetComponent<Camera>();
        Camera.main.GetUniversalAdditionalCameraData().cameraStack.Add(camera);
    }

    void OnDestroy()
    {
        if(Camera.main != null)
            Camera.main.GetUniversalAdditionalCameraData().cameraStack.Remove(camera);
    }

    void Update()
    {
        
    }
}
