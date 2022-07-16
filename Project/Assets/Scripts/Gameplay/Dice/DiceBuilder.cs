using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBuilder : MonoBehaviour
{
    public DiceEffectConfig[] configs;
    private DiceSlots slotContainer;
    void Start()
    {
        slotContainer = GetComponent<DiceSlots>();
        for(int i=0; i<Mathf.Min(slotContainer.slots.Length, configs.Length); i++)
        {
            Instantiate(configs[i].diceSidePrefab, slotContainer.slots[i].transform);
        }
    }
}
