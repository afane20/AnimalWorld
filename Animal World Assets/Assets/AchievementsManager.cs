using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AchievementsManager : MonoBehaviour {

	//Reference to a Prefab 
	public GameObject achievementPrefab;
	// Reference to a Panel, the container where the achievements will be created
	public Transform achievementParent;  


	// The medals. 
	//public Sprite[] medals; 

	/// <summary>
	/// The Method create achievement has 3 strings, 1 int
	/// </summary>
	void Start(){

		// Call this function to create as many achievements as you want
		createAchievement ("Getting the hang", "Complete the first memory level", "5", 0);
		createAchievement ("Unlock the First Animal", "get the 4 pieces for the animal", "5", 0);
		createAchievement ("Getting the hang", "Complete the first memory level", "5", 0);
		createAchievement ("Getting the hang", "Complete the first memory level", "5", 0);
		createAchievement ("Getting the hang", "Complete the first memory level", "5", 0);
		createAchievement ("Getting the hang", "Complete the first memory level", "5", 0);


	}

	public void createAchievement(string title, string description, string points, int imageNumber){

		GameObject achievement = (GameObject)Instantiate (achievementPrefab);
		setAchievementInfo (achievement, title, description, points, imageNumber );

	}

	public void setAchievementInfo(GameObject achievement, string title, string description, string points, int number){
		achievement.transform.SetParent (achievementParent);
		achievement.transform.localScale = new Vector3 (1, 1, 1);
		achievement.transform.GetChild (0).GetComponent<Text> ().text = title;
		achievement.transform.GetChild (1).GetComponent<Text> ().text = description;
		//achievement.transform.GetChild (2).GetComponent<Image> ().sprite = medals[number];
		achievement.transform.GetChild (3).GetChild(0).GetComponent<Text> ().text = points;


	}
}
