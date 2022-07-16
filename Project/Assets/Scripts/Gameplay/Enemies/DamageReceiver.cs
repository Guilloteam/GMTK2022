using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    public float recoilMultiplier = 2;
    private new Rigidbody rigidbody;

    public System.Action<int, Vector3> damageReceivedDelegate;

    private void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
    }

    public void OnDamageReceived(int damage, Vector3 forceApplied)
    {
        Vector3 direction = forceApplied;
        direction.y = 0;
        rigidbody.AddForce(direction.normalized * recoilMultiplier);
        damageReceivedDelegate?.Invoke(damage, forceApplied);
    }

    
}
