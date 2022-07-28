using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDiceEffect : MonoBehaviour
{
    public int score;
    public ScoreBonus bonusPrefab; 

    void Start()
    {
        float multiplier = ScoreMultiplierEffect.GetCurrentMultiplier();
        ScoreSystem.instance.score += score * multiplier;
        XPSystem.instance.AddXP(score * multiplier);
        Instantiate(bonusPrefab, transform.position, bonusPrefab.transform.rotation).bonus = (int)(score * multiplier);
    }
}
