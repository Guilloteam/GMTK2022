using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class AlphaShaderAnimation : MonoBehaviour
{
    public float appearDuration = 3;
    private MaterialPropertyBlock propertyBlock;
    private Renderer renderer;

    IEnumerator Start() 
    {
        propertyBlock = new MaterialPropertyBlock();
        renderer = GetComponent<Renderer>();
        GetComponent<Renderer>().GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat("_Alpha", 0);
        GetComponent<Renderer>().SetPropertyBlock(propertyBlock);
        for(float time = 0;time < appearDuration; time += Time.deltaTime)
        {
            propertyBlock.SetFloat("_Alpha", time / appearDuration);
            GetComponent<Renderer>().SetPropertyBlock(propertyBlock);
            yield return null;
        }
        propertyBlock.SetFloat("_Alpha", 1);
        GetComponent<Renderer>().SetPropertyBlock(propertyBlock);
    }

    void Update()
    {
        
    }
}
