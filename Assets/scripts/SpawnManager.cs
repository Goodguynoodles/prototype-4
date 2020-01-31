using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject Powerupwave;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemyWave(waveNumber);
        Instantiate(Powerupwave, GenerateSpawnPosition(), Powerupwave.transform.rotation);
    }

    void spawnEnemyWave(int enemiesTospawn)
    {
        for (int i = 0; i < enemiesTospawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosx = Random.Range(-spawnRange, spawnRange);
        float spawnPosz = Random.Range(-spawnRange, spawnRange);
        Vector3 randompos = new Vector3(spawnPosx, 0, spawnPosz);
        return randompos;
               
    }
    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            spawnEnemyWave(waveNumber);
            Instantiate(Powerupwave, GenerateSpawnPosition(), Powerupwave.transform.rotation);
        }
    }
}
