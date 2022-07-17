using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteApplier : MonoBehaviour
{
    private PaletteRoot paletteRoot;
    private new Renderer renderer;
    private MaterialPropertyBlock propertyBlock; 
    void Start()
    {
        propertyBlock = new MaterialPropertyBlock();
        paletteRoot = GetComponentInParent<PaletteRoot>();
        renderer = GetComponent<Renderer>();
        renderer.GetPropertyBlock(propertyBlock);
        paletteRoot.paletteChangedDelegate += UpdateDisplay;
        UpdateDisplay();
    }

    private void OnDestroy()
    {
        paletteRoot.paletteChangedDelegate -= UpdateDisplay;
    }

    void UpdateDisplay()
    {
        renderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor("_Color_R", paletteRoot.palette.color_R);
        propertyBlock.SetColor("_Color_V", paletteRoot.palette.color_G);
        propertyBlock.SetColor("_Color_B", paletteRoot.palette.color_B);
        renderer.SetPropertyBlock(propertyBlock);
    }
}
