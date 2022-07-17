using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceEffectFX : MonoBehaviour
{
    public Vector3 velocity;
    public float startScale = 1;
    public float endScale = 2;
    public float duration = 2;
    private MeshRenderer meshRenderer;
    public Color startColor;
    public string colorParamName = "_Color";
    public Texture2D texture;

    IEnumerator Start()
    {
        Vector3 startPosition = transform.position;
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.GetPropertyBlock(propertyBlock);
        DiceEffectRoot root = GetComponentInParent<DiceEffectRoot>();
        propertyBlock.SetTexture("_MainTex", root.config.activateFXTexture);
        for(float time = 0; time < duration; time += Time.deltaTime)
        {
            transform.position = startPosition + time * velocity;
            transform.localScale = Vector3.one * Mathf.Lerp(startScale, endScale, time / duration);
            propertyBlock.SetColor(colorParamName, new Color(startColor.r, startColor.g, startColor.b, startColor.a * (1 - time / duration)));
            meshRenderer.SetPropertyBlock(propertyBlock);
            yield return null;
        }
    }
}
