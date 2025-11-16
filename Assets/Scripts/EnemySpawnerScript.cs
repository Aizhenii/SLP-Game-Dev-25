using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyPrefab2;

    public float spawnInterval = 3f;
    private int enemyCounter;
    private int enemiesPerWave;
    public bool waveComplete;
    private Coroutine spawnRoutine;
    private Coroutine spawnRoutine2;
    public int waveNum;
    
    


    // Start is called before the first frame update
    void Start()
    {
        enemiesPerWave = 2;
        spawnRoutine = StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab, enemiesPerWave));
        spawnRoutine2 = StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab2, enemiesPerWave));
        enemyCounter = 0;
        waveComplete = false;
        waveNum = 1;
    }


    void Update()
    {
        
        // this block of code ends the wave once the right amount of enemies has been spawned.
        if (enemyCounter == enemiesPerWave)
        {
            StopCoroutine(spawnRoutine);
            StopCoroutine(spawnRoutine2);
            waveComplete = true;
        }

        // this block of code starts the next wave and increases the amount of enemies per wave.
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            
            waveComplete = false;
            enemyCounter = 0;
            enemiesPerWave += 5;
            spawnRoutine = StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab, enemiesPerWave));
            spawnRoutine2 = StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab2, enemiesPerWave));
            waveNum++;
        }
        
        
        
        
    }
    
    
    // this spawns the amount of enemies per wave
    private IEnumerator spawnEnemy(float interval, GameObject enemy, int enemyNum)
    {
        
        while (enemyCounter<enemyNum)
        {
            GameObject newEnemy = Instantiate(enemy, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            enemyCounter++;
            yield return new WaitForSeconds(interval);
        }

    }
}
