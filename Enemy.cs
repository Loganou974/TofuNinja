using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	public int phaseSpeed = 1;
	int phase; // 0 = passive ; 1 = passive; 2 = aggro
	// Use this for initialization
	
	GameObject player;
	public Vector3 myDir;
	Rigidbody rb;
	public float moveForce = 10.0f;

	private bool gameOver;

private void Awake() {
	rb = GetComponent<Rigidbody>();
}
	void Start () 
	{
		player = GameManager.instance.player;
		
		BeginPhase();
	}
	
	void BeginPhase()
	{
		StartCoroutine(Phases(1));
	}

	IEnumerator Phases(float speed)
	{
		Wait();
		yield return new WaitForSeconds(Random.Range(0.5f, 1.5f) / speed);
		Aiming();
	   yield return new WaitForSeconds(1 / speed);

		Attack(myDir);
		
		while(rb.velocity.sqrMagnitude > 0.1f)
		{
			print("waiting end of attack");
        	yield return null;
        } 

			yield return new WaitForSeconds(Random.Range(0.5f, 0.5f) / speed);

		BeginPhase();
	}

	void Wait()
	{
		
		ChangeMatColor(gameObject, Color.green);
		phase = 0;
		
	}

	Vector3 Aiming()
	{
			
		ChangeMatColor(gameObject, Color.yellow);
		phase = 1;
		myDir = player.transform.position - transform.position;



		return myDir;
	}

	void Attack(Vector3 dir)
	{
		//print("attacking");
		ChangeMatColor(gameObject, Color.red);
		phase = 2;
		dir.Normalize();
		rb.AddForce(dir * Random.Range(moveForce - (moveForce/2), moveForce));
	}

	private void OnTriggerEnter(Collider other)
	 {
		
		if(other.gameObject.CompareTag("Player"))
		{
			switch(phase)
			{
				case 0:
				Dead();
				break;

				case 1:
				Dead();
				break;

				case 2:
				GameManager.instance.GameOver();
				break;
			}
		}
	}

	void ChangeMatColor(GameObject target, Color _color) 
	{
		Material mat = target.GetComponent<MeshRenderer>().material;
		mat.color = _color;
		target.GetComponent<MeshRenderer>().material = mat;
	}

	public void Dead()
	{
		GameManager.instance.Score++;
		ParticleManager.instance.LaunchFX(1, transform);
		//ParticleManager.instance.LaunchFX(0,new Vector3(0,0,0));
		GameManager.instance.comboScript.AddComboPoints(100);
		Destroy(gameObject);
	}

	public void GameOver()
	{
		StopAllCoroutines();
		gameOver = true;
	}
}
