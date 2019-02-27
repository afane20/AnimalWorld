using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Utilities : MonoBehaviour{

    public static Utilities Instance { get; set; }
    //
    [Header("The Index")]
    public Sprite[] indexAnimalsSprite;
    public GameObject[] indexButtons;
    // Utilities Variables
    // Loading\Unloading Scenes
    public static string sceneNameToLoad;
    public static string sceneNameToThrow;

    //Player Prefs KEYS 
    public static string pieces = "Pieces";
    public static string piecePrice = "PiecePrice";
    public static string coins = "Coins";
    public static string language = "Lan";
    public static int spanish = 1;
    public static int english = 2;
    public static string KEYEasyContinue = "ContinueEasy";
    public static string KEYMedioumContinue = "ContinueMedioum";
    public static string KEYHardContinue = "ContinueHard";
    public static bool main = true; 

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject camera;

    //Particle System
    public GameObject coinsParticleSystem;
    public GameObject pieceParticleSystem;
    //Modals
    public GameObject pieceModal;
    public GameObject notEnoughCoinsModal;


    public static Scene currentScene;

    void Awake()
    {
            Instance = this;
    }
    // unload a Scene with a given name
    public static void sceneThrower (){
        SceneManager.UnloadSceneAsync(sceneNameToThrow);
    }
	
    // Este Mecanismo ayudara a mantener la primera Scene Activa mientras
    // las demas permaneceran activas una a la ves
    public static void LoadSceneWithName (string scene){
        // currentScene = SceneManager.GetActiveScene(); // If additive gets the first scene
        SceneManager.UnloadSceneAsync(sceneNameToThrow);
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);

        Debug.Log("CURRENT SCENEEEE!!!!!! " + currentScene.name);
    }

    public void SetCanvasFalse()
    {
        canvas.SetActive(false);
        this.GetComponent<AudioSource>().Stop();
    }

    public void SetCanvasTrue()
    {
        canvas.SetActive(true);
        this.GetComponent<AudioSource>().Play();

    }

    public void ENABLEAudioListener()
    {
        camera.GetComponent<AudioListener>().enabled = true;
    }

    public void DISABLEAudioListener()
    {
        camera.GetComponent<AudioListener>().enabled = false;
    }

    public void ActivatePieceModal(){
        pieceModal.SetActive(true);
        pieceParticleSystem.SetActive(true);
    }
    public void ClosePieceModal(){
        pieceModal.SetActive(false);
        pieceParticleSystem.SetActive(false);
    }

    public void activateNotEnoughCoindsModal(){
        notEnoughCoinsModal.SetActive(true);
    }

    //Refresh Index
    public void AssignIndex()
    {
        for (int i = 0; i < LearnManagerControl.Instance.Animals.Length;i++)
        {
            if (LearnManagerControl.Instance.Animals[i].GetComponent<Animal>().getHaveBeenFound())
            { 
                indexButtons[i].GetComponent<Image>().sprite = indexAnimalsSprite[i];
                indexButtons[i].transform.GetChild(0).GetComponent<Text>().enabled = false;
            }
        }
    }
}
