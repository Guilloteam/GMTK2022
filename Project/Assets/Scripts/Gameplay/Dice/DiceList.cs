using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dice/Dice List")]
public class DiceList : ScriptableObject
{
    public List<DiceBuild> dices = new List<DiceBuild>();
}
