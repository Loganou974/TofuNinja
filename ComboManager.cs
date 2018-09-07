using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
 {
	private int comboMultiplier = 1;
	public int ComboMultiplier
	{
		get { return comboMultiplier;}
		set {comboMultiplier = value; comboMulTxt.text = "x" + comboMultiplier.ToString();}
	}
    //public int comboPalier = 3; // how much you'll hav to kill to move up the comboMultiplier

    public float comboTimer = 10 ; // seconds left before comboMul go down

	public Image comboSlider;
	public Text comboMulTxt;

	public float timer;

	void Awake()
	{
		comboSlider = GetComponent<Image>();
		ResetTimer();
	}


	void ResetTimer()
	{
		timer = comboTimer;
	}
	void LateUpdate()
	{
		
		// check if timer lvl up or down the comboMultiplier
		if(timer > comboTimer ) ChangeComboMul("+");
		if(comboSlider.fillAmount == 0) ChangeComboMul("-");

	
		if(comboMultiplier > 1 ) timer -= (Time.deltaTime * (comboMultiplier/2));
		// affiche l'info sur le slider
		comboSlider.fillAmount =  (timer/comboTimer);

	
	}

	public void AddComboPoints(int points)
	{
		timer += points;
		iTween.PunchScale(gameObject, new Vector3(1,1,1), 0.5f);
	}

	void ChangeComboMul(string progression)
	{
		if(progression == "+")
		{
			Debug.Log("Level up !");
			ComboMultiplier++;
			ResetTimer();
		
		}

		else if(progression == "-")
		{
			Debug.Log("Level down !");
			ComboMultiplier--;
			ResetTimer();
		}

		else
		Debug.LogError("Progression Sign ( + or -) is missing in : " + this.name);
	}

}
