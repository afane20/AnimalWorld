using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDificil : MonoBehaviour {

	[SerializeField]
	private int cardState;
	// If the card State is "0" the card is Back, 1 Card has been flipped, 2 card has been matched
	[SerializeField]
	private int cardValue;
	[SerializeField]
	private bool initialized;

	private Sprite cardBack;
	private Sprite cardFace;

    [SerializeField]
	private GameObject appManager;
	private bool hasBeenMatched = false;

	void Start(){
		//appManager = GameObject.FindGameObjectWithTag ("AppManagerDificil");

	}

	public void setupGraphics(){
		if (cardState == 0)
			GetComponent<Image> ().sprite = getCardBack ();

		if (cardState == 1)
			GetComponent<Image> ().sprite = getCardFace ();

		if (cardState == 2)
			GetComponent<Image> ().sprite = getCardFace ();

	}

	public void flipCard(){
		Debug.Log ("FLipped");
		//		if (cardState == 0) 
		//			cardState = 1;
		//		else if (cardState == 1)
		//			cardState = 0;

		// When I click on the card, flip it 
		if (cardState == 0) {
			GetComponent<Image> ().sprite = getCardFace ();
			cardState = 1;

		}

		//		else if (cardState == 1) {
		//			GetComponent<Image> ().sprite = getCardBack ();
		//			cardState = 0;
		//		}

		appManager.GetComponent<AppManagerDificil> ().checkCards ();

	}

	//setters and getters
	public bool getHasBeenMatched(){return hasBeenMatched;}
	public void setHasBeenMatched(bool hasBeen){hasBeenMatched = hasBeen;}

	public int getCardValue(){return cardValue;}
	public void setCardValue (int value){ cardValue = value;}

	public int getCardState(){return cardState;}
	public void setCardState (int value){ cardState = value;}

	public bool getInitialized(){ return initialized;}
	public void setInitialized(bool init){ initialized = init; }

	public Sprite getCardBack(){return cardBack;}
	public void setCardBack(Sprite img){ cardBack = img;}

	public Sprite getCardFace(){return cardFace;}
	public void setCardFace(Sprite img){ cardFace = img;}


}
