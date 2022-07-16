using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceInstantiateEffect : MonoBehaviour
{
    public Transform prefab;
    public DiceEffectConfig diceEffectConfig;
    private DiceSlot slot;
    void Start()
    {
        slot = GetComponentInParent<DiceSlot>();
        if(slot != null)
            slot.activationStartDelegate += StartEffect;
    }

    void OnDestroy()
    {
        if(slot != null)
            slot.activationStartDelegate -= StartEffect;
    }

    void StartEffect()
    {
        Instantiate(prefab, transform.position, prefab.rotation).gameObject.AddComponent<DiceEffectRoot>().config = diceEffectConfig;
    }
}
