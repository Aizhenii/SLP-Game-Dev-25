using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using System;

public class WaveTextScript : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    
    public GameObject spawner;
    private EnemySpawnerScript waveNum;

    // Start is called before the first frame update
    void Start()
    {
        waveNum = spawner.GetComponent<EnemySpawnerScript>();
        waveText.text = waveNum.waveNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = "Wave #" + waveNum.waveNum.ToString();
    }
}
