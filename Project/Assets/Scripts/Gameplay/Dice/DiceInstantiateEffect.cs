using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceInstantiateEffect : MonoBehaviour
{
    public Transform prefab;
    public DiceEffectConfig diceEffectConfig;
    void Start()
    {
        GetComponentInParent<DiceSlot>().activationStartDelegate += StartEffect;
    }

    void StartEffect()
    {
        Instantiate(prefab, transform.position, prefab.rotation).gameObject.AddComponent<DiceEffectRoot>().config = diceEffectConfig;
    }
}