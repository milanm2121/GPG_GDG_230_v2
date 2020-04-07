using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasingCards : MonoBehaviour
{

   public static int cardcost;
   public collection collection;

   public void PurcahseTheCards()
    {
        cardcost = 250;
        if (FakeMicrotransactions.staticIngameCoins >= cardcost)
        {
            FakeMicrotransactions.staticIngameCoins -= cardcost;

            for (int i = 0; 6 < i; i++)
            {
                static_collections.Collection[Random.Range(0, 151)] += 1;
            }
            temp_collection x = new temp_collection();
            x.temp_collection_save();
            collection.load_cards();
        }
        else
            Debug.Log("comeback when you get some more money");
    }
}
