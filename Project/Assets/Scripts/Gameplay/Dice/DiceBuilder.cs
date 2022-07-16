using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBuilder : MonoBehaviour
{
    public DiceBuild diceConfig;
    private DiceSlots slotContainer;
    private Transform[] diceFaces;
    void Start()
    {
        slotContainer = GetComponent<DiceSlots>();
        diceFaces = new Transform[slotContainer.slots.Length];
        for(int i=0; i<Mathf.Min(slotContainer.slots.Length, diceConfig.sides.Length); i++)
        {
            Transform instance = Instantiate(diceConfig.sides[i].diceSidePrefab, slotContainer.slots[i].transform);
            instance.gameObject.layer = gameObject.layer;
            diceFaces[i] = instance;
        }
    }

    public void UpdateFaces()
    {
        for(int i=0; i<Mathf.Min(slotContainer.slots.Length, diceConfig.sides.Length); i++)
        {
            Destroy(diceFaces[i]);
            Transform instance = Instantiate(diceConfig.sides[i].diceSidePrefab, slotContainer.slots[i].transform);
            instance.gameObject.layer = gameObject.layer;
            diceFaces[i] = instance;
        }
    }
}
