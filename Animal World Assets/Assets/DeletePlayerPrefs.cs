using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeletePlayerPrefs : MonoBehaviour {

	public void EraseEVERYTHING(){

		PlayerPrefs.DeleteAll ();
        // todo restart the app 
        SceneManager.LoadScene(0);
	}
}
