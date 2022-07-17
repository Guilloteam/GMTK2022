using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DiceManager : MonoBehaviour
{
    public static DiceManager instance;
    public DiceBuilder dicePrefab;
    public DiceList startConfig;
    private List<DiceBuilder> spawnedDices = new List<DiceBuilder>();
    public Transform[] diceSpawnPoints;

    [SerializeField]
    private DiceList currentConfig;
    public UpgradeMenu diceForgeMenuPrefab;

    private UpgradeMenu currentDiceForgeMenu;
    public DiceEffectConfig[] availableDiceEffects;
    public DiceBuild emptyDiceConfig;
    public bool gamePaused = false;
    public Vector3 newDiceSpawnOffset;

    void Awake()
    {
        instance = this;
        currentConfig = Instantiate(startConfig);
        for(int i=0; i<currentConfig.dices.Count; i++)
        {
            currentConfig.dices[i] = Instantiate(currentConfig.dices[i]);
        }
    }

    void Start()
    {
        XPSystem.instance.levelUpDelegate += OpenDiceForgeMenu;
        for(int i=0; i<currentConfig.dices.Count; i++)
        {
            DiceBuilder dice = Instantiate(dicePrefab, diceSpawnPoints[i].position, Quaternion.identity);
            dice.diceConfig = currentConfig.dices[i];
            spawnedDices.Add(dice);
        }
    }

    public void AddEmptyDice()
    {
        DiceBuilder dice = Instantiate(dicePrefab, KeyboardMovement.instance.transform.position + newDiceSpawnOffset, Quaternion.identity);
        dice.diceConfig = Instantiate(emptyDiceConfig);
        currentConfig.dices.Add(dice.diceConfig);
        spawnedDices.Add(dice);
    }

    public void UpdateDiceFaces()
    {
        for(int i=0; i<currentConfig.dices.Count; i++)
        {
            DiceBuilder dice = null;
            if(i >= spawnedDices.Count)
            {
                dice = Instantiate(dicePrefab, diceSpawnPoints[i].position, Quaternion.identity);
                dice.diceConfig = currentConfig.dices[i];
            }
            else dice = spawnedDices[i];
            dice.diceConfig = currentConfig.dices[i];
            dice.UpdateFaces();
        }
    }

    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            OpenDiceForgeMenu();
        }
    }

    public void OpenDiceForgeMenu()
    {
        gamePaused = true;
        currentDiceForgeMenu = Instantiate(diceForgeMenuPrefab);
        currentDiceForgeMenu.config = currentConfig;
        // currentDiceForgeMenu.available = availableDiceEffects[Random.Range(0, availableDiceEffects.Length)];
        currentDiceForgeMenu.upgradeFinishedDelegate += () => {
            UpdateDiceFaces();
            gamePaused = false;
        };
    }
}
