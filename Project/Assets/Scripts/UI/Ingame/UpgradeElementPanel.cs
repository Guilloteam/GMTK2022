using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeElementPanel : MonoBehaviour
{
    public DiceEffectConfig upgrade;
    public TMPro.TextMeshProUGUI descriptionText;
    public Button button;
    public Image iconImage;
    public DiceEffectConfig addDiceEffectConfig;

    void Start()
    {
        if(upgrade == null)
        {
            descriptionText.text = addDiceEffectConfig.description;
            iconImage.sprite = addDiceEffectConfig.uiSprite;
        }
        else
        {
            descriptionText.text = upgrade.description;
            iconImage.sprite = upgrade.uiSprite;
        }
    }
}
