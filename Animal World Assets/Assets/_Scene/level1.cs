using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class level1 : MonoBehaviour {

	[Header ("Reference to Managers")]
	public ModalManager modalManager; 
	public ModalWindowDisplay modalWindowDisplay;

	public mcqquestion[] questions;   // creates an array which has a fixed size
	private static List<mcqquestion> unansweredQuestions; //creates list which changes its size during gameplay 
	private mcqquestion currentQuestion;

	public static int newhighscore;

	public Animator answers;


	public static int totalquestionstoask = 5;     //Change this value to set how many questions you have to ask in the game.
	public int getTotalQuestionsToAsk(){return totalquestionstoask;}

	[SerializeField]
	private Text factText;

	[SerializeField]
	private Text option1;

	[SerializeField]
	private Text option2;

	[SerializeField]
	private Text option3;

	[SerializeField]
	private Text option4;

	[SerializeField]
	private Text answerdialogbox;

	[SerializeField]
	public float timebetweenquestions;

	[SerializeField]
	private Text counter;

	[SerializeField]
	public float timeforeachquestion;


	private static int correctanswers = 0;
	private static int wronganswers = 0;

	public int getCorrectAnswers(){

		return correctanswers;
	}

	public int getWrongAnswers(){

		return wronganswers;
	}



	float end=0;

	/*section 1. This section selects a random question from mcqquestion.cs script along with associated 4 options
    and displays it */

	void Start (){

		PlayerPrefs.SetInt ("RecentLevel", 1);
		PlayerPrefs.Save ();

		if (unansweredQuestions == null || unansweredQuestions.Count == 0) 
		{

			unansweredQuestions = questions.ToList<mcqquestion>();
		}

		if (totalquestionstoask > 0) {
			SetCurrentQuestion ();
		} 

		if (totalquestionstoask == 0) {

			stopgame ();

		}
	}

	void SetCurrentQuestion(){

		int randomQuestionIndex = Random.Range (0, unansweredQuestions.Count);
		currentQuestion = unansweredQuestions [randomQuestionIndex];

		factText.text = currentQuestion.mcq;
		option1.text = currentQuestion.option1;
		option2.text = currentQuestion.option2;
		option3.text = currentQuestion.option3;
		option4.text = currentQuestion.option4;

	}

	//Section 1.1 
	// In this section we set the timer clock and logic behind it

	void Update(){


		if (end == 1 ) {

			return;
		} 

		if (end == 0)
		{
			timeforeachquestion = timeforeachquestion - Time.deltaTime;
			counter.text = timeforeachquestion.ToString ("F1");
		}


		if (timeforeachquestion < 0.0f)
		{
			unansweredQuestions.Remove(currentQuestion);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			totalquestionstoask = totalquestionstoask - 1;
		}


	}

	//end of section1.

	// section 2. this section is to show if the user's selected choice is correct or wrong

	public void option1selected(){

		if (currentQuestion.atrue) {

			answerdialogbox.text = "CORRECT";
			correctanswers = correctanswers + 1;
		} 

		else 
		{

			answerdialogbox.text = "WRONG";
			wronganswers = wronganswers + 1;
		}

		answers.SetTrigger ("mcqanswershow");
		StartCoroutine (TransitionToNextQuestion ());
	}


	public void option2selected(){

		if (currentQuestion.btrue) {

			answerdialogbox.text = "CORRECT";
			correctanswers = correctanswers + 1;
		} 

		else 
		{

			answerdialogbox.text = "WRONG";
			wronganswers = wronganswers + 1;

		}

		answers.SetTrigger ("mcqanswershow");
		StartCoroutine (TransitionToNextQuestion ());
	}

	public void option3selected(){

		if (currentQuestion.ctrue) {

			answerdialogbox.text = "CORRECT";
			correctanswers = correctanswers + 1;
		} 

		else 
		{

			answerdialogbox.text = "WRONG";
			wronganswers = wronganswers + 1;

		}


		answers.SetTrigger ("mcqanswershow");
		StartCoroutine (TransitionToNextQuestion ());
	}

	public void option4selected(){

		if (currentQuestion.dtrue) {

			answerdialogbox.text = "CORRECT";
			correctanswers = correctanswers + 1;
		} 

		else 
		{
			answerdialogbox.text = "WRONG";
			wronganswers = wronganswers + 1;

		}

		answers.SetTrigger ("mcqanswershow");
		StartCoroutine (TransitionToNextQuestion ());
	}


	//end of section 2.

	//section 3. This section sets the time delay between questions and reloads the scene after that time to show next question.

	IEnumerator TransitionToNextQuestion()
	{

		unansweredQuestions.Remove(currentQuestion);
		totalquestionstoask = totalquestionstoask - 1;
		yield return new WaitForSeconds (timebetweenquestions);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void skip(){

		unansweredQuestions.Remove(currentQuestion);
		totalquestionstoask = totalquestionstoask - 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

	}


	//section 4. Displays results of game

	void stopgame(){

		end = 1;
		factText.text = "END OF QUESTIONS";
		PlayerPrefs.SetInt ("level1score", correctanswers);
		PlayerPrefs.Save ();

		givestars();
	}
		
	//Give ratings
	void givestars(){
		if (correctanswers == 0) {

			modalWindowDisplay.setRotateNumber (true);
			modalManager.openWindowModal(modalManager.modalPanelHolder[0], 1);

		}

		if (correctanswers >= 1 && correctanswers <= 2) {
			modalWindowDisplay.setRotateNumber (true);
			modalManager.openWindowModal(modalManager.modalPanelHolder[0], 1);

			int newstarsearned = 1;
			int previousstars = PlayerPrefs.GetInt("level1stars",0);
			if (newstarsearned > previousstars) {

				PlayerPrefs.SetInt ("level1stars", 1);
				PlayerPrefs.Save ();
			}

		}

		if (correctanswers >= 3 && correctanswers <= 4) {
			modalWindowDisplay.setRotateNumber (true);
			modalManager.openWindowModal(modalManager.modalPanelHolder[0], 1);

			int newstarsearned = 2;
			int previousstars = PlayerPrefs.GetInt("level1stars",0);
			if (newstarsearned > previousstars) {

				PlayerPrefs.SetInt ("level1stars", 2);
				PlayerPrefs.Save ();
			}
		}

		if (correctanswers >= 5 && correctanswers <= 20) {
			modalWindowDisplay.setRotateNumber (true);
			modalManager.openWindowModal(modalManager.modalPanelHolder[0]);

			int newstarsearned = 3;
			int previousstars = PlayerPrefs.GetInt("level1stars",0);
			if (newstarsearned > previousstars) {

				PlayerPrefs.SetInt ("level1stars", 3);
				PlayerPrefs.Save ();
			}
		}
	
		//unlocknextlevel ();

	}



//	void unlocknextlevel(){
//
//		if (correctanswers >= 7) {
//		 
//			int previouslyunlocked=  PlayerPrefs.GetInt("levelsunlocked",0);
//			int newunlockedlevel = 2;
//
//			if (newunlockedlevel > previouslyunlocked) {
//				PlayerPrefs.SetInt ("levelsunlocked", 2);
//				PlayerPrefs.Save ();
//
//			}
//
//			reset ();
//
//		}
//
//		else
//			reset ();
//
//	}

	// Reset all variables to initial values

	void reset(){

		totalquestionstoask = 5; //change this value to initial value
		int levelloadingcriteria = correctanswers;
		correctanswers = 0;
		wronganswers = 0;
		unansweredQuestions = null;
		questions = null;

		if(levelloadingcriteria >= 4){
	
			//SceneManager.LoadScene ("levelcompleted");
			// Activate modal, and the rotation of the numbers for the dialog in the modal window 
			modalManager.openWindowModal(modalManager.modalPanelHolder[0]);

		}
		if (levelloadingcriteria < 4) {
			// Activate modal, and the rotation of the numbers for the dialog in the modal window 
			modalManager.openWindowModal(modalManager.modalPanelHolder[0], 1);

		}
	}

	public void cancelquiz(){

		totalquestionstoask = 20; //change this value to initial value
		correctanswers = 0;
		wronganswers = 0;
		unansweredQuestions = null;
		questions = null;
		SceneManager.LoadScene ("Aprender");


	}
}
