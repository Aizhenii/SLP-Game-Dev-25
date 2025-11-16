using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    
    [SerializeField] private GameObject enemyPrefab2;

    public float spawnInterval = 3f;

    private int enemyCounter;
    private int enemiesPerWave;
    private bool allEnemiesSpawned;

    public int waveNum;
    public bool waveComplete;
    //public bool levelComplete;
    public bool gameWon;

    private Coroutine spawnRoutine;
    private Coroutine spawnRoutine2;


    // Start is called before the first frame update
    void Start()
    {
        enemiesPerWave = 2;
        waveNum = 1;

        StartWave();
    }


    void Update()
    {
        
        // this block of code ends the wave once the right amount of enemies has been spawned.
        //if (enemyCounter == enemiesPerWave)
        //{
        //    StopCoroutine(spawnRoutine);
        //    StopCoroutine(spawnRoutine2);
        //    waveComplete = true;
        //}

        //check when all enemies that were spawned are defeated
        if (allEnemiesSpawned && GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !waveComplete)
        {
            //lets logicManger know wave is cleared but not to start next wave yet
            waveComplete = true;
        }
        
        if (waveNum > 3 && waveComplete && !gameWon)
        {
            //levelComplete = true;
            gameWon = true;

        }

        // this block of code starts the next wave and increases the amount of enemies per wave.
        //if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        //{

        //    waveComplete = false;
        //    enemyCounter = 0;
        //    enemiesPerWave += 5;
        //    spawnRoutine = StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab, enemiesPerWave));
        //    spawnRoutine2 = StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab2, enemiesPerWave));
        //    waveNum++;
        //}
    }

    public void StartWave()
    {
        waveComplete = false;
        allEnemiesSpawned = false;
        enemyCounter = 0;

        spawnRoutine = StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab, enemiesPerWave));
        spawnRoutine2 = StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab2, enemiesPerWave));
    }
    
    public void StartNextWave()
    {
        //Moved the logic from update here
        enemiesPerWave += 5;
        waveNum++;

        //resume game
        Time.timeScale = 1f;

        StartWave();
    }
    
    // this spawns the amount of enemies per wave
    private IEnumerator spawnEnemy(float interval, GameObject enemy, int enemyNum)
    {
        
        while (enemyCounter < enemyNum)
        {
            GameObject newEnemy = Instantiate(enemy, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            enemyCounter++;
            yield return new WaitForSeconds(interval);
        }

        //no more enemies will spawn
        allEnemiesSpawned = true;
    }
}
