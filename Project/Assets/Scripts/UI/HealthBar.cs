using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform bar;
    private DamageReceiver damageReceiver;

    void Start()
    {
        damageReceiver = KeyboardMovement.instance.GetComponentInChildren<DamageReceiver>();
    }

    void Update()
    {
        bar.anchorMax = new Vector2(damageReceiver.healthRatio, bar.anchorMax.y);
    }
}
