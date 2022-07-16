using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CollisionDamageDealer : MonoBehaviour
{
    public int damage;
    public float impactThreshold;

    private void OnCollisionEnter(Collision collision)
    {
        DamageReceiver damageReceiver = collision.collider.GetComponent<DamageReceiver>();
        if(damageReceiver != null && collision.impulse.sqrMagnitude > impactThreshold * impactThreshold)
        {
            damageReceiver.OnDamageReceived(damage, -collision.impulse);
        }
    }
}