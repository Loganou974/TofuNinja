using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDMoveFromTo : MonoBehaviour
 {

	void Start()
	{
	
	}
	// Use this for initialization
	public void Show(float showTime)
	{
		iTween.MoveFrom(gameObject, iTween.Hash("position", new Vector3(-250,0,0), "time", 1.0f, "islocal", true));
		StartCoroutine(Waiting(showTime));
		
	}

	IEnumerator Waiting(float showTime){
		yield return new WaitForSeconds(showTime);
		Hide();
	}

	void Hide()
	{
		iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(250,0,0), "time", 1.0f, "islocal", true));

	}
}
