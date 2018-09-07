using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour 
{
    public GameObject[] enemyPrefabs;
    void Start()
    {
        GameObject[] curEnemies = {enemyPrefabs[0],enemyPrefabs[0],enemyPrefabs[0]};
        
        

        Wave curWave = gameObject.AddComponent<Wave>();
        curWave = new Wave(1,curEnemies,1);
    }
	
}
