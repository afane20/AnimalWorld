using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

// Banner Ad from GOOGLE
public class DisplayAds : MonoBehaviour {

    private static BannerView bannerView;


	// Use this for initialization
	void Start () {
        RequestBanner();
	}

    // This is the real Ad code 
    // ca-app-pub-1031665279076115/8630369533 REAL ID for Android
    // REAL ID for iOS
    // ca-app-pub-3940256099942544/6300978111 Test Add for Android from Google 
    // ca-app-pub-3940256099942544/2934735716 Test Add for iOS from google 
    private void RequestBanner()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-1031665279076115/8630369533";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        AdSize adSize = new AdSize(360, 50);
        bannerView = new BannerView(adUnitId, adSize, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    public void DestroyTheBannerAd(){
        bannerView.Destroy();
    }

}
