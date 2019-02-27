using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelcompleted : MonoBehaviour {


	[SerializeField]
	Text levelname;

	[SerializeField]
	Text score;

	public GameObject onestar;
	public GameObject twostars;
	public GameObject threestars;

	private int starscollected, completedlevel;


/// ///////////////////////////////////THIS SECTION IS TO SHARE SCORE ON FB ///////////////////////////////

	// Your app’s unique identifier.
	string AppID = "1469549399961687";
	// The link attached to this post.
	private string Link = "www.google.com";

	// The URL of a picture attached to this post. The picture must be at least 200px by 200px.
	private string Picture = "https://cdn3.iconfinder.com/data/icons/brain-games/1042/Quiz-Games.png";

	// The name of the link attachment.
	private string Name = "MY HISHCORE";

	// The caption of the link (appears beneath the link name).
	private string Caption = "CLICK TO DOWNLOAD AWESOME QUIZ GAME!";

	// The description of the link (appears beneath the link caption).
	private string Description;


	/// ///////////////////// END OF FB SHARE SECTION ///////////////////////////////////

	void Start(){

		completedlevel = PlayerPrefs.GetInt ("RecentLevel");
		levelname.text = "Level " +completedlevel+ " ";

		for (int levels = 1; levels <= completedlevel; levels++) {
		
			if (levels == completedlevel) {
			
				score.text = "Score : "+ PlayerPrefs.GetInt ("level" + completedlevel + "score").ToString ()+" ";
				setstars (completedlevel);
			} 
		
		}

	}

	void setstars(int levelindex){


		starscollected = PlayerPrefs.GetInt ("level" + levelindex + "stars");

		if (starscollected == 1) {
		
			onestar.SetActive (true);
		}

		if (starscollected == 2) {

			twostars.SetActive (true);
		}

		if (starscollected == 3) {

			threestars.SetActive (true);
		}

	}

	public void sharescoreonfb(){

		Description = "I scored "+starscollected+" stars in QuizUp level "+completedlevel+"";
		Application.OpenURL("https://www.facebook.com/dialog/feed?"+ "app_id="+AppID+ "&link="+
			Link+ "&picture="+Picture+ "&name="+ReplaceSpace(Name)+ "&caption="+
			ReplaceSpace(Caption)+ "&description="+ReplaceSpace(Description)+
			"&redirect_uri=https://facebook.com/");
	}

	string ReplaceSpace (string val) {
		return val.Replace(" ", "%20");
	}

	public void next(){

		SceneManager.LoadScene ("levels");
	}
}
