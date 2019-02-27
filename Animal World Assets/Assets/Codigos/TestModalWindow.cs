using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

//  This script will be updated in Part 2 of this 2 part series.
public class TestModalWindow : MonoBehaviour {

	//public GameObject Manager; 
	public Sprite icon;
	private ModalPanel modalPanel;
	private DisplayManager displayManager;

	private UnityAction myYesAction;
	private UnityAction myNoAction;
	private UnityAction myCancelAction;



	void Awake () {
		//modalPanel = ModalPanel.Instance ();
		//displayManager = DisplayManager.Instance ();

		//myYesAction = new UnityAction (TestYesFunction);
		//myNoAction = new UnityAction (TestNoFunction);
	    myCancelAction = new UnityAction (TestCancelFunction);
	}

	//  Send to the Modal Panel to set up the Buttons and Functions to call
	/// <summary>
	/// This is the Function that it is being used in the code.....
	/// </summary>
	public void TestYNC () {
		modalPanel = new ModalPanel ();
		displayManager = new DisplayManager();
		//modalPanel.Choice ("Do you want to spawn this sphere?", TestYesFunction, TestNoFunction, TestCancelFunction);
	     //modalPanel.Choice ("Would you like a poke in the eye?\nHow about with a sharp stick?", myYesAction, myNoAction, myCancelAction);
		modalPanel.Choice ("Would you like a poke in the eye?\nHow about with a sharp stick?", myCancelAction);

	}

	// This function has an icon, also!!! you can simply pass the FUNCTION intead to declare an "UnityAction and initialize it with a function"
//	public void TestYNCI () {
//		//modalPanel.Choice ("Do you want to spawn this sphere?", icon, TestYesFunction, TestNoFunction, TestCancelFunction);
//		modalPanel.Choice ("Do you want to spawn this sphere?", icon, TestCancelFunction);
//
//	}
//
//	//  These are wrapped into UnityActions
//	void TestYesFunction () {
//		displayManager.DisplayMessage ("Heck yeah! Yup!");
//	}
//
//	void TestNoFunction () {
//		displayManager.DisplayMessage ("No way, José!");
//	}
//
    void TestCancelFunction () {
//		displayManager.DisplayMessage ("I give up!");
	}
}