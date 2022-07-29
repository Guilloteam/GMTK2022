using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteFadeEffect : MonoBehaviour
{
    private PaletteApplier paletteApplier;
    public float duration = 5;
    private float time;

    void Start()
    {
        paletteApplier = GetComponent<PaletteApplier>();
        paletteApplier.updatePropertyBlockDelegate += OnPropertyBlockUpdate;
    }

    private void OnDestroy()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        paletteApplier.UpdateDisplay();
    }

    private void OnPropertyBlockUpdate(MaterialPropertyBlock propertyBlock)
    {
        propertyBlock.SetFloat("_Alpha", 1 - time / duration);
    }
}
