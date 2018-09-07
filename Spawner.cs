using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
	public GameObject objToSpawn;
	float spawnRate = 1.0f;
	public bool activated = false;
	public Transform[] locations;

	// Use this for initialization
	void Start () 
	{
		if(activated)
		Spawn(objToSpawn, locations[0]);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Spawn(GameObject newObj, Transform location)
	{
		GameObject newSpawn = Instantiate(newObj, location.position, Quaternion.identity) as GameObject;
	}
}
