using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class LoadingBar : MonoBehaviour {

	AsyncOperation sincronizador;
	public GameObject loadingScreenBackGround;
	public Slider progressBar;
	public Text loadingText;


	// Use this for initialization
	void Start () {
        // ca-app-pub-1031665279076115~8877781545 ::: APP ID AdMob
        // ca-app-pub-1031665279076115~7225842784 Google Test ID
        // Initializing AdMob
        #if UNITY_ANDROID
            string appId = "ca-app-pub-1031665279076115~8877781545";
        #elif UNITY_IPHONE
            string appID = "ca-app-pub-1031665279076115~7086241981";
        #else
            string appId = "unexpected_platform";
        #endif
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

		progressBar.gameObject.SetActive (false);
        //LoadSceneWithProgress(1);
        StartCoroutine(LoadMeNowScene());
        Debug.Log("I WAS CALLEd START FUNCTION ---------");


	}

	public void LoadSceneButton(int sceneNumber){
		progressBar.gameObject.SetActive (true);
		progressBar.value = 0;
		StartCoroutine (LoadSceneWithProgress(sceneNumber));
		

	}
		
    IEnumerator LoadMeNowScene(){
        yield return new WaitForSeconds(2.0f);
        Debug.Log("I WAS CALLLEDDDD   !WWEWEWWEWEWE ");

        SceneManager.LoadScene("MainScene");

    }

	IEnumerator LoadSceneWithProgress(int sceneNumber)
	{

        yield return null;

		//StartCoroutine (LoadOtherScene ());

		sincronizador = SceneManager.LoadSceneAsync (sceneNumber);
		//sincronizador.allowSceneActivation = false;
		loadingText.text = "Loading " + (sincronizador.progress * 100);

		//yield return new WaitForSeconds (1);

		while (!sincronizador.isDone){
			// The progress will not go above 90% because the "AllowSceneActivation" set to true is required 
			// to continue. it will stop loading at 90%
			progressBar.value = sincronizador.progress;

			//once it is almost done
			if (sincronizador.progress == 0.9f){
                progressBar.value = sincronizador.progress * 100;
				//sincronizador.allowSceneActivation = true; // IMPORTANT: This is required at 90% of Scene Loading	

			}

			Debug.Log (sincronizador.progress);
			yield return null;

		}// end of while Loop
	}
		
}
