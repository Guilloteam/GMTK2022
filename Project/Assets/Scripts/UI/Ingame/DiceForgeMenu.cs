using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceForgeMenu : MonoBehaviour
{
    private UpgradeMenu upgradeMenu;
    private UIDice[] dices;
    public UIDice dicePrefab;
    public Vector3 diceOffset = Vector3.right;
    public Vector3 secondRowOffset = Vector3.forward;
    public Transform diceContainer;
    public new Camera camera;
    public DiceEffectConfig newEffect;
    public Transform toAttachContainer;
    private Transform toAttachElement;
    public System.Action upgradeFinishedDelegate;
    public System.Action cancelUpgradeDelegate;

    public Button cancelButton;

    void Start()
    {
        upgradeMenu = GetComponentInParent<UpgradeMenu>();
        toAttachElement = Instantiate(newEffect.diceSidePrefab, toAttachContainer);
        toAttachElement.gameObject.layer = gameObject.layer;
        dices = new UIDice[upgradeMenu.config.dices.Count];
        for(int i=0; i<upgradeMenu.config.dices.Count; i++)
        {
            UIDice dice = Instantiate(dicePrefab, diceContainer);
            dices[i] = dice;
            dice.transform.position = diceOffset / 2 * (i - (upgradeMenu.config.dices.Count-1) / 2.0f) + secondRowOffset * (i%2);
            dice.camera = camera;
            dice.GetComponent<DiceBuilder>().diceConfig = upgradeMenu.config.dices[i];
            int diceIndex = i;
            dice.diceSlotClickedDelegate += (DiceSlot slot, int slotIndex) => {
                upgradeMenu.config.dices[diceIndex].sides[slotIndex] = newEffect;
                StartCoroutine(AttachToSlotCoroutine(slot));
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

    public IEnumerator AttachToSlotCoroutine(DiceSlot slot)
    {
        yield return slot.AttachElementCoroutine(toAttachElement);
        upgradeFinishedDelegate?.Invoke();
        Destroy(gameObject);
        foreach(UIDice dice in dices)
        {
            Destroy(dice.gameObject);
        }
    }
}
