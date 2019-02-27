using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerActivate : MonoBehaviour {

	public void ENABLEAudioListener(){
		this.GetComponent<AudioListener> ().enabled = true;
	}

	public void DISABLEAudioListener(){
		this.GetComponent<AudioListener> ().enabled = false;
	}
}
