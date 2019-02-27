using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	// this 2 are references to the PREFAB of the OBJECTS, the canvas of Aprender, and SceneManager
	public GameObject canvas;
	//public GameObject sceneManager;

	//reference to the canvas 
	//public GameObject learnSceneCanvas;
	// Use this for initialization
	void Awake () {
		if (DontDestroy.instance == null)

			//Instantiate gameManager prefab
			Instantiate(canvas);
		
			//Instantiate (sceneManager);
	}

	void Start(){
	//	GameObject.FindGameObjectWithTag ("LearnCanvas").SetActive(true);


	}

}
