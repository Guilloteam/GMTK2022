using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum DamageType
{
    Dice,
    Enemy,
    Spell
}
public class CollisionDamageDealer : MonoBehaviour
{
    public DamageType damageType;
    public int damage;
    public float impactThreshold;

    private void OnCollisionEnter(Collision collision)
    {
        DamageReceiver damageReceiver = collision.collider.GetComponent<DamageReceiver>();
        if(damageReceiver != null && collision.impulse.sqrMagnitude > impactThreshold * impactThreshold)
        {
            for(int i=0; i<damageReceiver.allowedDamageTypes.Length; i++)
            {
                if(damageReceiver.allowedDamageTypes[i] == damageType)
                {
                    damageReceiver.OnDamageReceived(damage, -collision.impulse);
                    return;
                }
            }
        }
    }
}