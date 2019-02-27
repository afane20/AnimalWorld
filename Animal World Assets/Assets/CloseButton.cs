using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour {

	public GameObject modalManager;

	public void OnClick(){
		modalManager.transform.GetChild (2).GetComponent<AudioSource> ().Play ();

	}
}
