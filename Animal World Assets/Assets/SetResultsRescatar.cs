using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetResultsRescatar : MonoBehaviour {

    [SerializeField] Text dogsTxt, coinsTxt, beesTxt;
	// Use this for initialization
	void Start () {
        dogsTxt.text = PlayerPrefs.GetInt("Dogs").ToString();
        coinsTxt.text = PlayerPrefs.GetInt("CoinsEarned").ToString();
        beesTxt.text = PlayerPrefs.GetInt("Bees").ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
