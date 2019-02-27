using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	// reference to the Prefab 
	public GameObject managerPrefab;
	public GameObject theObject; 
	
	public void OnceTapped(){

		theObject.SetActive (false);
		Destroy(theObject);
		GameObject display = (GameObject)Instantiate(managerPrefab);


	}
//	public void DestroyGameObject()
//	{
//		Destroy(theObject);
//	}
}
