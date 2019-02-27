using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TirarScene : MonoBehaviour {

    public string sceneToThrow;
    // Use this for initialization

    void Awake()
    {
        Utilities.main = false; // this helps control the reward of the user
    }
    void Start () {
        SceneManager.UnloadSceneAsync(sceneToThrow);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
