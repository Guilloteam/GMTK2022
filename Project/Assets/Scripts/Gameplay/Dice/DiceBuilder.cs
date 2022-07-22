using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBuilder : MonoBehaviour
{
    public DiceBuild diceConfig;
    private DiceSlots slotContainer;
    private Transform[] diceFaces;
    private DiceEffectConfig[] sideEffects;
    void Start()
    {
        slotContainer = GetComponent<DiceSlots>();
        diceFaces = new Transform[slotContainer.slots.Length];
        sideEffects = new DiceEffectConfig[slotContainer.slots.Length];
        for(int i=0; i<Mathf.Min(slotContainer.slots.Length, diceConfig.sides.Length); i++)
        {
            Transform instance = Instantiate(diceConfig.sides[i].diceSidePrefab, slotContainer.slots[i].transform);
            instance.gameObject.layer = gameObject.layer;
            diceFaces[i] = instance;
            sideEffects[i] = diceConfig.sides[i];
        }
    }

    public void UpdateFaces()
    {
        for(int i=0; i<Mathf.Min(slotContainer.slots.Length, diceConfig.sides.Length); i++)
        {
            if(sideEffects[i] != diceConfig.sides[i])
            {
                Destroy(diceFaces[i].gameObject);
                Transform instance = Instantiate(diceConfig.sides[i].diceSidePrefab, slotContainer.slots[i].transform);
                instance.gameObject.layer = gameObject.layer;
                diceFaces[i] = instance;
                sideEffects[i] = diceConfig.sides[i];
            }
        }
    }
}
