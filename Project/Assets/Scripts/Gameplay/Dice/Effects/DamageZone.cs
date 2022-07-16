using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float radius = 5;
    public float damage = 1;
    public float repulseMaxForce = 10;
    public float repulseMinForce = 1;
    public LayerMask layerMask;
    public bool applyOnStart = true;

    void Start()
    {
        if(applyOnStart)
        {
            ApplyDamage();
        }
    }

    void Update()
    {
        
    }

    public void ApplyDamage()
    {
        Collider[] inRangeColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        foreach(Collider collider in inRangeColliders)
        {
            DamageReceiver damageReceiver = collider.GetComponent<DamageReceiver>();
            if(damageReceiver != null)
            {
                Vector3 direction = collider.transform.position - transform.position;
                damageReceiver.OnDamageReceived(damage, direction.normalized * Mathf.Lerp(repulseMaxForce, repulseMinForce, direction.magnitude / radius));
            }
        }
    }
}
