using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is to make a fake microtransaction to our game. 
//I am making it this mock transaction as close as it needs to be to the real deal.
//Though this is the proper coding for this.
//Guillaume Blanchard
public class FakeMicrotransactions : MonoBehaviour
{

    //making the coins  static so that other scripts can call on it in order to buy cards
    public static int staticIngameCoins;
    public int ingameCoins;

    public Text coinText;

    public static int whichPurchaseOne;
    public int[] purchases;

    public GameObject purchaseScreen;
    public GameObject storeScreen;

    public InputField cardName;
    public InputField cardNumber;
    public InputField cardExpiryMonth;
    public InputField cardExpiryYear;
    public InputField cardCCV;


    void Update()
    {
        if (ingameCoins != staticIngameCoins)
        {
            ingameCoins = staticIngameCoins;
            coinText.text = ingameCoins.ToString();
        }
    }

    //I fix this later in order to see if something has been put in into the card detail.
    public void CheckForPurchase()
    {
        if (cardName.text.Length >= 4)
            if (cardNumber.text.Length >= 12)
                if (cardExpiryMonth.text.Length == 2)
                    if (cardExpiryYear.text.Length == 2)
                        if (cardCCV.text.Length == 3)
                        {

                            Debug.Log(whichPurchaseOne);
                            for (int i = 0; i < 6; i++)
                            {

                                if (whichPurchaseOne == i)
                                {
                                    staticIngameCoins += purchases[i];
                                    ingameCoins = staticIngameCoins;
                                    Debug.Log("it works");
                                    purchaseScreen.SetActive(false);
                                    cardName.text = " ";
                                    cardNumber.text = " ";
                                    cardExpiryMonth.text = " ";
                                    cardExpiryYear.text = " ";
                                    cardCCV.text = " ";
                                    coinText.text = ingameCoins.ToString();
                                }
                            }
                        }

    }
}
