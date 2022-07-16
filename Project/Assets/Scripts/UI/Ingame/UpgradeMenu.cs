using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UpgradeElement
{
    public float probability;
    public DiceEffectConfig effect;
}

public class UpgradeMenu : MonoBehaviour
{
    public DiceList config;
    public UpgradeElement[] upgrades;
    public DiceEffectConfig[] availableUpgrades;
    public System.Action upgradeFinishedDelegate;
    public Transform upgradeListPanel;

    public UpgradeElementPanel elementPrefab;
    private UpgradeElementPanel[] elements;
    public DiceForgeMenu diceForgeMenu;
    public GameObject mainUpgradePanel;
    public Camera UICamera;

    void Start()
    {
        PickUpgrades(3);
        elements = new UpgradeElementPanel[availableUpgrades.Length];
        for(int i=0; i<availableUpgrades.Length; i++)
        {
            UpgradeElementPanel element = Instantiate(elementPrefab, upgradeListPanel);
            element.upgrade = availableUpgrades[i];
            elements[i] = element;
            int upgradeIndex = i;
            element.button.onClick.AddListener(() => {
                ShowDiceForgePanel(upgradeIndex);
            });
        }
        diceForgeMenu.upgradeFinishedDelegate += () => {Destroy(gameObject); upgradeFinishedDelegate?.Invoke();};
        diceForgeMenu.cancelButton.onClick.AddListener(ReturnToUpgradeSelect);
    }

    public void ShowDiceForgePanel(int upgradeIndex)
    {
        diceForgeMenu.newEffect = elements[upgradeIndex].upgrade;
        diceForgeMenu.gameObject.SetActive(true);
        mainUpgradePanel.SetActive(false);
        UICamera.gameObject.SetActive(true);
    }

    public void ReturnToUpgradeSelect()
    {
        diceForgeMenu.gameObject.SetActive(false);
        mainUpgradePanel.SetActive(true);
        UICamera.gameObject.SetActive(false);
    }

    private void PickUpgrades(int count)
    {
        availableUpgrades = new DiceEffectConfig[count];
        List<UpgradeElement> upgradeElements = new List<UpgradeElement>();
        upgradeElements.AddRange(upgrades);
        for(int i=0; i<count; i++)
        {
            float probabilitySum = 0;
            foreach(UpgradeElement element in upgradeElements)
            {
                probabilitySum += element.probability;
            }
            float randomCursor = Random.Range(0, probabilitySum);
            float probabilityCursor = 0;
            int j=0;
            for(j=0; probabilityCursor + upgradeElements[j].probability < randomCursor; j++)
            {
                probabilityCursor += upgradeElements[j].probability;
            }
            availableUpgrades[i] = upgradeElements[j].effect;
            upgradeElements.RemoveAt(j);
        }
    }
}
