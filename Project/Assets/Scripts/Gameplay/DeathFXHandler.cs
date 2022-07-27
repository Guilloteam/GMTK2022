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
        {
            Transform fx = Instantiate(fxPrefab, transform.position, Quaternion.identity);
            PaletteRoot paletteRoot = fx.GetComponent<PaletteRoot>();
            if(paletteRoot)
            {
                paletteRoot.palette = GetComponentInParent<PaletteRoot>().palette;
            }
        }

        if(toDestroy != null)
        {
            Destroy(toDestroy);
        }
    }
}
