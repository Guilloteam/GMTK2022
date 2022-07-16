using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private DamageZone damageZone;
    private static List<ExplosionEffect> activeEffects = new List<ExplosionEffect>();
    public GameObject fxPrefab;

    void Start()
    {
        damageZone = GetComponent<DamageZone>();
        activeEffects.Add(this);
        foreach(ExplosionEffect effect in activeEffects)
        {
            effect.PlayEffect();
        }
    }

    private void OnDestroy()
    {
        activeEffects.Remove(this);
    }

    public void PlayEffect()
    {
        damageZone.ApplyDamage();
        Instantiate(fxPrefab, transform.position, transform.rotation);
    }
}
