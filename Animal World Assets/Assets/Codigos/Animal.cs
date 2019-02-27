using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animal : MonoBehaviour {
	//Reference to Itself 
	[SerializeField] GameObject beast;
	private bool allPiecesHaveBeenFound;

	// The Animal Attributes
	[SerializeField] int animalPieces;

	[Header("Sprite Images")]
	[SerializeField] public Sprite[] animalBlurImage;
	[SerializeField] public Sprite[] animalFaceImage;

	// TEXT of ANIMAL
	[Header("Editor Reference: Animal Description")]
	//[SerializeField] GameObject animalReferenceName;
	[SerializeField] Text animalReferenceDescription;




	[Header("Texto--Strings")]
	//Name of the Animal
	[SerializeField] string animalName;
    [SerializeField] string animalNameEnglish;

	// If the user hasnt completed the animal, the displayed name shall be ????
	string unknownName = "?????";
	// Real Description of the Animal 
    [TextArea(3, 14)]
    public string animalDescription;
    [TextArea(3, 14)]
    public string animalDescriptionEnglish;



	public int[] hasBeenFound = {0, 0, 0, 0 }; // Si es 0 ha sido encontrada, sino 1, ha sido encontrada


	public Sprite[] getAnimalFace(){return animalFaceImage;}
	public Sprite[] getAnimalBlur(){return animalBlurImage;}

	void Start(){
		//animalReferenceName = beast.transform.Find("Text:NameOfAnimal").gameObject;
		animalReferenceDescription = beast.transform.GetChild(4).GetComponent<Text>();
	}

	public void displayText(){
		Debug.Log ("I have been Called and accessed!!!!!");

	}

	public void setAnimalName(){
        if (allPiecesHaveBeenFound == false) // If any card has been found
            animalReferenceDescription.GetComponent<Text>().text = unknownName;
          

        else
        {
            if (PlayerPrefs.GetInt("Lan") == 1)
                animalReferenceDescription.GetComponent<Text>().text = animalName + ", " + animalDescription;
            else if (PlayerPrefs.GetInt("Lan" ) == 2)
               animalReferenceDescription.GetComponent<Text>().text = animalNameEnglish + ", " + animalDescriptionEnglish;
        }
	}


	public bool getHaveBeenFound(){
		return allPiecesHaveBeenFound;
	}

	public void setHaveBeenFound(bool found){
		allPiecesHaveBeenFound = found;
	}
}
