using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

//  This script will be updated in Part 2 of this 2 part series.
public class ModalPanel : MonoBehaviour {

	//public Text question;
	//public Image iconImage;
	//public Button yesButton;
	//public Button noButton;
	public Button cancelButton;
	public GameObject modalPanelObject;
	//public ModalPanel modalPanel; 
	//public ModalPanel modalPanel;



		


	public void Choice (string question, UnityAction cancelEvent) {
			
		modalPanelObject.SetActive (true);

		cancelButton.onClick.RemoveAllListeners();
		cancelButton.onClick.AddListener (cancelEvent);
		cancelButton.onClick.AddListener (ClosePanel);

	
		cancelButton.gameObject.SetActive (true);
	}

	//public void Choice (string question, Sprite icon, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent) {
	public void Choice (string question, Sprite icon, UnityAction cancelEvent) {
			
		modalPanelObject.SetActive (true);


		cancelButton.onClick.RemoveAllListeners();
		cancelButton.onClick.AddListener (cancelEvent);
		cancelButton.onClick.AddListener (ClosePanel);

	
		cancelButton.gameObject.SetActive (true);
	}

	void ClosePanel () {
		modalPanelObject.SetActive (false);
	}
}