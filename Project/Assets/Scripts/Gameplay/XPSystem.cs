using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPSystem : MonoBehaviour
{
    public static XPSystem instance;
    public XPLevelConfig xpConfig;
    public System.Action levelUpDelegate;

    private void Awake()
    {
        instance = this;
    }

    public float currentXP = 0;
    public int level = 0;
    public float nextLevelXP { get { return xpConfig.GetNextLevelXP(level); }}

    public void AddXP(float xp)
    {
        currentXP += xp;
        if(currentXP > nextLevelXP)
        {
            currentXP -= nextLevelXP;
            level++;
            levelUpDelegate?.Invoke();
        }
    }
}
