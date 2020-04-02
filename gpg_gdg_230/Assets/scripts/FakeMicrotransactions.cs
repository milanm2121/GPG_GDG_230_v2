using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeMicrotransactions : MonoBehaviour
{

    public int ingameCoins;

    public static int whichPurchaseOne;
    public int[] purchases;

    public GameObject purchaseScreen;
    public GameObject storeScreen;

    public Text cardName;
    public Text cardNumber;
    public Text cardExpiry;
    public Text cardCCV;


    //I fix this later in order to see if something has been put in into the card detail.
    public void CheckForPurchase()
    {
        Debug.Log(whichPurchaseOne);
        for (int i = 0; i < 6; i++)
        {

            if (whichPurchaseOne == i)
            {
                ingameCoins += purchases[i];
                Debug.Log("it works");
                purchaseScreen.SetActive(false);
            }
        }

    }
}
