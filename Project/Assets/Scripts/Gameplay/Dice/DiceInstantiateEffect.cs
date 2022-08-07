using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiceEffectType
{
    StopOnGrab,
    StopOnThrow,
}

public class DiceInstantiateEffect : MonoBehaviour
{
    public Transform prefab;
    public DiceEffectType effectType;
    public DiceEffectConfig diceEffectConfig;
    private DiceSlot slot;
    public bool attachToSide = false;
    public float effectDuration = 1;
    private bool effectStopped = false;
    private DiceEffectRoot diceEffectRoot;
    
    void Start()
    {
        slot = GetComponentInParent<DiceSlot>();
        if(slot != null)
        {
            slot.activationStartDelegate += StartEffect;
            switch(effectType)
            {
                case DiceEffectType.StopOnGrab:
                    slot.grabbedDelegate += StopEffect;
                    slot.activeFaceTurnedDelegate += StopEffect;
                    break;
                case DiceEffectType.StopOnThrow:
                    slot.thrownDelegate += StopEffect;
                    slot.activeFaceTurnedDelegate += StopEffect;
                    break;

            }
        }
    }

    void OnDestroy()
    {
        StopAllCoroutines();
        if(diceEffectRoot != null)
            Destroy(diceEffectRoot.gameObject);
        if(slot != null)
        {
            slot.activationStartDelegate -= StartEffect;
            slot.grabbedDelegate -= StopEffect;
            slot.activeFaceTurnedDelegate -= StopEffect;
        }
    }

    void StartEffect()
    {
        StartCoroutine(EffectCoroutine());
    }

    private void StopEffect()
    {
        effectStopped = true;
        Debug.Log("Effect Stopped");
    }

    private IEnumerator EffectCoroutine()
    {
        effectStopped = false;
        diceEffectRoot = Instantiate(prefab, transform.position, prefab.rotation, attachToSide ? transform : null).gameObject.AddComponent<DiceEffectRoot>();
        diceEffectRoot.config = diceEffectConfig;
        diceEffectRoot.dice = GetComponentInParent<DiceBuilder>();
        
        float time = 0;
        for(; !effectStopped && (effectDuration == 0 || time < effectDuration); time += Time.deltaTime)
            yield return null;
        if(time >= effectDuration)
            diceEffectRoot.activationFinishedDelegate?.Invoke();
        bool activationFinished = time >= effectDuration;
        while(!effectStopped)
            yield return null;
        Destroy(diceEffectRoot.gameObject);
    }
}
