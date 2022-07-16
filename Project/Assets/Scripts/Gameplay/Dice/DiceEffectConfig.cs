using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Dice/Dice Effect Config")]
public class DiceEffectConfig : ScriptableObject
{
    public Texture2D activateFXTexture;
    public Transform diceSidePrefab;
}
