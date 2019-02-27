using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalManager : MonoBehaviour {


	[SerializeField] 
	public GameObject[] modalPanelHolder;

	/// <summary>
	/// Setup all the modals to SetActive False, we dont want them to show
	/// </summary>
	void Awake(){
		
	}

	// Open any Modal Window
	public void openWindowModal(GameObject windowToOpen){

		windowToOpen.SetActive (true);
		//this.transform.GetChild (0).GetComponent<AudioSource> ().Play ();
		

	}
		
	// Overloaded Function to stablish the sound child to play
	public void openWindowModal(GameObject windowToOpen, int number){
		windowToOpen.SetActive (true);
		this.transform.GetChild (number).GetComponent<AudioSource> ().Play ();
	}

	/// <summary>
	/// Close the Modal Window
	/// </summary>

	public void closeModalWindow(GameObject windowToClose){
		windowToClose.SetActive (false);
		//this.transform.GetChild (0).GetComponent<AudioSource> ().Play ();
	}



	public void closeModalWindowLEVELS(GameObject windowToClose){
		windowToClose.SetActive (false);
		this.transform.GetChild (2).GetComponent<AudioSource> ().Play ();
	}


		

	public void openResultsWindow(GameObject resultsWindow){
		resultsWindow.SetActive (true);
		this.transform.GetChild (0).GetComponent<AudioSource> ().Play ();


	}

	// Use this for initialization
	void Start () {
        for (int i = 0; i < modalPanelHolder.Length; i++)
        {
            modalPanelHolder[i].SetActive(false);

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
