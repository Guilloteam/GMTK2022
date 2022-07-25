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
        paletteRoot = GetComponentInParent<PaletteRoot>();
    }

    void Update()
    {
        
    }

    private void OnDeath()
    {
        Transform splatter = Instantiate(splatterPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.LookRotation(lastDamageDirection));
        splatter.gameObject.GetComponent<PaletteRoot>().palette = paletteRoot.palette;
    }

    private void OnDamageReceived(float damage, Vector3 direction)
    {
        lastDamageDirection = direction;
        lastDamageDirection.y = 0;
        lastDamageDirection = lastDamageDirection.normalized;
    }
}
