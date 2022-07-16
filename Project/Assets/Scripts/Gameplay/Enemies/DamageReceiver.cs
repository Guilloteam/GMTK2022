using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    public int health = 2;
    private new Rigidbody rigidbody;

    public System.Action<int, Vector3> damageReceivedDelegate;
    public System.Action deathDelegate;

    private void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
    }

    public void OnDamageReceived(int damage, Vector3 forceApplied)
    {
        Vector3 direction = forceApplied;
        direction.y = 0;
        health -= damage;
        if(health <= 0)
        {
            deathDelegate?.Invoke();
        }
        else
        {
            damageReceivedDelegate?.Invoke(damage, forceApplied);
        }
    }

    
}
