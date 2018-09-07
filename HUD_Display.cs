using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Display : MonoBehaviour 
{
	public float fadeTime = 5;
	CanvasGroup render;
	// Use this for initialization
	void Awake ()
	{
		render = GetComponent<CanvasGroup>();
	}

	public IEnumerator Show(float _fadeTime = -1)
     {
		 if(_fadeTime == -1) _fadeTime = fadeTime; // default value if not defined
         float elapsedTime = 0.0f;
         while (elapsedTime < _fadeTime)
         {
             yield return new WaitForEndOfFrame();
             elapsedTime += Time.deltaTime;
			 render.alpha = elapsedTime/_fadeTime ;
         }
     }

public IEnumerator Hide(float _fadeTime = -1)
     {
		 render.alpha = 0;

         yield return null;
     }

}
