using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterManager : MonoBehaviour {

	[Header("Points Settings")]
	public float possiblePoints; // lets say 100
	public Text displayScore; // Displays the score
	private float numberOfMatches; // number of matches per level
	private float pointsPerMatchedAnimal; // the amount of points per animal discovered
	private float pointsEarnedByPlayer; 


	[Header("Timer Settings")]
	public Text counter;
	public float timeForThisLevel;

	//Grab the Modal Manager and display to the Player that He lost
	[Header("References- You Lost Modal")]
	public ModalManager modalManager;
	//Reference to the modal When the User RUNS OUT OF TIME
	public GameObject modalWindowToOpen;
	public Text levelName;
	public Text scorePoints;
	public Text coinsEarned;
	public Text BestScore;


	// Reference to the Starts that will be earned
	[Header("Stars -You Lost Panel- ")]
	public GameObject[] starts;
	private Animator startPlay;
	private Animator startPlay2;
	private Animator startPlay3;


	[Header("Stars -Play Again Panel- ")]
	private Animator playAgain;
	private Animator playAgain2;
	private Animator playAgain3;

	public bool playerFAILED; 
	int counterOfMatches;
	private bool lostRotateNumber;
	public bool GetLostRotateNumber(){return lostRotateNumber;}
	public void SetLostRotateNumber(bool rotate){lostRotateNumber = rotate;}
	int numberGenerated;

	int countRotations;

	// Use this for initialization
	void Start () {
		levelName.text = this.GetComponent<AppManagerMedio> ().actualLevelName;
 		lostRotateNumber = true;
		pointsEarnedByPlayer = 0;
		countRotations = 0;
		// you Lost Panel Stars Animator
		startPlay = starts [0].GetComponent<Animator> ();
		startPlay2 = starts [1].GetComponent<Animator> ();
		startPlay3 = starts [2].GetComponent<Animator> ();
		// Play again stars animators 
		playAgain =  starts [3].GetComponent<Animator> ();
		playAgain2 =  starts [4].GetComponent<Animator> ();
		playAgain3 =  starts [5].GetComponent<Animator> ();

		playerFAILED = false;
	}
	
	// Update is called once per frame



	// This function controls the timer 
	bool isOpen = false;
	void Update () {
		counterOfMatches = this.GetComponent<AppManagerMedio> ().totalMatches;

		// If we still have matches, and the time hasnt reach 0 yet... 
		if (timeForThisLevel > 0 && (counterOfMatches > 0)) {
			timeForThisLevel = timeForThisLevel - Time.deltaTime;
			counter.text = timeForThisLevel.ToString ("F1");
		}

		// if the timer is 0, and the matches is greater than 0 JUDAGOR no TERMINO
		if (timeForThisLevel <= 0 && (counterOfMatches > 0) && isOpen == false) {
			modalManager.openWindowModal (modalWindowToOpen, 1);
			playerFAILED = true;
			starsAnimation ();
			isOpen = true;
		}

	}

	/// <summary>
	/// This funtions is called 50 TIMES a second
	/// </summary>
	void FixedUpdate(){
		if (timeForThisLevel <= 0){
		if (lostRotateNumber) {
				numberGenerated = Random.Range (10, 100);
				scorePoints.text = "Score:   " + numberGenerated.ToString ();
				coinsEarned.text = "             " + numberGenerated.ToString ();
				BestScore.text = "Best:    " + numberGenerated.ToString ();

				countRotations++;
			Debug.Log ("Count ROTATIONS======= " + countRotations);
			if (countRotations == 60) {
				lostRotateNumber = false;
				scorePoints.text = "Score:   " + pointsEarnedByPlayer;
				coinsEarned.text = "             " + PlayerPrefs.GetFloat(this.GetComponent<AppManagerMedio>().getKEYcoins);
				BestScore.text =   "Best:    " + PlayerPrefs.GetFloat(this.GetComponent<AppManagerMedio>().getKEYbestScore);

			}
		}
		}
	}

	public void starsAnimation(){
		numberOfMatches = this.GetComponent<AppManagerMedio> ().totalMatches;
		if (pointsEarnedByPlayer < (possiblePoints / 2) && pointsEarnedByPlayer >= 25)
		startPlay.Play ("EstrellaIzquierda-animada");

		if (pointsEarnedByPlayer >= (possiblePoints / 2)) {
			startPlay.Play ("EstrellaIzquierda-animada");
			startPlay2.Play ("Estrella2-Animada");

		}

		if (pointsEarnedByPlayer == possiblePoints) {
			startPlay.Play ("EstrellaIzquierda-animada");
			startPlay2.Play ("Estrella2-Animada");
			startPlay3.Play ("Estrella3-Animada");

		}

	}

	public void starsAnimation(bool activate){
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

	/// <summary>
	/// pointsPerMatchedAnimal
	/// The Valued to be added per Matched Card 
	/// </summary>
	public void calculatePoints(int matches){
		numberOfMatches = matches;
		Debug.Log ("NUMBER OF MATCHES ======== " + numberOfMatches);

		pointsPerMatchedAnimal = (possiblePoints / numberOfMatches);
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

	public float GetPointsEarnedByPlayer(){return pointsEarnedByPlayer;}
	public void SetPointsEarnedByPlayer(float points){pointsEarnedByPlayer = points;}



}
