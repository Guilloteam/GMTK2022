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
    public PaletteConfig[] palettes;
    public Transform levelUpFXPrefab;

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
        XPSystem.instance.levelUpDelegate += LevelUp;
        for(int i=0; i<currentConfig.dices.Count; i++)
        {
            DiceBuilder dice = Instantiate(dicePrefab, diceSpawnPoints[i].position, Quaternion.identity);
            dice.GetComponent<PaletteRoot>().palette = palettes[spawnedDices.Count%palettes.Length];
            dice.diceConfig = currentConfig.dices[i];
            spawnedDices.Add(dice);
        }
    }

    public void AddEmptyDice()
    {
        DiceBuilder dice = Instantiate(dicePrefab, KeyboardMovement.instance.transform.position + newDiceSpawnOffset, Quaternion.identity);
        dice.GetComponent<PaletteRoot>().palette = palettes[spawnedDices.Count];
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
                dice.GetComponent<PaletteRoot>().palette = palettes[spawnedDices.Count];
                dice.diceConfig = currentConfig.dices[i];
            }
            else dice = spawnedDices[i];
            dice.diceConfig = currentConfig.dices[i];
            dice.UpdateFaces();
        }
    }

    void Update()
    {
        // if(Keyboard.current.spaceKey.wasPressedThisFrame)
        // {
        //     OpenDiceForgeMenu();
        // }
    }

    public void LevelUp()
    {
        StartCoroutine(LevelUpCoroutine());
        
    }

    public IEnumerator LevelUpCoroutine()
    {
        Instantiate(levelUpFXPrefab, KeyboardMovement.instance.transform.position, levelUpFXPrefab.transform.rotation);
        yield return new WaitForSeconds(2);
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
