using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalWindowDisplay : MonoBehaviour {
	[Header("References to Text and LEVEL script")]
	public Text questions; 
	public Text correct;
	public Text incorrect; 
	public Text coinsEarned; 
	public level1 nivel1;

	// Assigned in Unity
	public int possiblePoints; 
	public int numberOfQuestions; 

	private int correctAnswers;
	private int incorrectAnswers; 

	//This variables are used to rotate the numbers of the MODAL WINDOW
	bool rotateNumber;
	int numberGenerated; 
	int countRotations;

	void Start(){
		
	}

	void FixedUpdate(){
		if (rotateNumber) {
			numberGenerated = Random.Range (10, 100);
			questions.text = "Questions:  " + numberGenerated.ToString ();
			correct.text = "Correct:  " + numberGenerated.ToString ();
			incorrect.text = "Incorrect:  " + numberGenerated.ToString ();
			coinsEarned.text = numberGenerated.ToString ();
			countRotations++;

			if (countRotations == 70) {
				rotateNumber = false;
				questions.text = "Questions:  " + numberOfQuestions.ToString ();
				correct.text = "Correct:  " + correctAnswers.ToString();
				incorrect.text = "Incorrect:  " + incorrectAnswers.ToString();
				coinsEarned.text = possiblePoints.ToString ();; 
			}
		}

	}

	//This functions is called whenever a modal window is open
	public void setRotateNumber(bool rotate){
		rotateNumber = rotate;
		correctAnswers = nivel1.getCorrectAnswers ();
		incorrectAnswers = nivel1.getWrongAnswers ();
		//numberOfQuestions = nivel1.getTotalQuestionsToAsk ();

	}

	public void closeModalWindow(GameObject windowToClose){
		windowToClose.SetActive (false);
		this.transform.GetChild (0).GetComponent<AudioSource> ().Play ();
	}


}
