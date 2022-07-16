using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceForgeMenu : MonoBehaviour
{
    public DiceList config;
    private UIDice[] dices;
    public UIDice dicePrefab;
    public Vector3 diceOffset = Vector3.right;
    public Transform diceContainer;
    public new Camera camera;

    void Start()
    {
        dices = new UIDice[config.dices.Count];
        for(int i=0; i<config.dices.Count; i++)
        {
            UIDice dice = Instantiate(dicePrefab, diceContainer);
            dices[i] = dice;
            dice.transform.position = diceOffset * (i - (config.dices.Count-1) / 2.0f);
            dice.camera = camera;
            dice.diceSlotClickedDelegate += (DiceSlot slot) => {

            };
        }
    }

    public void TurnDices()
    {
        for(int i=0; i<dices.Length; i++)
        {
            dices[i].Turn();
        }
    }
}
