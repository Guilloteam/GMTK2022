using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Dice/Dice Effect Config")]
public class DiceEffectConfig : ScriptableObject
{
    public Texture2D activateFXTexture;
    public Sprite uiSprite;
    public string description;
    public Color activateFXColor;
    public Transform diceSidePrefab;
}
