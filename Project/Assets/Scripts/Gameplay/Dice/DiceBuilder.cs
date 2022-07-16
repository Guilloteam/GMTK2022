using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBuilder : MonoBehaviour
{
    public DiceBuild diceConfig;
    private DiceSlots slotContainer;
    void Start()
    {
        slotContainer = GetComponent<DiceSlots>();
        for(int i=0; i<Mathf.Min(slotContainer.slots.Length, diceConfig.sides.Length); i++)
        {
            Transform instance = Instantiate(diceConfig.sides[i].diceSidePrefab, slotContainer.slots[i].transform);
            instance.gameObject.layer = gameObject.layer;
        }
    }
}
