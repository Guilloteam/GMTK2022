using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    public AcidPool effectPrefab;
    private AcidPool activeEffect;
    public float range = 1;

    public void Start()
    {
        activeEffect = Instantiate(effectPrefab, transform.position, effectPrefab.transform.rotation);
    }

    private void Update()
    {
        if(activeEffect != null)
        {
            activeEffect.quantity += Time.deltaTime;
        }
        if(Vector3.SqrMagnitude(transform.position - activeEffect.transform.position) > range * range)
        {
            activeEffect = Instantiate(effectPrefab, transform.position, effectPrefab.transform.rotation);
        }
    }
}
