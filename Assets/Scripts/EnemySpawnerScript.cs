using System.Collections;
using System.Collections.Generic;
//using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    public float spawnInterval = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
