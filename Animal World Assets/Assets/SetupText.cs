using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupText : MonoBehaviour {

    [SerializeField]GameObject confirm, yes;
    [SerializeField]GameObject ENGconfirm, ENGyes;


	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("Lan") == 1) // set to Spanish 
        {
            confirm.SetActive(true); 
            yes.SetActive(true);

            ENGconfirm.SetActive(false);
            ENGyes.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Lan") == 2) {
            ENGconfirm.SetActive(true);
            ENGyes.SetActive(true);

            confirm.SetActive(false);
            yes.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
