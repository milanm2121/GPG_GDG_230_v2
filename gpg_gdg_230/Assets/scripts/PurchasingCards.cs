using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasingCards : MonoBehaviour
{

   public static int cardcost;

   public void PurcahseTheCards()
    {
        cardcost = 250;
        FakeMicrotransactions.staticIngameCoins -= cardcost;
    }
}
