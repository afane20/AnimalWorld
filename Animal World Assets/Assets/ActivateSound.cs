using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSound : MonoBehaviour {

	public void OnClick(){
		//if PlayerPref Sound = 1, yes, 0 means NO
		if (PlayerPrefs.GetInt ("Sound") == 1)
			this.GetComponent<AudioSource> ().Play ();
		else
			return;
	}


	void Start(){
		if (PlayerPrefs.GetInt ("Sound") == 0)
			this.GetComponent<AudioSource> ().enabled = false;
		else
			return;
	}
}
