using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/XP Level Config")]
public class XPLevelConfig : ScriptableObject
{
    public float firstXPLevel = 10;
    public float scaling = 0.05f;
    public float GetNextLevelXP(int level)
    {
        return firstXPLevel * Mathf.Pow(1+scaling, level);
    }
}
