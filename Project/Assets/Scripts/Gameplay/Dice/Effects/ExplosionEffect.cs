using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private DamageZone damageZone;
    private static List<ExplosionEffect> activeEffects = new List<ExplosionEffect>();
    public GameObject fxPrefab;
    public string explosionGroup;

    void Start()
    {
        damageZone = GetComponent<DamageZone>();
        activeEffects.Add(this);
        if(explosionGroup != "")
        {
            foreach(ExplosionEffect effect in activeEffects)
            {
                if(effect.explosionGroup == explosionGroup)
                    effect.PlayEffect();
            }
        }
        else
        {
            PlayEffect();
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
