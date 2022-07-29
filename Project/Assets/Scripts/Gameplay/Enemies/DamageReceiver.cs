using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    public DamageType[] allowedDamageTypes;
    public float health = 2;
    private float startHealth;
    private new Rigidbody rigidbody;
    public float maxSoundEventDelay = 1;
    private float soundEventTime;

    public System.Action<float, Vector3> damageReceivedDelegate;
    public System.Action<float> healReceivedDelegate;
    public System.Action damageReceivedSoundEvent;
    public System.Action healReceivedSoundEvent;
    public System.Action deathDelegate;
    public Transform deathPrefab;
    public Transform hurtPrefab;

    public float healthRatio { get { return health / startHealth; } }

    private void Start()
    {
        startHealth = health;
        rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
        soundEventTime -= Time.deltaTime;
    }

    public void OnDamageReceived(float damage, Vector3 forceApplied)
    {
        Vector3 direction = forceApplied;
        direction.y = 0;
        health -= damage;
        if(health >= startHealth)
        {
            health = startHealth;
        }
        if(health <= 0)
        {
            if(deathPrefab != null)
                Instantiate(deathPrefab, transform.position, deathPrefab.rotation);
            damageReceivedDelegate?.Invoke(damage, forceApplied);
            deathDelegate?.Invoke();
        }
        else
        {
            if(damage > 0 && hurtPrefab != null)
                Instantiate(hurtPrefab, transform.position, Quaternion.identity);
            if(soundEventTime <= 0)
            {
                if(damage > 0)
                {
                    damageReceivedDelegate?.Invoke(damage, forceApplied);
                    damageReceivedSoundEvent?.Invoke();
                }
                else 
                {
                    healReceivedSoundEvent?.Invoke();
                }
                soundEventTime = maxSoundEventDelay;
            }
        }
    }

    
}
