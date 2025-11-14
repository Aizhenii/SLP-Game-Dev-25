using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    public float spawnInterval = 3f;
    private int enemyCounter;
    private int enemiesPerWave;
    public bool waveComplete;
    private Coroutine spawnRoutine;
    public static bool waveStart;
    


    // Start is called before the first frame update
    void Start()
    {
        enemiesPerWave = 2;
        spawnRoutine = StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab, enemiesPerWave));
        enemyCounter = 0;
        waveComplete = false;
    }


    void Update()
    {
        
        // this block of code ends the wave once the right amount of enemies has been spawned.
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 2)
        {
            waveStart = false;
        }
        
        if (enemyCounter == enemiesPerWave)
        {
            StopCoroutine(spawnRoutine);
            waveComplete = true;
        }

        // this block of code starts the next wave and increases the amount of enemies per wave.
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            
            waveComplete = false;
            enemyCounter = 0;
            enemiesPerWave += 5;
            waveStart = true;
            //enemyCounter = 0;
            //enemiesPerWave += 5;
            //spawnRoutine = StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab, enemiesPerWave));
            
            //waveComplete = false;
            //enemyCounter = 0;
            //enemiesPerWave += 5;
            //spawnRoutine = StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab, enemiesPerWave));
        }
        
        if (waveStart == true)
        {
            spawnRoutine = StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab, enemiesPerWave));
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
