using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControlScript : MonoBehaviour
{
    public static int spawnChecks;
    public static bool waveBegin;

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            spawnChecks = 0;
            waveBegin = false;
        }
        if (spawnChecks == 2)
        {
            waveBegin = true;
        }
    }
}
