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
    public System.Action pieceSnapStartDelegate;
    public System.Action pieceSnapEndDelegate;

    public Button cancelButton;

    IEnumerator Start()
    {
        Physics.autoSimulation = false;
        upgradeMenu = GetComponentInParent<UpgradeMenu>();
        toAttachElement = Instantiate(newEffect.diceSidePrefab, toAttachContainer);
        toAttachElement.gameObject.layer = gameObject.layer;
        dices = new UIDice[upgradeMenu.config.dices.Count];
        for(int i=0; i<upgradeMenu.config.dices.Count; i++)
        {
            UIDice dice = Instantiate(dicePrefab, diceContainer);
            dices[i] = dice;
            dice.transform.position = diceOffset / 2 * (i - (upgradeMenu.config.dices.Count-1) / 2.0f) + secondRowOffset * ((i+1)%2);
            dice.camera = camera;
            dice.GetComponent<DiceBuilder>().diceConfig = upgradeMenu.config.dices[i];
            int diceIndex = i;
            dice.diceSlotClickedDelegate += (DiceSlot slot, int slotIndex) => {
                upgradeMenu.config.dices[diceIndex].sides[slotIndex] = newEffect;
                StartCoroutine(AttachToSlotCoroutine(slot));
            };
        }
        yield return null;
        Physics.Simulate(Time.fixedDeltaTime);
    }

    void OnDestroy()
    {
        Physics.autoSimulation = true;
    }

    public void TurnDices()
    {
        StartCoroutine(TurnCoroutine());
    }

    public IEnumerator AttachToSlotCoroutine(DiceSlot slot)
    {
        pieceSnapStartDelegate?.Invoke();
        yield return slot.AttachElementCoroutine(toAttachElement);
        pieceSnapEndDelegate?.Invoke();
        for(float time = 0; time < 0.5f; time += Time.unscaledDeltaTime)
            yield return null;
        upgradeFinishedDelegate?.Invoke();
        Destroy(gameObject);
        foreach(UIDice dice in dices)
        {
            Destroy(dice.gameObject);
        }
    }

    private IEnumerator TurnCoroutine()
    {
        List<Coroutine> coroutines = new List<Coroutine>();
        for(int i=0; i<dices.Length; i++)
        {
            coroutines.Add(StartCoroutine(dices[i].TurnCoroutine()));
        }
        foreach(Coroutine coroutine in coroutines)
            yield return coroutine;
        Physics.autoSimulation = false;
        Physics.Simulate(Time.fixedDeltaTime);
    }
}
