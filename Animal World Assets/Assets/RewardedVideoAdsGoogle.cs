using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class RewardedVideoAdsGoogle : MonoBehaviour {

    // Try a static variable for random access
    private RewardBasedVideoAd rewardBasedVideo;

    public void Start()
    {
        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;


        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
        this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
        this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
        this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
        this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
        this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
        this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;


        this.RequestRewardedVideo();
    }

    // ca-app-pub-1031665279076115/8454986000 REAL ID VideoAdd google - Android
    // ca-app-pub-3940256099942544/5224354917 Google Test ID Android
    // ca-app-pub-3940256099942544/1712485313 Google Test ID iOS
    private void RequestRewardedVideo()
    {
        #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1031665279076115/8454986000";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);

    }

    public void ShowRewardBasedVideo()
    {
        if (this.rewardBasedVideo.IsLoaded())
        {   // SHOW THE ADD, WAIT for a little bit
            this.rewardBasedVideo.Show();
            StartCoroutine(WaitASecond());
            if (Utilities.main == true)
            {
                PlayerManager.Instance.addCoins(75);
                PlayerManager.Instance.RefreshValues();
                PlayerManager.Instance.WhatYouEarnedModal();
            } else {
                PlayerManager.Instance.addCoins(30);
                PlayerManager.Instance.RefreshValues();
                PlayerManager.Instance.WhatYouEarnedModal();
            }
               

        }
        else
        {
            MonoBehaviour.print("Reward based video ad is not ready yet");
        }
    }

    IEnumerator WaitASecond()
    {
        yield return new WaitForSeconds(5.0f);
    }

    #region RewardBasedVideo callback handlers

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    #endregion

}
