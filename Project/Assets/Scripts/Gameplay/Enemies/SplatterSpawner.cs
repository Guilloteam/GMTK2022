using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterSpawner : MonoBehaviour
{
    public Transform splatterPrefab;
    private DamageReceiver damageReceiver;
    private PaletteRoot paletteRoot;
    private Vector3 lastDamageDirection;

    void Start()
    {
        damageReceiver = GetComponent<DamageReceiver>();
        damageReceiver.deathDelegate += OnDeath;
        damageReceiver.damageReceivedDelegate += OnDamageReceived;
    }

    void Update()
    {
        
    }

    private void OnDeath()
    {
        Instantiate(splatterPrefab, transform.position, Quaternion.LookRotation(lastDamageDirection));
    }

    private void OnDamageReceived(float damage, Vector3 direction)
    {
        lastDamageDirection = direction;
        lastDamageDirection.y = 0;
        lastDamageDirection = lastDamageDirection.normalized;
    }
}
