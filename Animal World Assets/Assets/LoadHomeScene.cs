using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadHomeScene : MonoBehaviour {

    public static LoadHomeScene Instance { get; set; }
    AsyncOperation sincronizador;
    // Use this for initialization

    public Text restart;
    public Text puntos;

    private void Awake()
    {
        Instance = this;
    }

	void Start () {
        sincronizador = SceneManager.LoadSceneAsync("MainScene");
        sincronizador.allowSceneActivation = false;

        if (PlayerPrefs.GetInt("Lan") == 1){
            restart.text = "Reiniciar";
            puntos.text = "Puntos: ";
        }
        else {
            restart.text = "Restart";
            puntos.text = "Score: ";
        }
	}

    public void loadMAINScene(){
        sincronizador.allowSceneActivation = true;
    }
	
}
