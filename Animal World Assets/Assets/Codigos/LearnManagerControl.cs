using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearnManagerControl : MonoBehaviour {

    public static LearnManagerControl Instance { get; set; }

    //Setting up the Album
    // The Animal Objects 
    public GameObject[] Animals;
	[Header("Reference to UI-Image Element in the Editor for each Animal")]
	public Image[] piezasDeAnimal;  //REFERENCE to OBJECT to the Object in the Editor

	//public int pieacesFound; 
	public PlayerManager playerManager;

    string randomText = "oadkfoon kdjfkd jfhjdh jeiurb \n dhjfh jdhfj hha???? ???? ????? ???? \n jdhfjd jbsajuen ";
    private void Awake()
    {
        Instance = this;   
    }
    /// <summary>
    /// This is for Initialization. 
    /// First Call SetOwnedImages, to check is the user owns an animal or not, The function gets 
    /// the number of pieces own by the player, once that is set, set the images Sprite. SetImages
    /// </summary>
    void Start () {
		
		setOwnedImages (); 
		setImages ();
        Utilities.Instance.AssignIndex();

    }


	public void setImages(){
        int counter = 0;
        int animalPiezaNumber = 1;
		for (int i = 0; i < Animals.Length;i++){
            // The Number "4" indicates the number of pieces of each Animal... Later Change it
            // for a variable to allow an animal to have "ANY" amount of pieces

            // If the animal has NOT been found yet "0 NO, 1 YES"

			for (int p = 0; p < 4; p++) {
                //Check if the animal has NOT been found 
				if (Animals[i].GetComponent<Animal>().hasBeenFound[p] == 0){
                    //change the face of the Animial that shows on the interface
					piezasDeAnimal [counter].GetComponent<Image> ().sprite = Animals [i].GetComponent<Animal> ().animalBlurImage [p];
                    piezasDeAnimal [counter].GetComponent<AnimalPieza> ().setAnimalPiezaNumber (animalPiezaNumber);
					
                    //Adding and set the IMAGES of the Object that complements the animal, image, image 2, image3, image 4
                    piezasDeAnimal[counter].GetComponent<AnimalPieza>().setAnimalBlur(Animals[i].GetComponent<Animal>().animalBlurImage[p]);
                    piezasDeAnimal[counter].GetComponent<AnimalPieza>().setAnimalFace(Animals[i].GetComponent<Animal>().animalFaceImage[p]);


                    counter++;
                    animalPiezaNumber++;
				}
                // if Animal has been found 
				else if (Animals[i].GetComponent<Animal>().hasBeenFound[p] == 1){
                    // Change for the face of the Animal 
					piezasDeAnimal [counter].GetComponent<Image> ().sprite = Animals [i].GetComponent<Animal> ().animalFaceImage [p];
                    piezasDeAnimal [counter].GetComponent<AnimalPieza> ().setAnimalPiezaNumber (animalPiezaNumber);
					piezasDeAnimal [counter].GetComponent<AnimalPieza> ().setFound(true);
					piezasDeAnimal [counter].GetComponent<AnimalPieza>().setupGraphics();

                    //Adding and set the IMAGES of the Object that complements the animal, image, image 2, image3, image 4
                    piezasDeAnimal[counter].GetComponent<AnimalPieza>().setAnimalBlur(Animals[i].GetComponent<Animal>().animalBlurImage[p]);
                    piezasDeAnimal[counter].GetComponent<AnimalPieza>().setAnimalFace(Animals[i].GetComponent<Animal>().animalFaceImage[p]);

					counter++;

				}
			} // End of 2nd Loop
		}// End of Primary Loop
	}
		
	//Get the number of pieces from the PLAYER, 
	public void setOwnedImages(){
        int numberOfPieces = (int)PlayerPrefs.GetFloat(Utilities.pieces);

		//numberOfPieces = 2; // if I have more that a multiple of 4, that means I have more Animals
		int cociente = numberOfPieces / 4; 
		int residuo = numberOfPieces % 4;
		Debug.Log ("cociente " + cociente);
		Debug.Log ("Residuo " + residuo);

		int counter = 0;
		// Check The number of Animals and the number of pieces that have been found
		for (int i = 0; i < Animals.Length; i++) {
			for (int j = 0; j < 4; j++) {
				if (counter < numberOfPieces){
					// declare that the piece has been found "1" means found "0" means not found
					Animals [i].GetComponent<Animal> ().hasBeenFound [j] = 1;
					counter++;
					if (Animals [i].GetComponent<Animal> ().hasBeenFound [3] == 1) {
						Animals [i].GetComponent<Animal> ().setHaveBeenFound (true);
						Animals [i].GetComponent<Animal> ().setAnimalName ();
					}
					Debug.Log ("Has Been Found Value : " + Animals [i].GetComponent<Animal> ().hasBeenFound [j]);
				}

			}
		}
	
	}

   
	// Update is called once per frame
	void Update () {
		
	}
}
