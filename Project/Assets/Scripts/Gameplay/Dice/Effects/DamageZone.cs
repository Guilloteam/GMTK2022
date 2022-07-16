using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float radius = 5;
    public int damage = 1;
    public float repulseMaxForce = 10;
    public float repulseMinForce = 1;
    public LayerMask layerMask;

    void Start()
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

    void Update()
    {
        
    }
}
