using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayEarnedPiece : MonoBehaviour {

    public LearnManagerControl learnManagerControl;
    public static displayEarnedPiece Instance { get; set; }

    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //displayThePiece();
    }

    public void displayThePiece()
    {
        int pieces = (int)PlayerPrefs.GetFloat(Utilities.pieces);
        // check the animals.
        //for (int i = 0; i < learnManagerControl.piezasDeAnimal.Length; i++){
        //    if (learnManagerControl.piezasDeAnimal[i].GetComponent<AnimalPieza>().getAnimalPiezaNumber() == (int)PlayerPrefs.GetFloat(Utilities.pieces)){
        //        // Display Animal Piece Earned
        //        this.GetComponent<Image>().sprite = learnManagerControl.piezasDeAnimal[i].GetComponent<AnimalPieza>().getAnimalFace();
        //        Debug.Log("NUMBER === " + PlayerPrefs.GetFloat(Utilities.pieces));

        //    }
        //    //do nothing
        //}

        // set Own pieces and Setup the Album
        if (pieces == 1)
            this.GetComponent<Image>().sprite = learnManagerControl.piezasDeAnimal[0].GetComponent<AnimalPieza>().getAnimalFace();
        else 
            this.GetComponent<Image>().sprite = learnManagerControl.piezasDeAnimal[pieces - 1].GetComponent<AnimalPieza>().getAnimalFace();

        learnManagerControl.setOwnedImages();
        learnManagerControl.setImages();
    }
}
