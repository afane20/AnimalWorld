using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManagerDificil : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] string sceneName;
	public GameObject cancelInputsPanel; 
	public GameObject playAgainPanel; 
	public float time;
	//public float possiblePoints;
	public ModalManager modalManager; 
	public Text tellerText;
	public bool isThisEasyMode; 

	[Header("Text - PLay Again Modal Window")]
	public Text levelName;
	public string actualLevelName;
	public Text scorePoints;
	public Text bestScore;
	public Text coinsEarned; 


	[Header("Sprite Elements")]
	public Sprite[] CardFace;
	public Sprite CardBack;

	[Header("UI Elements")]
	public GameObject[] Cards;
	public Text numberOfMatches;

	private int counter;

	// used to count the rotations of the score In the display
	int countRotations = 0;

	private bool Initialized = false;

	//Number of Matches, number of Cards
	int numberOfCards;
	int matches; 
	public int totalMatches;

	// used for animating the display 
	int numberGenerated;
	bool rotateNumber = false;

	//PLayerPrefs
	private string KEYcoins = "Coins";
	private string KEYscore = "Score";
	private string KEYbestScore = "BestScore";
	public float levelNumber;

	public string getKEYcoins {
		get {
			return KEYcoins;
		}
	}

	public string getKEYbestScore {
		get {
			return KEYbestScore;
		}
	}

	// Update is called once per frame



	void Update () {


	}

	void Awake(){
		levelName.text = actualLevelName;
		tellerText.text = actualLevelName;
	}

	// This array will Randomly select a different combination of cards




	/// <summary>
	/// this function runs 50 times a second, it is used to make the numbers look
	/// random. to create an animation of numerical transformation
	/// </summary>
	void FixedUpdate(){
		if (rotateNumber) {
			numberGenerated = Random.Range (10, 100);
			scorePoints.text = "Score:   " + numberGenerated.ToString ();
			coinsEarned.text = "             " + numberGenerated.ToString ();
			bestScore.text = "Best:    " + numberGenerated.ToString ();
			countRotations++;

			if (countRotations == 60) {
				rotateNumber = false;
				scorePoints.text = "Score:   " + this.GetComponent<CounterManagerDificil>().GetPointsEarnedByPlayer();
				coinsEarned.text = "             " + PlayerPrefs.GetFloat(KEYcoins);
				bestScore.text = "Best:    " + PlayerPrefs.GetFloat (KEYbestScore);

			}
		}

	}
	int randomNumber;
	void Start(){
		KEYbestScore = KEYbestScore + levelNumber.ToString();
        PlayerPrefs.SetString(Utilities.KEYHardContinue, sceneName);

		numberOfCards = Cards.Length;
		matches = (numberOfCards / 2);
		totalMatches = matches;
		this.GetComponent<CounterManagerDificil> ().calculatePoints (totalMatches);
        randomNumber = Random.Range(0, 5);

		//Debug.Log ("Number Of Cards: " + numberOfCards);
		//Debug.Log ("Number Of Matches: " + numberOfMatches);
		// assign the number of matches to the TEXT Object in the display
		numberOfMatches.text = "Matches: " + totalMatches;

		switch (matches) {
		case 3:
			populateContent(Matches3MixTheCards);
			break;
		case 4:
			populateContent (Matches4MixTheCards);
			break;
		case 5:
			populateContent (Matches5MixTheCards);
			break;
		case 6:
			populateContent (Matches6MixTheCards);
			break;
		case 7:
			populateContent (Matches7MixTheCards);
			break;
		case 8:
			populateContent (Matches8MixTheCards);
			break;
		case 9:
			populateContent (Matches9MixTheCards);
			break;
            default:
                populateContent();
                break;
		}

		Initialized = true;
		// Counter!!!!

	}
    // Default settings to populate the content without integrating a random generator
    public void populateContent()
    {
        for (int i = 0; i < matches; i++)
        {
            Cards[i].GetComponent<Image>().sprite = CardBack;
            Cards[i].GetComponent<CardDificil>().setCardValue(i);
            Cards[i].GetComponent<CardDificil>().setCardFace(CardFace[i]); // save the face of the card with the respective object 
            Cards[i].GetComponent<CardDificil>().setCardBack(CardBack);
            counter++;
        }
        Debug.Log(counter);
        for (int i = 0; i < matches; i++)
        {
            Cards[i + matches].GetComponent<Image>().sprite = CardBack;
            Cards[i + matches].GetComponent<CardDificil>().setCardValue(i);
            Cards[i + matches].GetComponent<CardDificil>().setCardFace(CardFace[i]); // save the Face of the card with the respective object 
            Cards[i + matches].GetComponent<CardDificil>().setCardBack(CardBack); // save the Back of the card with the respective object 

        }
    }

    // Populate the Cards according to the number of Cards, The cards will vary accordingly
	public void populateContent(int[,] IIDimensionalArray){
		//Use the 2Dimenstional array to allocate and arrange the position of the cards 
		for (int i = 0; i < matches; i++) {
			Cards [IIDimensionalArray[randomNumber,i]].GetComponent<Image>().sprite = CardBack;
			Cards [IIDimensionalArray[randomNumber,i]].GetComponent<CardDificil> ().setCardValue(i);
			Cards [IIDimensionalArray[randomNumber,i]].GetComponent<CardDificil> ().setCardFace(CardFace [i]); // save the face of the card with the respective object 
			Cards [IIDimensionalArray[randomNumber,i]].GetComponent<CardDificil>().setCardBack(CardBack);
			counter++;
		}
		Debug.Log (counter);
		for (int i = 0; i < matches; i++) {
			Cards [IIDimensionalArray[randomNumber,i + matches]].GetComponent<Image>().sprite = CardBack;
			Cards [IIDimensionalArray[randomNumber,i + matches]].GetComponent<CardDificil> ().setCardValue(i);
			Cards [IIDimensionalArray[randomNumber,i + matches]].GetComponent<CardDificil> ().setCardFace(CardFace [i]); // save the Face of the card with the respective object 
			Cards [IIDimensionalArray[randomNumber,i + matches]].GetComponent<CardDificil> ().setCardBack(CardBack); // save the Back of the card with the respective object 


		}
	}
	/**************
	* Preparing the number of cards, assingning a value to the CardObject
	* Counter: counts the cards, continue at the half, assign the same values to the rest of the cards
	**************/

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
			if (Cards [i].GetComponent<CardDificil> ().getCardState() == 1 && (Cards[i].GetComponent<CardDificil>().getHasBeenMatched() == false)) {
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
		if (Cards [cardLocation [0]].GetComponent<CardDificil> ().getCardValue () == Cards [cardLocation [1]].GetComponent<CardDificil> ().getCardValue ()) {
			state = 2;
			totalMatches--;
			Debug.Log ("CArds have been matched!!!");
			//if (!isThisEasyMode)
			this.GetComponent<CounterManagerDificil>().addPoints(); // New code

			Cards [cardLocation [0]].GetComponent<CardDificil> ().setHasBeenMatched(true);
			Cards [cardLocation [1]].GetComponent<CardDificil> ().setHasBeenMatched(true);

			this.transform.GetChild (0).GetComponent<AudioSource> ().Play ();
			// CARDS DID NOT MATCH 
		} else {
			this.GetComponent<CounterManagerDificil>().losePoints();
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
			
		} 

	}

    void youWonTheGame(){
        modalManager.openResultsWindow(playAgainPanel);
        savePlayerData();
        this.GetComponent<CounterManagerDificil>().starsAnimation(true);
        rotateNumber = true;

    }

	IEnumerator pause(List<int> cardLocation){
		yield return new WaitForSeconds(time);

		Cards [cardLocation [0]].GetComponent<CardDificil> ().setCardState (0);
		Cards [cardLocation [1]].GetComponent<CardDificil> ().setCardState (0);

		Cards [cardLocation [0]].GetComponent<CardDificil> ().setupGraphics();
		Cards [cardLocation [1]].GetComponent<CardDificil> ().setupGraphics();
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
		PlayerPrefs.SetFloat (KEYscore, this.GetComponent<CounterManagerDificil>().GetPointsEarnedByPlayer());

		// setup the best score per level
		// If this KEY has been declared already 
		if (PlayerPrefs.HasKey (KEYbestScore)) {

			if (PlayerPrefs.GetFloat (KEYbestScore) > this.GetComponent<CounterManagerDificil>().GetPointsEarnedByPlayer()) {
				return; 
			} else if (PlayerPrefs.GetFloat (KEYbestScore) < this.GetComponent<CounterManagerDificil>().GetPointsEarnedByPlayer())
				PlayerPrefs.SetFloat (KEYbestScore, this.GetComponent<CounterManagerDificil>().GetPointsEarnedByPlayer());


		} else if (!PlayerPrefs.HasKey (KEYbestScore)) {
			PlayerPrefs.SetFloat (KEYbestScore, this.GetComponent<CounterManagerDificil>().GetPointsEarnedByPlayer());
		}


	} 

		//Setup The Mixing of the CARDS
		//Size: 5, Max number 5
		int[,] Matches3MixTheCards = new int[,] {
			{0,1,2,3,4,5},
			{5,4,3,2,1,0},
			{0,1,5,4,3,2},
			{4,1,0,5,2,3},
			{3,2,5,0,1,4}
		};
		//Size 5, max number 7
		int[,] Matches4MixTheCards = new int[,] {
			{0,1,2,3,4,5,6,7},
			{6,5,4,3,2,1,0,7},
			{0,6,1,5,4,3,2,7}, 
			{5,7,6,4,1,3,2,0},
			{7,6,5,4,3,2,1,0}
		};
		//Size 5, max number 9
		int[,] Matches5MixTheCards = new int[,] {
			{9,8,7,4,6,5,3,2,1,0},
			{6,5,4,3,2,1,0,7,8,9},
			{0,6,1,5,4,3,2,7,8,9},
			{0,1,2,3,4,5,6,7,8,9},
			{2,5,8,1,4,7,3,6,9,0}
		};
		//Size: 5, max number 11
		int[,] Matches6MixTheCards = new int[,] {
			{10,9,8,7,4,6,5,3,2,1,0,11},
			{11,6,5,4,3,2,1,0,7,8,9,10},
			{0,6,1,5,4,3,2,7,8,9,10,11},
			{0,1,2,3,4,5,6,7,11,8,10,9},
			{10,2,5,11,8,1,4,7,3,6,9,0}
		};
		//Size: 5, max number 13
		int[,] Matches7MixTheCards = new int[,] {
			{10,13,9,8,7,4,6,5,3,2,1,0,11,12},
			{11,12,6,5,4,3,2,1,0,7,8,9,10,13},
			{12,0,6,13,1,5,4,3,2,7,8,9,10,11},
			{0,1,2,3,4,5,6,7,11,12,8,10,9,13},
			{10,2,5,11,8,1,13,4,12,7,3,6,9,0}
		};

		//Size 5, max number 15
		int[,] Matches8MixTheCards = new int[,] {
			{15,10,13,9,8,7,4,6,5,3,2,1,0,11,12},
			{11,12,6,5,4,3,15,2,1,0,7,8,9,10,13},
			{12,0,6,13,1,5,4,3,2,7,8,9,10,11,15,},
			{0,1,15,2,3,4,5,6,7,11,12,8,10,9,13},
			{10,2,5,11,8,1,13,4,12,7,3,6,9,0,15}
		};
		// Size: 5, max number 17
		int[,] Matches9MixTheCards = new int[,] {
			{15,16,10,13,14,9,8,7,4,6,5,3,2,1,0,11,12,17},
			{17,11,12,6,5,4,3,15,16,2,1,0,7,8,9,10,13,14},
			{12,0,17,6,13,1,5,4,3,14,2,7,8,9,10,11,15,16},
			{0,1,15,2,3,4,5,6,7,11,12,8,16,10,9,13,14,17},
			{16,10,2,17,5,11,8,1,13,14,4,12,7,3,6,9,0,15}
		};


}
