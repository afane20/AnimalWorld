using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateButtonWithScore : MonoBehaviour {

	[SerializeField]
	private CounterManager counterManager;

	public void ActiveButtonORnot(){
		if (counterManager.GetPointsEarnedByPlayer () <= 0 || (counterManager.playerFAILED == true) ){
			this.GetComponent<Button> ().interactable = false;
		}
	}
}
