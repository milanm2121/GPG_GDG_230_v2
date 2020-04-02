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

    public InputField cardName;
    public InputField cardNumber;
    public InputField cardExpiryMonth;
    public InputField cardExpiryYear;
    public InputField cardCCV;


    //I fix this later in order to see if something has been put in into the card detail.
    public void CheckForPurchase()
    {
        if (cardName.text.Length >= 4 && cardNumber.text.Length == 12 && cardExpiryMonth.text.Length == 2 & cardExpiryYear.text.Length == 2 && cardCCV.text.Length == 3)
        {

            Debug.Log(whichPurchaseOne);
            for (int i = 0; i < 6; i++)
            {

                if (whichPurchaseOne == i)
                {
                    ingameCoins += purchases[i];
                    Debug.Log("it works");
                    purchaseScreen.SetActive(false);
                    cardName.text = " ";
                    cardNumber.text = " ";
                    cardExpiryMonth.text = " ";
                    cardExpiryYear.text = " ";
                    cardCCV.text = " ";
                }
            }
        }

    }
}
