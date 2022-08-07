using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class AcidPool : MonoBehaviour
{
    public float capacity = 3;
    public float quantity = 0;
    public float emptyRate = 0.2f;
    public float maxRadius = 10;
    private MaterialPropertyBlock propertyBlock;
    private Renderer renderer;
    private float radiusRatio { get { float f = quantity / capacity; return 1 - (1-f) * (1-f); }}

    void Start() 
    {
        propertyBlock = new MaterialPropertyBlock();
        renderer = GetComponent<Renderer>();
        GetComponent<Renderer>().GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat("_Zone_Radius", 0);
        GetComponent<Renderer>().SetPropertyBlock(propertyBlock);
    }

    void Update()
    {
        transform.localScale = radiusRatio * maxRadius * 4 * Vector3.one;
        propertyBlock.SetFloat("_Zone_Radius", Mathf.Clamp01(radiusRatio) * maxRadius);
        GetComponent<Renderer>().SetPropertyBlock(propertyBlock);
        quantity -= emptyRate * Time.deltaTime;
        if(quantity < -1)
            Destroy(gameObject);
    }
}
