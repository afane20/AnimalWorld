using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateButtonWithScoreDificil : MonoBehaviour {

	// Reference to Counter Manager 
	[SerializeField]
	private CounterManagerDificil counterManagerDificil;

	void Start(){
		if (counterManagerDificil.GetPointsEarnedByPlayer () <= 0)
			this.GetComponent<Button> ().interactable = false;
			
		if ((counterManagerDificil.playerFAILED == true))
			this.GetComponent<Button> ().interactable = false;

	}

	//To avoid reconstruction of the object leave the function alone!!!!
	public void ActiveButtonORnot(){
//		if (counterManagerDificil.GetPointsEarnedByPlayer () <= 0) {
//			this.GetComponent<Button> ().interactable = false;
//		} 
//
//		if ((counterManagerDificil.playerFAILED == true)) {
//
//			this.GetComponent<Button> ().interactable = false;
//
//		}
	}
}
