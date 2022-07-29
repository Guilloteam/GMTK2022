using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1;
    public float recoil = 10;
    private new Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        DamageReceiver receiver = other.GetComponent<DamageReceiver>();
        if(receiver != null)
        {
            receiver.OnDamageReceived(damage, rigidbody.velocity.normalized);
            Destroy(gameObject);
        }
    }
}
