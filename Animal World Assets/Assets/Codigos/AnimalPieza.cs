using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalPieza : MonoBehaviour {


	[SerializeField] private int cardState;    	// Check the state of the Card
	private bool found = false;					// the piece has been found 

	[SerializeField] private int piezaNumber; 	// defines the position and the number of the card 

	private Sprite animalBlur;
	private Sprite animalFace;

	private GameObject learnManager;

	void Start(){
		// Get me one active GameObject Tag 'LearnManager'
		learnManager = GameObject.FindGameObjectWithTag("LearnManager");
	}

	public void setupGraphics(){
		if (found == true) {
			Color temp = GetComponent<Image> ().color;
			temp.a = 1f;
			GetComponent<Image> ().color = temp;


		} else {
			Color temp = GetComponent<Image> ().color;
			temp.a = 0.5f;
			GetComponent<Image> ().color = temp;
		}

	}

	//Getters
	public Sprite getAnimalFace(){return animalFace;}
	public Sprite getAnimalBlur(){return animalBlur;}
	public int getAnimalPiezaNumber()  {return piezaNumber ;}
	public bool getFound()		 {return found;}


	//Setters
	public void setAnimalFace(Sprite animal){animalFace = animal;}
	public void setAnimalBlur(Sprite blur){animalBlur = blur;}
	public void setAnimalPiezaNumber(int number){piezaNumber = number;}
	public void setFound(bool encontrada){found = encontrada;}

}
