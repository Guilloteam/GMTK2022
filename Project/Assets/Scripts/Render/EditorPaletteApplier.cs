using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorPaletteApplier : MonoBehaviour
{
    private PaletteRoot paletteRoot;
    private new Renderer renderer;
    private MaterialPropertyBlock propertyBlock; 
    public System.Action<MaterialPropertyBlock> updatePropertyBlockDelegate;
    
    void Awake()
    {
        
        paletteRoot.paletteChangedDelegate += UpdateDisplay;
        UpdateDisplay();
    }

    private void OnDestroy()
    {
        paletteRoot.paletteChangedDelegate -= UpdateDisplay;
    }

    public void UpdateDisplay()
    {
        propertyBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(propertyBlock);
        paletteRoot = GetComponent<PaletteRoot>();
        renderer = GetComponent<Renderer>();
        renderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor("_Color_R", paletteRoot.startPalette.color_R);
        propertyBlock.SetColor("_Color_V", paletteRoot.startPalette.color_G);
        propertyBlock.SetColor("_Color_B", paletteRoot.startPalette.color_B);
        updatePropertyBlockDelegate?.Invoke(propertyBlock);
        renderer.SetPropertyBlock(propertyBlock);
    }

    private void Update()
    {
        if(!Application.isPlaying)
        {
            UpdateDisplay();
        }
    }
}
