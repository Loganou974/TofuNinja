using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
	public Wave(int _id, GameObject[] _enemies, float _waitTime )
	{
		ID = _id;
		enemies = _enemies;
		waitTime = _waitTime;
	}
	int ID;
	float waitTime;
	GameObject[] enemies;

	bool isFinished;
	int enemyCount;
	void Start()
	{
		Spawn();
		
	}

	void Spawn()
	{
		for(int i = 0 ; i < enemies.Length ; i++)
		{
			GameObject curEnemy = new GameObject();
			curEnemy.name = "Enemy" + i;
			
		}
		
	}
}
