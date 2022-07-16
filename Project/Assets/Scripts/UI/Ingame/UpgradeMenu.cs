using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public DiceList config;
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
        Debug.Log("RETURN TO UPGRADE SELECT");
        diceForgeMenu.gameObject.SetActive(false);
        mainUpgradePanel.SetActive(true);
        UICamera.gameObject.SetActive(false);
    }
}
