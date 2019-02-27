using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class SceneManagerDisplay : MonoBehaviour {
	AsyncOperation sincronizador;
	public Slider progressBar;
	public Text loadingText;


	// Use this for initialization
	void Start () {
		progressBar.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown (KeyCode.Escape)) {
		if (Input.GetKey("escape")){
			Application.Quit();


		}
	}

	// This functions is called to activate the "LEARN SCENE" ID: "2"
	public void LoadLearnScene(int sceneNumber){
		progressBar.gameObject.SetActive (true);
		progressBar.value = 0.0f;
		StartCoroutine (LoadSceneWithProgress(sceneNumber));

	}

	public void LoadMainScene(int sceneNumber){
		progressBar.gameObject.SetActive (true);
		progressBar.value = 0.0f;
		StartCoroutine (LoadSceneWithProgress(sceneNumber));


	}

	public void LoadSceneWithName(string sceneName){

		progressBar.gameObject.SetActive (true);
		progressBar.value = 0.0f;
		StartCoroutine (Loading(sceneName));
		//progressBar.gameObject.SetActive (false);




	}

	IEnumerator Loading(string sceneName){
        sincronizador = SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);
		loadingText.text = "Loading " + (sincronizador.progress * 100);
		progressBar.value = sincronizador.progress;

		yield return null;



	}

	IEnumerator LoadSceneWithProgress(int sceneNumber)
	{
		sincronizador = SceneManager.LoadSceneAsync (sceneNumber);
		//sincronizador.allowSceneActivation = false;
		loadingText.text = "Loading " + (sincronizador.progress * 100);

		//yield return new WaitForSeconds (1);

		//while (!sincronizador.isDone){
			// The progress will not go above 90% because the "AllowSceneActivation" set to true is required 
			// to continue. it will stop loading at 90%
			progressBar.value = sincronizador.progress;

			//once it is almost done
			//if (sincronizador.progress == 0.9f){
			//	sincronizador.allowSceneActivation = true; // IMPORTANT: This is required at 90% of Scene Loading
			

			//}

			//Debug.Log (sincronizador.progress);
			yield return null;

		//}// end of while Loop
	}

}
