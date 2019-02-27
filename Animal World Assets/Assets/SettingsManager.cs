using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	// Use this for initialization

	[Header ("Reference to MODAL MANAGER, SOUND: yes, no")]
	public GameObject modalManager;

	[Header ("Buttons Reference")]
	public Button soundButton;
	public Button musicButton;
	public Button languageButton;
	public Button borrar;
	//Sprites to change
	[Header ("Sprites to Change")]
	public Sprite NoSoundButton;
	public Sprite NoMusicButton;
	public Sprite yesSoundButton;
	public Sprite yesMusicButton; 
	public Sprite englishButton;
	public Sprite spanishButton; 
	public Sprite disconnectedButton;
	public Sprite connectedButton;

	[Header ("Reference to Text Objects")]
	public Text soundEffectText;
	public Text musicText;
	public Text languageText;
	public Text borrarText;
	public Text conectedText;
	public Text beginSessionText;
	public Text eraseButtonYes;
	public Text areYouSureText;
    public Text addCoinsText;
    public Text addPieceText;
    public Text watchAdd;
	public GameObject settingsText;
	public GameObject memoryGameText;
	public GameObject rescueText;
	public GameObject aprenderText;
	public GameObject exerciseMindText; 
	public GameObject memoryEasyText;
	public GameObject memoryMedioumText;
	public GameObject memoryHardText;
	public GameObject shoppingText; 
	public GameObject AchievementsText; 
	public GameObject eraseGameData;

	//Default State of sounds and music
	private bool activeSound = true; 
	private bool activeMusic = true;
	private bool activeEnglish = true; 
    private string piecePrice = "PiecePrice";


	void Awake(){
        Utilities.main = true;
        //Enable saved preferences
        // 1 spanish, 0 english 

        //Setup the music: 1 Yes Music, 0 NO music
        // When the Player plays for the very first time
        if (PlayerPrefs.HasKey("primeraVez") != true) // This key doesnt exist
        {
            PlayerPrefs.SetFloat(piecePrice, 150);
            PlayerPrefs.SetInt("Music", 1); // 
            PlayerPrefs.SetInt("Sound", 1);
            PlayerPrefs.SetInt("Lan", 2);
            PlayerPrefs.SetString("ContinueEasy", "Facil-Memoria");
            PlayerPrefs.SetString("ContinueMedioum", "Dificultad Media");
            PlayerPrefs.SetString("ContinueHard", "Dificil Memoria 1");

        }

        if (PlayerPrefs.GetInt ("Lan") == 1) {
			SetSpanish ();

			Debug.Log(PlayerPrefs.GetInt ("Lan").ToString());


		} else if (PlayerPrefs.GetInt ("Lan") == 2) {

			SetEnglish ();

			Debug.Log(PlayerPrefs.GetInt ("Lan").ToString());

		}

       
        if (PlayerPrefs.GetInt ("Music") == 0)
			DeactivateMusicFuntion ();
		else
			ActivateMusicFuntion ();

		// Setup Sound: 1 yYES, 0 NO
		if (PlayerPrefs.GetInt ("Sound") == 0)
			DeactivateSoundFunction ();
		else 
			ActivateSoundFunction ();

        PlayerPrefs.SetInt("primeraVez", 10); // This key now exist, the value it is just to initialize it
	}
	/// <summary>
	/// If the value is '1' then the attribute is Active, if it is '0' the value is inactive
	/// </summary>
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void DeactivateSoundFunction(){
		soundButton.GetComponent<Image> ().sprite = NoSoundButton;
		PlayerPrefs.SetInt ("Sound", 0);
		modalManager.transform.GetChild (0).gameObject.SetActive (false);
		activeSound = false; 

	}
	/// <summary>
	/// This function activates and desactivate the sound
	/// </summary>
	public void SoundMethod (){
		if (activeSound == true) {
			DeactivateSoundFunction ();

		}

		else if (activeSound == false) {
			ActivateSoundFunction ();
		}

	}

	public void ActivateSoundFunction(){
		soundButton.GetComponent<Image> ().sprite = yesSoundButton;
		PlayerPrefs.SetInt ("Sound", 1);

		modalManager.transform.GetChild (0).gameObject.SetActive (true);
		activeSound = true; 

	}


	// The Settings Button ON/OFF (Modal Panel- Settings> MusicButton)
	public void MusicMethod (){
		if (activeMusic == true) {
			DeactivateMusicFuntion ();
		}

		else if (activeMusic == false) {
			ActivateMusicFuntion ();
		}

	}

	public void SetSpanish(){
		languageButton.transform.GetChild(0).GetComponent<Text>().text = "SPN";
		languageButton.GetComponent<Image> ().sprite = spanishButton;

		soundEffectText.text = "Eft. Sonido";
		musicText.text = "Musica";
		languageText.text = "Lenguaje";
		borrarText.text = "Borrar";
		beginSessionText.text = "Iniciar ses. en Google Play";
		eraseButtonYes.text = "Si";
		areYouSureText.text = "Esta Action borrara todos los datos del juego, Esta accion no se puede desacer, Esta Seguro?";
        addPieceText.text = "Agregar una pieza";
        addCoinsText.text = "Agregar 75 Monedas";
        watchAdd.text = "Ver add";

		settingsText.transform.GetChild (0).gameObject.SetActive (false);
		memoryGameText.transform.GetChild (0).gameObject.SetActive (false);
		rescueText.transform.GetChild (0).gameObject.SetActive (false);
		aprenderText.transform.GetChild (0).gameObject.SetActive (false);
		exerciseMindText.transform.GetChild (0).gameObject.SetActive (false);
		memoryEasyText.transform.GetChild (0).gameObject.SetActive (false);
		memoryMedioumText.transform.GetChild (0).gameObject.SetActive (false);
		memoryHardText.transform.GetChild (0).gameObject.SetActive (false);
		shoppingText.transform.GetChild (0).gameObject.SetActive (false);
		AchievementsText.transform.GetChild (0).gameObject.SetActive (false);
		eraseGameData.transform.GetChild (0).gameObject.SetActive (false);

		settingsText.transform.GetChild (1).gameObject.SetActive (true);
		memoryGameText.transform.GetChild (1).gameObject.SetActive (true);
		rescueText.transform.GetChild (1).gameObject.SetActive (true);
		aprenderText.transform.GetChild (1).gameObject.SetActive (true);
		exerciseMindText.transform.GetChild (1).gameObject.SetActive (true);
		memoryEasyText.transform.GetChild (1).gameObject.SetActive (true);
		memoryMedioumText.transform.GetChild (1).gameObject.SetActive (true);
		memoryHardText.transform.GetChild (1).gameObject.SetActive (true);
		shoppingText.transform.GetChild (1).gameObject.SetActive (true);
		AchievementsText.transform.GetChild (1).gameObject.SetActive (true);
		eraseGameData.transform.GetChild (1).gameObject.SetActive (true);


		activeEnglish = false; 


	}

	public void SetEnglish(){
		languageButton.transform.GetChild(0).GetComponent<Text>().text = "ENG";
		languageButton.GetComponent<Image> ().sprite = englishButton;

		soundEffectText.text = "Eft. Sound";
		musicText.text = "Music";
		languageText.text = "Language";
		borrarText.text = "Erase";
		beginSessionText.text = "Login to Google Play";
		eraseButtonYes.text = "Yes";
		areYouSureText.text = "This Action will erase all your game Data. This action cannot be undone. Are you sure?";
        addPieceText.text = "Add one Piece!";
        addCoinsText.text = "Add 75 Coins!";
        watchAdd.text = "Watch add";

		settingsText.transform.GetChild (0).gameObject.SetActive (true);
		memoryGameText.transform.GetChild (0).gameObject.SetActive (true);
		rescueText.transform.GetChild (0).gameObject.SetActive (true);
		aprenderText.transform.GetChild (0).gameObject.SetActive (true);
		exerciseMindText.transform.GetChild (0).gameObject.SetActive (true);
		memoryEasyText.transform.GetChild (0).gameObject.SetActive (true);
		memoryMedioumText.transform.GetChild (0).gameObject.SetActive (true);
		memoryHardText.transform.GetChild (0).gameObject.SetActive (true);
		shoppingText.transform.GetChild (0).gameObject.SetActive (true);
		AchievementsText.transform.GetChild (0).gameObject.SetActive (true);
		eraseGameData.transform.GetChild (0).gameObject.SetActive (true);

		settingsText.transform.GetChild (1).gameObject.SetActive (false);
		memoryGameText.transform.GetChild (1).gameObject.SetActive (false);
		rescueText.transform.GetChild (1).gameObject.SetActive (false);
		aprenderText.transform.GetChild (1).gameObject.SetActive (false);
		exerciseMindText.transform.GetChild (1).gameObject.SetActive (false);
		memoryEasyText.transform.GetChild (1).gameObject.SetActive (false);
		memoryMedioumText.transform.GetChild (1).gameObject.SetActive (false);
		memoryHardText.transform.GetChild (1).gameObject.SetActive (false);
		shoppingText.transform.GetChild (1).gameObject.SetActive (false);
		AchievementsText.transform.GetChild (1).gameObject.SetActive (false);
		eraseGameData.transform.GetChild (1).gameObject.SetActive (false);

		activeEnglish = true; 


	}
	public void SetLenguageSpanishEnglish(){
		if (activeEnglish == true) {
			languageButton.transform.GetChild(0).GetComponent<Text>().text = "SPN";
			languageButton.GetComponent<Image> ().sprite = spanishButton;
			PlayerPrefs.SetInt ("Lan", 1); // 1 spanish, 0 english 
			Debug.Log(PlayerPrefs.GetInt ("Lan").ToString());
			SetSpanish ();
            LearnManagerControl.Instance.setOwnedImages();
		}

		else if (activeEnglish == false) {
			languageButton.transform.GetChild(0).GetComponent<Text>().text = "ENG";
			languageButton.GetComponent<Image> ().sprite = englishButton;

			PlayerPrefs.SetInt ("Lan", 2);
			Debug.Log(PlayerPrefs.GetInt ("Lan").ToString());

			SetEnglish ();
		}
	
	}

	//Activate Music
	public void ActivateMusicFuntion(){
		musicButton.GetComponent<Image> ().sprite = yesMusicButton;
		PlayerPrefs.SetInt ("Music", 1);

		this.GetComponent<AudioSource> ().enabled = true; 
		activeMusic = true; 

	}

	// Get rid of Music
	public void DeactivateMusicFuntion(){
		musicButton.GetComponent<Image> ().sprite = NoMusicButton;
		PlayerPrefs.SetInt ("Music", 0);

		this.GetComponent<AudioSource> ().enabled = false;
		activeMusic = false; 

	}

	public void SyncWithGooglePlay(){



	}


}
