using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFXHandler : MonoBehaviour
{
    public GameObject toDestroy;
    public Transform fxPrefab;
    void Start()
    {
        GetComponent<DamageReceiver>().deathDelegate += OnDeath;
        
    }

    private void OnDeath()
    {
        if(fxPrefab != null)
            Instantiate(fxPrefab, transform.position, Quaternion.identity);
        if(toDestroy != null)
        {
            Destroy(toDestroy);
        }
    }
}
