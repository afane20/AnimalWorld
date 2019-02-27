using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsState : MonoBehaviour {

	public static bool learnSceneCanvasOFF = true;
	//public GameObject learnCanvas;
	public GameObject learnCanvas;

	public void SetLearnSceneCanvasFALSE(){
		learnSceneCanvasOFF = false;
		Debug.Log ("CANVAS OFFF ::::: " + learnSceneCanvasOFF.ToString());
		learnCanvas = GameObject.Find ("Canvas-LearnScene");
		learnCanvas.SetActive (true);
	}
}
