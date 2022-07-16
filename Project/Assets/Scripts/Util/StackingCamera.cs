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
        Camera.main.GetUniversalAdditionalCameraData().cameraStack.Remove(camera);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
