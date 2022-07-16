using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSlots : MonoBehaviour
{
    public DiceSlot[] slots;

    private void Awake()
    {
        for(int i=0; i<slots.Length; i++)
            slots[i].slotIndex = i;
    }
}
