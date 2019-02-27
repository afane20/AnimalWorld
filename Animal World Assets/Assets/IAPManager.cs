using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;

public class IAPManager : MonoBehaviour, IStoreListener {

    public static IAPManager Instance { get; set; }

    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    // This is the ID for the Stores. 
    // A REFERENCE TO THIS VARIABLES MUST EXIST 3 TIMES DURING THE PROCESS 
    // Product ID  Must Match with Google play Console
    public static string googleId1Dollar100Coins = "100coins";
    public static string googleId2Dollars500Coins = "500coins";
    public static string googleId5Dollars1000Coins = "1000coins";
    public static string googleId2DollarsZapper = "zapper";
    public static string googleId10DollarsUnlockAll = "10dollars";
    public static string googleId3Animals2Dollars = "3animals";
    //public static string kProductIDConsumable = "consumable";
    //public static string kProductIDNonConsumable = "nonconsumable";
    //public static string kProductIDSubscription = "subscription";



    // Apple App Store-specific product identifier for the subscription product.
    private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

    // Google Play Store-specific product identifier subscription product.
    private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(googleId1Dollar100Coins, ProductType.Consumable);
        builder.AddProduct(googleId2Dollars500Coins, ProductType.Consumable);
        builder.AddProduct(googleId5Dollars1000Coins, ProductType.Consumable);
        builder.AddProduct(googleId2DollarsZapper, ProductType.NonConsumable);
        builder.AddProduct(googleId10DollarsUnlockAll, ProductType.NonConsumable);
        builder.AddProduct(googleId3Animals2Dollars, ProductType.NonConsumable);

        // DLC you buy it only once!
        //builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);

        // This is for asubcriptions. 
        //builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
            //    { kProductNameAppleSubscription, AppleAppStore.Name },
            //    { kProductNameGooglePlaySubscription, GooglePlay.Name },
            //});

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    //Function example
    public void BuyConsumable()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        //BuyProductID(kProductIDConsumable);
    }

    public void Buy100Coins(){
        BuyProductID(googleId1Dollar100Coins);

    }
    public void Buy500Coins()
    {
        BuyProductID(googleId2Dollars500Coins);

    }
    public void Buy1000Coins()
    {
        BuyProductID(googleId5Dollars1000Coins);

    }

    // Buy only once 
    public void BuyZapper()
    {
        BuyProductID(googleId2DollarsZapper);
    }

    //Only Once 
    public void BuyUnlockAll(){

        BuyProductID(googleId10DollarsUnlockAll);
    }

    public void Buy3Animals()
    {

        BuyProductID(googleId3Animals2Dollars);
    }

    public void BuySubscription()
    {
        // Buy the subscription product using its the general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        // Notice how we use the general product identifier in spite of this ID being mapped to
        // custom store-specific identifiers above.
        //BuyProductID(kProductIDSubscription);
    }


    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    // The Code is about the SAME, JUST CHANGE the ID and then what will the user RECEIVE
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, googleId1Dollar100Coins, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerManager.Instance.addCoins(100);
        }
        // Or ... a non-consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, googleId2Dollars500Coins, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerManager.Instance.addCoins(500);
        }

        //Only Once
        else if (String.Equals(args.purchasedProduct.definition.id, googleId5Dollars1000Coins, StringComparison.Ordinal))
        {
            PlayerManager.Instance.addCoins(1000);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

        }

        //Only Once
        else if (String.Equals(args.purchasedProduct.definition.id, googleId10DollarsUnlockAll, StringComparison.Ordinal))
        {
            // Activate all the pieces. Application must restart
            PlayerManager.Instance.addCoins(3000);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }

        // Or ... a subscription product has been purchased by this user. ONLY ONCE, JUST CHANGE THE ID 
        else if (String.Equals(args.purchasedProduct.definition.id, googleId3Animals2Dollars, StringComparison.Ordinal))
        {
            PlayerManager.Instance.addPieces(12);
            PlayerManager.Instance.threeAnimalsActivatedModal();
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }

        else if (String.Equals(args.purchasedProduct.definition.id, googleId2DollarsZapper, StringComparison.Ordinal))
        {
            PlayerManager.Instance.addZapper();
            PlayerManager.Instance.zapperModal();
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }




        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
