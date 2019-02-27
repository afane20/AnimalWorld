using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class Done_GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public GameObject restartButton;
	public GameObject homeButton;
    public GameObject modalPanelResults;

	private bool gameOver;
	private bool restart;
	private int score;
	
    // Google Code
    // private RewardBasedVideoAd rewardBasedVideo;

	void Start ()
	{
        PlayerPrefs.SetInt("Dogs", 0);
        PlayerPrefs.SetInt("Bees", 0);
        PlayerPrefs.SetInt("CoinsEarned", 0);


        // Get the video Ad - Google 
        // this.rewardBasedVideo = RewardBasedVideoAd.Instance;
        // this.RequestRewardedVideo();

		gameOver = false;
		restart = false;
		//restartText.text = "";
		restartButton.SetActive(false);
		homeButton.SetActive (false);
        modalPanelResults.SetActive(false);
		gameOverText.text = "";
		score = 0;


		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

    // Google code 
    //private void RequestRewardedVideo()
    //{
    //    #if UNITY_ANDROID
    //         string adUnitId = "ca-app-pub-3940256099942544/5224354917";
    //    #elif UNITY_IPHONE
    //         string adUnitId = "ca-app-pub-3940256099942544/1712485313";
    //    #else
    //         string adUnitId = "unexpected_platform";
    //    #endif

    //    // Create an empty ad request.
    //    AdRequest request = new AdRequest.Builder().Build();
    //    // Load the rewarded video ad with the request.
    //    this.rewardBasedVideo.LoadAd(request, adUnitId);
    //}
    //Google code
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
			if (gameOver)
			{
				restartButton.SetActive (true);
				homeButton.SetActive (true);
                modalPanelResults.SetActive(true);
				//restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void GameOver ()
	{
        if (PlayerPrefs.GetInt("Lan") == 2)
		    gameOverText.text = "Game Over!";
        else 
            gameOverText.text = "Tu canasta Fue Destruida";
        
		gameOver = true;
	}

    IEnumerator WaitAlittle()
    {
        yield return new WaitForSeconds(0.5f);

    }
    public void restartGame(){
        SceneManager.LoadScene("Rescatar1");
	
	}
}