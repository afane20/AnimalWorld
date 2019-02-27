using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class appManager : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] string sceneName;
	public GameObject cancelInputsPanel; 
	public GameObject playAgainPanel; 
	public float time;
	public float possiblePoints;
	public ModalManager modalManager; 
	public Text tellerText;
	public bool isThisEasyMode; 

	[Header("Text - PLay Again Modal Window")]
	public Text levelName;
	public string actualLevelName;
	public Text scorePoints;
	public Text bestScore;
	public Text coinsEarned; 

	private float pointsPerMatchedAnimal; // the amount of points per animal discovered
	private float pointsEarnedByPlayer; 

	[Header("Text - Main Scene TEXT")]
	public Text displayScore;
	[Header("Sprite Elements")]
	public Sprite[] CardFace;
	public Sprite CardBack;

	[Header("UI Elements")]
	public Text numberOfMatches;
	public GameObject[] Cards;

	//Star Animatiors 
	private Animator playAgain;
	private Animator playAgain2;
	private Animator playAgain3;

	private int counter;

	// used to count the rotations of the score In the display
	int countRotations = 0;

	private bool Initialized = false;

	//Number of Matches, number of Cards
	int numberOfCards;
	int matches; 
	public int totalMatches;
	public float levelNumber; 

	// used for animating the display 
	int numberGenerated;
	bool rotateNumber = false;

	//PlayerPrefs
	string KEYcoins = "Coins";
	string KEYscore = "Score";
	string KEYbestScore = "BestScore";
	int bestScoreValue;

	// Update is called once per frame

	void Update () {


	}

	void Awake(){
		levelName.text = actualLevelName;
		//playAgainPanel.transform.GetChild (0).GetChild (1).GetChild (0).GetComponent<Text>().text = actualLevelName;
		tellerText.text = actualLevelName;
	}

	/// <summary>
	/// this function runs 50 times a second, it is used to make the numbers look
	/// random. to create an animation of numerical transformation
	/// </summary>
	void FixedUpdate(){
		if (rotateNumber) {
			numberGenerated = Random.Range (10, 100);
			scorePoints.text = "Score:   " + numberGenerated.ToString ();
			coinsEarned.text = "             " + numberGenerated.ToString ();
			bestScore.text =   "Best:    " + numberGenerated.ToString ();
			countRotations++;

			if (countRotations == 60) {
				rotateNumber = false;
				scorePoints.text = "Score:   " + pointsEarnedByPlayer;
				coinsEarned.text = "             " + PlayerPrefs.GetFloat(KEYcoins);
				bestScore.text = "Best:    " + PlayerPrefs.GetFloat (KEYbestScore);

			}
		}
			
	}



	void Start(){
        PlayerPrefs.SetString(Utilities.KEYEasyContinue, sceneName);

		KEYbestScore = KEYbestScore + levelNumber.ToString();
		Debug.Log (KEYbestScore);
		playAgain = playAgainPanel.transform.GetChild (0).GetChild (9).GetComponent<Animator> ();
		playAgain2 = playAgainPanel.transform.GetChild (0).GetChild (10).GetComponent<Animator> ();
		playAgain3 = playAgainPanel.transform.GetChild (0).GetChild (11).GetComponent<Animator> ();

		numberOfCards = Cards.Length;
		matches = (numberOfCards / 2);
		totalMatches = matches;
		//this.GetComponent<CounterManager> ().calculatePoints (totalMatches);

		//Debug.Log ("Number Of Cards: " + numberOfCards);
		//Debug.Log ("Number Of Matches: " + numberOfMatches);
		// assign the number of matches to the TEXT Object in the display
		numberOfMatches.text = "Matches: " + totalMatches;
		for (int i = 0; i < matches; i++) {
			Cards [i].GetComponent<Image>().sprite = CardBack;
			Cards [i].GetComponent<Card> ().setCardValue(i);
			Cards [i].GetComponent<Card> ().setCardFace(CardFace [i]); // save the face of the card with the respective object 
			Cards [i].GetComponent<Card>().setCardBack(CardBack);
			counter++;
		}
		Debug.Log (counter);
		for (int i = 0; i < matches; i++) {
			Cards [i + matches].GetComponent<Image>().sprite = CardBack;
			Cards [i + matches].GetComponent<Card> ().setCardValue(i);
			Cards [i + matches].GetComponent<Card> ().setCardFace(CardFace [i]); // save the Face of the card with the respective object 
			Cards [i + matches].GetComponent<Card> ().setCardBack(CardBack); // save the Back of the card with the respective object 


		}

		calculatePoints ();
		Initialized = true;
		// Counter!!!!

	}

	public void calculatePoints(){
		pointsPerMatchedAnimal = (possiblePoints / matches);
		Debug.Log ("CALCULATE POINTS ======== " + pointsPerMatchedAnimal);

	}

	public void addPoints(){
		pointsEarnedByPlayer += pointsPerMatchedAnimal;
		displayScore.text = "Score:     " + pointsEarnedByPlayer;
	}

	public void losePoints(){
		pointsEarnedByPlayer -= (pointsPerMatchedAnimal / 2);
		displayScore.text = "Score:     " + pointsEarnedByPlayer;
	}
	/**************
	* Preparing the number of cards, assingning a value to the CardObject
	* Counter: counts the cards, continue at the half, assign the same values to the rest of the cards
	**************/
	void initializeCards (){


	}

	public Sprite getCardBack(){
		return CardBack;
	}

	public Sprite getCardFront(int i){
		return CardFace [i];
	}

	public void checkCards(){
		Debug.Log ("CheckCards");
		// Check all the cards. if a card is found at "I" where the state is "1" then save the location of the card
		List<int> cardLocation = new List<int> ();

		for (int i = 0; i < Cards.Length; i++) {
			if (Cards [i].GetComponent<Card> ().getCardState() == 1 && (Cards[i].GetComponent<Card>().getHasBeenMatched() == false)) {
				cardLocation.Add(i);
				Debug.Log ("Adding Card Location" + i);
				if (cardLocation.Count == 2) {
					Debug.Log ("Getrting out of Loop");
					break;
				}
			}

		}

		if (cardLocation.Count == 2)
			cardComparison (cardLocation);
	}

	/// <summary>
	/// Compares the card to check if they have been match or NOT
	/// </summary>
	void cardComparison(List<int> cardLocation){

		Debug.Log ("Comparison Functon!!!");

		int state = 0;

		// Card DID Match
		if (Cards [cardLocation [0]].GetComponent<Card> ().getCardValue () == Cards [cardLocation [1]].GetComponent<Card> ().getCardValue ()) {
			state = 2;
			totalMatches--;
			Debug.Log ("CArds have been matched!!!");
			//if (!isThisEasyMode)
			//this.GetComponent<CounterManager>().addPoints(); // New code
			addPoints();

			Cards [cardLocation [0]].GetComponent<Card> ().setHasBeenMatched(true);
			Cards [cardLocation [1]].GetComponent<Card> ().setHasBeenMatched(true);

			this.transform.GetChild (0).GetComponent<AudioSource> ().Play ();

			//if (totalMatches == 0) 
			//Change the Scene, return to main menu or to next level.

			// CARDS DID NOT MATCH 
		} else {
			//this.GetComponent<CounterManager>().losePoints();
			losePoints();
			Debug.Log ("Cards didnt Match");
			cancelAllInput (true);
			this.transform.GetChild (1).GetComponent<AudioSource> ().Play ();
			StartCoroutine (pause(cardLocation));
		}
		// CARD DID MATCH
		Debug.Log ("Total Matches---- " + totalMatches);
		numberOfMatches.text = "Matches: " + totalMatches;

		// If the Player Won the Game by Matching EVERYTIHNG
		if (totalMatches == 0) {
            Invoke("youWonTheGame", 2);
			//modalManager.openResultsWindow (playAgainPanel);
			////this.GetComponent<CounterManager> ().starsAnimation (true);
			//starsAnimation();
			//savePlayerData ();
			//rotateNumber = true;
		} 

//		if (totalMatches > 0 ) {
//			this.GetComponent<CounterManager> ().SetLostRotateNumber (true);
//		}

	}

    void youWonTheGame()
    {
        modalManager.openResultsWindow (playAgainPanel);
        //this.GetComponent<CounterManager> ().starsAnimation (true);
        starsAnimation();
        savePlayerData ();
        rotateNumber = true;
    }

	public void starsAnimation(){
		if (pointsEarnedByPlayer < (possiblePoints / 2) && pointsEarnedByPlayer >= 25)
			playAgain.Play ("EstrellaIzquierda-animada");

		if (pointsEarnedByPlayer >= (possiblePoints / 2)) {
			playAgain.Play ("EstrellaIzquierda-animada");
			playAgain2.Play ("Estrella2-Animada");

		}
		if (pointsEarnedByPlayer == possiblePoints) {
			playAgain.Play ("EstrellaIzquierda-animada");
			playAgain2.Play ("Estrella2-Animada");
			playAgain3.Play ("Estrella3-Animada");
		}
	}

	IEnumerator pause(List<int> cardLocation){
		yield return new WaitForSeconds(time);

		Cards [cardLocation [0]].GetComponent<Card> ().setCardState (0);
		Cards [cardLocation [1]].GetComponent<Card> ().setCardState (0);

		Cards [cardLocation [0]].GetComponent<Card> ().setupGraphics();
		Cards [cardLocation [1]].GetComponent<Card> ().setupGraphics();
		cancelAllInput (false);
	}

	void cancelAllInput(bool state){
		cancelInputsPanel.SetActive (state);

	}


	public void savePlayerData(){

		//Setup the coins earned by the Player in this level plus the ones the Player have already
		float currentCoins;
		if (PlayerPrefs.HasKey (KEYcoins)) {
			currentCoins = PlayerPrefs.GetFloat (KEYcoins);
			currentCoins = currentCoins + matches;
			PlayerPrefs.SetFloat (KEYcoins, currentCoins);

		} else if (!PlayerPrefs.HasKey (KEYcoins)) {
			PlayerPrefs.SetFloat (KEYcoins, matches);
		}

		// Setup the Score 
		PlayerPrefs.SetFloat (KEYscore, pointsEarnedByPlayer);

		// setup the best score per level
		// If this KEY has been declared already 
		if (PlayerPrefs.HasKey (KEYbestScore)) {
			
			if (PlayerPrefs.GetFloat (KEYbestScore) > pointsEarnedByPlayer) {
				return; 
			} else if (PlayerPrefs.GetFloat (KEYbestScore) < pointsEarnedByPlayer)
				PlayerPrefs.SetFloat (KEYbestScore, pointsEarnedByPlayer);


		} else if (!PlayerPrefs.HasKey (KEYbestScore)) {
			PlayerPrefs.SetFloat (KEYbestScore, pointsEarnedByPlayer);
		}


	} 

}
