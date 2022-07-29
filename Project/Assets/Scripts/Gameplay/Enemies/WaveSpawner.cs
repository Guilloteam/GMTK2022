using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
    public Transform[] enemies;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] easyWaves;
    public Wave[] mediumWaves;
    public Wave[] hardWaves;
    public AnimationCurve easyWaveCurve;
    public AnimationCurve mediumWaveCurve;
    public AnimationCurve hardWaveCurve;
    public float spawnRadius = 30;
    public float startSpawnDelay = 5;
    public float endSpawnDelay = 2;
    public float minSpawnDelay = 1;
    public float endSpawnTime = 60;
    public float spawnTime = 10;
    public Transform spawnParent;
    private float difficultyTime = 0;
    public float difficultyDuration = 120;
    public float startMaxWaveCount = 1;
    public float endMaxWaveCount = 5;
    public List<Wave> spawnedWaves = new List<Wave>();

    void Start()
    {
        
    }

    void Update()
    {
        difficultyTime += Time.deltaTime;
        float difficultyRatio = difficultyTime / difficultyDuration;
        float spawnTimeRatio = Mathf.Max(0, (difficultyTime - difficultyDuration) / endSpawnTime);
        float spawnDelay = startSpawnDelay + Mathf.Max(minSpawnDelay, (endSpawnDelay - startSpawnDelay) * spawnTimeRatio);
        spawnTime -= Time.deltaTime;
        for(int i=spawnedWaves.Count-1; i>=0; i--)
        {
            bool enemyAlive = false;
            for(int j=0; j<spawnedWaves[i].enemies.Length; j++)
            {
                if(spawnedWaves[i].enemies[j] != null)
                {
                    if(spawnedWaves[i].enemies[j].childCount == 0)
                    {
                        Destroy(spawnedWaves[i].enemies[j].gameObject);
                        break;
                    }

                    enemyAlive = true;
                    break;
                }
            }
            if(!enemyAlive)
            {
                spawnedWaves.RemoveAt(i);
            }
        }
        float maxWaveCount = Mathf.Lerp(startMaxWaveCount, endMaxWaveCount, difficultyRatio);
        if(spawnTime < 0 && spawnedWaves.Count + 1 < maxWaveCount)
        {
            spawnTime += spawnDelay;
            float easyWeight = easyWaveCurve.Evaluate(difficultyRatio);
            float mediumWeight = mediumWaveCurve.Evaluate(difficultyRatio);
            float hardWeight = hardWaveCurve.Evaluate(difficultyRatio);
            float easyProbability = easyWeight / (easyWeight + mediumWeight + hardWeight);
            float mediumProbability = easyProbability + mediumWeight / (easyWeight + mediumWeight + hardWeight);
            float hardProbability = mediumProbability + mediumWeight / (easyWeight + mediumWeight + hardWeight);
            float randomValue = Random.value;
            Wave[] waves = null;
            if(randomValue < easyProbability)
            {
                waves = easyWaves;
            }
            else if(randomValue < mediumProbability)
            {
                waves = mediumWaves;
            }
            else
            {
                waves = hardWaves;
            }
            int waveIndex = Random.Range(0, waves.Length);
            List<Transform> availableSpawnPoints = new List<Transform>();
            
            Wave wave = new Wave();
            wave.enemies = new Transform[waves[waveIndex].enemies.Length];
            for(int i=0; i<waves[waveIndex].enemies.Length; i++)
            {
                Transform[] enemies = waves[waveIndex].enemies;
                int spawnPointIndex = Random.Range(0, availableSpawnPoints.Count);
                float spawnAngle = Random.Range(0, 360);
                Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(spawnAngle) * spawnRadius, 0, Mathf.Sin(spawnAngle) * spawnRadius);
                Transform spawnedEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPosition, Quaternion.identity, spawnParent);
                wave.enemies[i] = spawnedEnemy;
            }
            spawnedWaves.Add(wave);
        }
    }
}
