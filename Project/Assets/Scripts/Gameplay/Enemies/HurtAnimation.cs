using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAnimation : MonoBehaviour
{
    private DamageReceiver damageReceiver;
    public PaletteConfig hurtPalette;
    public PaletteConfig healPalette;
    private PaletteConfig defaultPalette;
    private PaletteConfig currentPalette;
    public PaletteRoot paletteRoot;
    public float maxHurtDamage = 1;
    public float maxShake = 0.3f;
    public float damageCountDecrease = 0.2f;
    private float damageCount = 0;
    public Transform shakeTransform;
    public Vector3 shakeDirections = new Vector3(1, 0, 1);
    private Vector3 startLocalPos;
    public float minHurtValue = 0.3f;


    void Start()
    {
        paletteRoot = GetComponentInParent<PaletteRoot>();
        defaultPalette = paletteRoot.palette;
        currentPalette = Instantiate(defaultPalette);
        paletteRoot.palette = currentPalette;
        damageReceiver = GetComponent<DamageReceiver>();
        damageReceiver.damageReceivedDelegate += (float damage, Vector3 direction) => {
            damageCount += damage;
            if(damageCount > maxHurtDamage)
            {
                damageCount = maxHurtDamage;
            }
            if(damageCount < minHurtValue)
            {
                damageCount = minHurtValue;
            }
        };
        startLocalPos = shakeTransform.localPosition;
    }

    void Update()
    {
        if(damageCount > 0)
        {
            damageCount -= damageCountDecrease * Time.deltaTime;
            float damageRatio = Mathf.Clamp01(damageCount / maxHurtDamage);
            if(Time.timeScale > 0)
            {
                shakeTransform.localPosition = startLocalPos + damageRatio * maxShake * new Vector3(shakeDirections.x * Random.Range(-1, 1), shakeDirections.y * Random.Range(-1, 1), shakeDirections.z * Random.Range(-1, 1));
            }
            currentPalette.color_R = Color.Lerp(defaultPalette.color_R, hurtPalette.color_R, damageRatio);
            currentPalette.color_G = Color.Lerp(defaultPalette.color_G, hurtPalette.color_G, damageRatio);
            currentPalette.color_B = Color.Lerp(defaultPalette.color_B, hurtPalette.color_B, damageRatio);
            paletteRoot.paletteChangedDelegate?.Invoke();
        }
        else
        {
            shakeTransform.localPosition = startLocalPos;
        }
    }
}
