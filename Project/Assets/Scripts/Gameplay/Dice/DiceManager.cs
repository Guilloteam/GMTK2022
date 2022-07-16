using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public static DiceManager instance;
    public DiceBuilder dicePrefab;
    public DiceList startConfig;
    private List<DiceBuilder> spawnedDices = new List<DiceBuilder>();
    public Transform[] diceSpawnPoints;

    [SerializeField]
    private DiceList currentConfig;

    void Awake()
    {
        instance = this;
        currentConfig = Instantiate(startConfig);
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
                spawnedDices.Add(dice);
            }
            else dice = spawnedDices[i];
            dice.diceConfig = currentConfig.dices[i];
            dice.UpdateFaces();
        }
    }

    void Update()
    {
        
    }
}
