using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

// Unity Ads
public class PlayerAds : MonoBehaviour {

    //// Show the ad once it is ready 
    //public void ShowAd(){

    //    if (Advertisement.IsReady()){
    //        Advertisement.Show("", new ShowOptions(){resultCallback = HandleAdResult});
    //    }

    //}

    //// Check the What happened
    //private void HandleAdResult(ShowResult result){
    //    switch (result){
    //        // The REWARD of the Player 
    //        case ShowResult.Finished:
    //            //Debug.Log("Player gains 5 gems");
    //            this.transform.GetChild(0).gameObject.SetActive(true);
    //            PlayerManager.Instance.addCoins(30);
    //            break;
           
    //        // Player skipped the ad
    //        case ShowResult.Skipped:
    //            //Debug.Log("Player did not fully watch the ad");
    //            break;
            
    //        case ShowResult.Failed:
    //            //Debug.Log("PLayer Failed to launch the ad? ");
    //            break;

    //    }
    //}
}
