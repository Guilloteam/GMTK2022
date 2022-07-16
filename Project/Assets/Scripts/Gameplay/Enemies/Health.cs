using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startHealth;
    public int currentHealth;
    public Transform damageFXPrefab;
    void Start()
    {
        currentHealth = startHealth;
    }

    void Update()
    {
        
    }

}
