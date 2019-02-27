using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour {

  

	public void LoadSceneWithName(string sceneName){

		SceneManager.LoadScene (sceneName);

	}

    // This will allow for the main Scene To Remain active
	public void LoadSceneWithNameADDITIVE(string sceneName){

        SceneManager.LoadScene (sceneName, LoadSceneMode.Additive);
        //Utilities.sceneNameToThrow
	}

    // Load Scene REPEAT Button
    public void REPEATButtonLoadScene(string sceneName){
        Utilities.sceneNameToLoad = sceneName;
        Utilities.sceneNameToThrow = sceneName;

        Utilities.LoadSceneWithName(Utilities.sceneNameToLoad);
    }

	// Set the game object to active or Disactive
	public void SetGameObjectActivityFALSE(GameObject theObject){
		theObject.SetActive (false);

	}

	public void SetGameObjectActivityTRUE(GameObject theObject){
		theObject.SetActive (true);

	}

    public void LoadContinueEasy(){
        SceneManager.LoadScene(PlayerPrefs.GetString(Utilities.KEYEasyContinue), LoadSceneMode.Additive);

    }

    public void LoadContinueMedioum()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString(Utilities.KEYMedioumContinue), LoadSceneMode.Additive);

    }

    public void LoadContinueHard()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString(Utilities.KEYHardContinue), LoadSceneMode.Additive);

    }

	//Activate/Deactivate the Aprencer Canvas

}
