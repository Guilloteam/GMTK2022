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
    public float damage;
    public float impactThreshold;
    public float pushbackForce = 50;
    private List<DamageReceiver> hurtTargets = new List<DamageReceiver>();

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!enabled)
            return;
        DamageReceiver damageReceiver = collision.collider.GetComponent<DamageReceiver>();
        if(hurtTargets.Contains(damageReceiver))
            return;
        if(damageReceiver != null && collision.impulse.sqrMagnitude > impactThreshold * impactThreshold)
        {
            for(int i=0; i<damageReceiver.allowedDamageTypes.Length; i++)
            {
                if(damageReceiver.allowedDamageTypes[i] == damageType)
                {
                    damageReceiver.OnDamageReceived(damage, collision.impulse.normalized);
                    RecoilDamageHandler recoilHandler = damageReceiver.GetComponent<RecoilDamageHandler>();
                    Vector3 pushbackDirection = collision.impulse;
                    pushbackDirection.y = 0;
                    recoilHandler.OnPushback(pushbackDirection.normalized * pushbackForce);
                    hurtTargets.Add(damageReceiver);
                    return;
                }
            }
        }
    }

    public void ResetHurtTargets()
    {
        hurtTargets.Clear();
    }
}