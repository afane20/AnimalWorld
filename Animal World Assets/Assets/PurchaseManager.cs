using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseManager : MonoBehaviour {

    public void buy100Coins ()
    {
        IAPManager.Instance.Buy100Coins();
    }

    public void buy500Coins()
    {
        IAPManager.Instance.Buy500Coins();
    }
    public void buy1000Coins()
    {
        IAPManager.Instance.Buy1000Coins();

    }
    public void buyZapper()
    {
        IAPManager.Instance.BuyZapper();

    }

    public void buy3Animals()
    {
        IAPManager.Instance.Buy3Animals();

    }

    public void buyUnlockAll()
    {
        IAPManager.Instance.BuyUnlockAll();

    }
}
