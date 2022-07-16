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

    void Start()
    {
        descriptionText.text = upgrade.description;
        iconImage.sprite = upgrade.uiSprite;
    }
}
