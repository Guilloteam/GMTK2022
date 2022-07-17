using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPSystem : MonoBehaviour
{
    public static XPSystem instance;
    public XPLevelConfig xpConfig;
    public System.Action levelUpDelegate;

    public float levelProgressionRatio { get { return currentXP / nextLevelXP; }}
    private void Awake()
    {
        instance = this;
        nextLevelXP = xpConfig.GetNextLevelXP(level);
    }

    public float currentXP = 0;
    public int level = 0;
    public float nextLevelXP { get; private set; }

    public void AddXP(float xp)
    {
        currentXP += xp;
        if(currentXP > nextLevelXP)
        {
            currentXP -= nextLevelXP;
            level++;
            levelUpDelegate?.Invoke();
            nextLevelXP = xpConfig.GetNextLevelXP(level);
        }
    }
}
