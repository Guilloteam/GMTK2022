using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplierEffect : MonoBehaviour
{
    public float multiplier = 2;
    private static List<ScoreMultiplierEffect> activeEffects = new List<ScoreMultiplierEffect>();

    void Start()
    {
        activeEffects.Add(this);
    }

    private void OnDestroy()
    {
        activeEffects.Remove(this);
    }

    public static float GetCurrentMultiplier()
    {
        float result = 1;
        foreach(ScoreMultiplierEffect effect in activeEffects)
        {
            result *= effect.multiplier;
        }
        return result;
    }
}
