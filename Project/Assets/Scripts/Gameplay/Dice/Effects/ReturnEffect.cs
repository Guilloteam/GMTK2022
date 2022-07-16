using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnEffect : MonoBehaviour
{
    private Grabbable grabbable;
    void Start()
    {
        DiceBuilder diceBuilder = GetComponent<DiceEffectRoot>().dice;
        grabbable = diceBuilder.GetComponent<Grabbable>();
    }

    void Update()
    {
        if(grabbable && GrabHand.instance.grabbedElement == null)
        {
            GrabHand.instance.ForceGrab(grabbable);
            enabled = false;
        }
    }
}
