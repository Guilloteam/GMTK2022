using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnDeath : MonoBehaviour
{
    private DamageReceiver damageReceiver;
    public int score;
    public ScoreBonus fxPrefab;
    void Start()
    {
        damageReceiver = GetComponent<DamageReceiver>();
        damageReceiver.deathDelegate += () => {
            ScoreSystem.instance.score += score;
            Instantiate(fxPrefab, transform.position, fxPrefab.transform.rotation).bonus = score;
        };
    }

    void Update()
    {
        
    }
}
