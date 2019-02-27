using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsEarned : MonoBehaviour {

	// Use this for initialization
	string coins = "Coins";

	void Start () {
		if (PlayerPrefs.HasKey (coins)) {
			this.GetComponent<Text>().text = PlayerPrefs.GetFloat (coins).ToString();
		}
		else if (!PlayerPrefs.HasKey(coins))
			this.GetComponent<Text>().text = "0";
	}
	
	
}
