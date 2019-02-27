using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    //Access it anywhere
    public static PlayerManager Instance { get; set; }

    public LearnManagerControl learnManager;

    public int pieceActivated;

    [Header("References to Values")]
    public Text EditorNumberOfPieces;
    public Text EditorNumberOfCoins;
    public Text EditorPieceCost;

    //Toast Message
    public GameObject coinsModal;
    public Text coinsTextprice; 

    private bool isOn = false;
    // Use this for initialization
    //public Text ownedCoinds;

    //PlayerPrefs strings KEYS
    private string coins = "Coins";
    private string pieces = "Pieces";
    private string piecePrice = "PiecePrice";
    private string zapper = "zapper"; // if zapper is 1 then activate, if it is 0 NOPE

    void Awake(){
        Instance = this;
    }

    void Start () {
        //setup the index

        // Read from a file or Storage how many pieaces have been Found
        // then Assign the pieaces to the Variable
        if (false == PlayerPrefs.HasKey(pieces)){
            PlayerPrefs.SetFloat(pieces, 0);
            EditorNumberOfPieces.text = PlayerPrefs.GetFloat(pieces).ToString();
        }
        else 
            EditorNumberOfPieces.text = PlayerPrefs.GetFloat(pieces).ToString();


    }

    public void addZapper(){
        PlayerPrefs.SetInt(zapper, 1);
    }

    public void RefreshValues (){
        EditorNumberOfCoins.text = PlayerPrefs.GetFloat(coins).ToString();
        EditorNumberOfPieces.text = PlayerPrefs.GetFloat(pieces).ToString();

    }

    // Refresh the UI, Refresh the index inside the book
    public void addAPiece(){
        // Purchased the Animal Piece
        if (PlayerPrefs.GetFloat(coins) >= PlayerPrefs.GetFloat(piecePrice)){
            PlayerPrefs.SetFloat(pieces, PlayerPrefs.GetFloat(pieces) + 1); // Add one piece 
            PlayerPrefs.SetFloat(coins, (PlayerPrefs.GetFloat(coins) - PlayerPrefs.GetFloat(piecePrice))); // subtracting the price of the piece from coins
            this.gameObject.transform.GetChild(0).GetComponent<AudioSource>().Play(); // sound effect 
            RefreshValues(); // refresh the UI
            Utilities.Instance.ActivatePieceModal(); // Open Modal to inform the user about the piece added
            displayEarnedPiece.Instance.displayThePiece();  // Displays the piece that was added
            Utilities.Instance.AssignIndex(); // Assign discovered piece to the index

            // Increment the price of the Animal piece by 25 
            PlayerPrefs.SetFloat(piecePrice, PlayerPrefs.GetFloat(piecePrice) + 10);
            coinsTextprice.text = PlayerPrefs.GetFloat(piecePrice).ToString();
        } else if (PlayerPrefs.GetFloat(coins) < PlayerPrefs.GetFloat(piecePrice)){
            // No enough Coins!!!!
            this.gameObject.transform.GetChild(1).GetComponent<AudioSource>().Play();
            Utilities.Instance.activateNotEnoughCoindsModal();
        }

    }

    // purchasing with money
    public void addAPieceTotal(int v)
    {
        PlayerPrefs.SetFloat(pieces, v);
        RefreshValues();
    }

    public void addPieces(float var)
    {
        PlayerPrefs.SetFloat(pieces, PlayerPrefs.GetFloat(pieces) + var);
        RefreshValues();
    }

    public void addCoins(float addCoins){
        PlayerPrefs.SetFloat(coins, PlayerPrefs.GetFloat(coins) + addCoins);
        RefreshValues();
    }

    // This functions opens and closes the modals. 
    // Activates de particle system.
    public void WhatYouEarnedModal()
    {
        coinsModal.SetActive(true);
        isOn = true;
        //activate particle system
        Utilities.Instance.coinsParticleSystem.SetActive(true);


    }

    public void coinModalPSOff()
    {
        Utilities.Instance.coinsParticleSystem.SetActive(false);
        isOn = false;
        coinsModal.SetActive(false);
    }


    // Update is called once per frame
    void Update () {
        
    }

    public void setPieaces(int piece){

        pieceActivated = piece;
    }

    public int getPiece(){
        return pieceActivated;
    }

    public void zapperModal()
    {
    
    }

    public void threeAnimalsActivatedModal()
    {

    }



    //public void createAchievement(string title, string description, string points, int imageNumber)
    //{

    //    GameObject achievement = (GameObject)Instantiate(toastMessage);
    //    setAchievementInfo(achievement, title, description, points, imageNumber);

    //}

    //public void setAchievementInfo(GameObject achievement, string title, string description, string points, int number)
    //{
    //    achievement.transform.SetParent(toastParent);
    //    achievement.transform.localScale = new Vector3(1, 1, 1);
    //    achievement.transform.GetChild(0).GetComponent<Text>().text = title;
    //    achievement.transform.GetChild(1).GetComponent<Text>().text = description;
    //    //achievement.transform.GetChild(2).GetComponent<Image>().sprite = medals[number];
    //    achievement.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = points;


    //}
}
