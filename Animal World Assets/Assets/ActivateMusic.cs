using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("Music") == 0)
			this.GetComponent<AudioSource> ().enabled = false;
		else
			// Do nothing
			return;
	}
	

}
