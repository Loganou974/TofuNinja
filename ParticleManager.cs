using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour 
{
	public ParticleSystem[] particles;
	public static ParticleManager instance;

	private void Awake() {
		instance = GetComponent<ParticleManager>();

	}

	public void LaunchFX(int index, Transform target)
	{
		GameObject newGO = Instantiate(particles[index].gameObject, target.position, Quaternion.identity ) as GameObject;
		//newGO.transform.parent = target;
		Destroy(newGO, particles[index].main.startLifetime.constant);
	}

	public void LaunchFX(int index, Vector3 position)
	{
		GameObject newGO = Instantiate(particles[index].gameObject, transform.position, Quaternion.identity ) as GameObject;
		newGO.transform.position = position;
		//newGO.transform.parent = target;
		Destroy(newGO, particles[index].main.startLifetime.constant);		// AUTO DESTROY PARTICLE
		
	}
}
