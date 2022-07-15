using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableMaterialHighlight : MonoBehaviour
{
    private Grabbable grabbable;
    public Material highlightMaterial;
    public MeshRenderer meshRenderer;

    private Material defaultMaterial;
    
    private void Start()
    {
        grabbable = GetComponent<Grabbable>();
        grabbable.hoverStartDelegate += OnHoverStart;
        grabbable.hoverEndDelegate += OnHoverEnd;
        defaultMaterial = meshRenderer.material;
    }
    
    private void OnHoverStart()
    {
        meshRenderer.material = highlightMaterial;
    }

    private void OnHoverEnd()
    {
        meshRenderer.material = defaultMaterial;
    }
}
