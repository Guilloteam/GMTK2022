using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceEffectRoot : MonoBehaviour
{
    public DiceEffectConfig config;
    public DiceBuilder dice;
    public float activationDuration;
    public float activationTime;
    public System.Action activationStartedDelegate;
    public System.Action activationFinishedDelegate;
    public System.Action activationInterruptedDelegate;

}
