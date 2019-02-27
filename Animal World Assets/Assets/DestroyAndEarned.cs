using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAndEarned : MonoBehaviour {

    private string coins = "Coins";

	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject explosion;
    //public GameObject playerExplosion;
    public int scoreValue;
    public float coinsEarned;
	private Done_GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" || other.tag == "Enemy")
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

//		if (other.tag == "Player")
//		{
//			//This code instantiate an explotion, it requires a gameObject, with position and rotation. 
//			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
//			gameController.GameOver();
//
//		}
		gameController.AddScore(scoreValue);
        PlayerPrefs.SetFloat(coins, PlayerPrefs.GetFloat(coins) + 1); // add coins
        PlayerPrefs.SetInt("CoinsEarned", PlayerPrefs.GetInt("CoinsEarned") + 1); // the coins earned
        // This Player 
        //Destroy (other.gameObject);

        //This Game Object  
		Destroy (gameObject);


	}

}
