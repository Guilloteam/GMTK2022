using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[ExecuteInEditMode]
public class CameraCapture : MonoBehaviour
{
    public new Camera camera;
    int height = 1024;
    int width = 1024;
    int depth = 24;

    public bool capture;
    public string targetDirectory = "";
    public string targetPath = "test.png";

    private void Update()
    {
        if(capture)
        {
            CaptureScreen();
            
            capture = false;
        }
    }

    public void CaptureScreen() 
    {
        RenderTexture renderTexture = new RenderTexture(width, height, depth);
        Rect rect = new Rect(0,0,width,height);
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        camera.targetTexture = renderTexture;
        camera.Render();

        RenderTexture currentRenderTexture = RenderTexture.active;
        RenderTexture.active = renderTexture;
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();
        byte[] data = texture.EncodeToPNG();
        string path = Application.dataPath + "/" + targetPath;
        File.WriteAllBytes(path, data);

        camera.targetTexture = null;
        RenderTexture.active = currentRenderTexture;
        DestroyImmediate(renderTexture);
    }
}
