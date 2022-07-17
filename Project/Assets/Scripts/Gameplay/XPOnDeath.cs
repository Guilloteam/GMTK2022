using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPOnDeath : MonoBehaviour
{
    private DamageReceiver damageReceiver;
    public int xp;
    void Start()
    {
        damageReceiver = GetComponent<DamageReceiver>();
        damageReceiver.deathDelegate += () => {
            XPSystem.instance.AddXP(xp);
        };
    }

    void Update()
    {
        
    }
}
