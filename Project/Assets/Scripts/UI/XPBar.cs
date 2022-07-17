using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBar : MonoBehaviour
{
    public RectTransform bar;
    void Start()
    {
        
    }

    void Update()
    {
        bar.anchorMax = new Vector2(XPSystem.instance.levelProgressionRatio, bar.anchorMax.y);
    }
}
