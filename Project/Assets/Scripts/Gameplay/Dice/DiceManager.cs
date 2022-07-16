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
    public DiceForgeMenu diceForgeMenuPrefab;

    private DiceForgeMenu currentDiceForgeMenu;
    public DiceEffectConfig[] availableDiceEffects;
    public bool gamePaused = false;

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
        for(int i=0; i<currentConfig.dices.Count; i++)
        {
            DiceBuilder dice = Instantiate(dicePrefab, diceSpawnPoints[i].position, Quaternion.identity);
            dice.diceConfig = currentConfig.dices[i];
            spawnedDices.Add(dice);
        }
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
        currentDiceForgeMenu.newEffect = availableDiceEffects[Random.Range(0, availableDiceEffects.Length)];
        currentDiceForgeMenu.upgradeFinishedDelegate += () => {
            UpdateDiceFaces();
            gamePaused = false;
        };
    }
}
