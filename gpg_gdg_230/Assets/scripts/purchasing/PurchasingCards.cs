using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PurchasingCards : MonoBehaviour
{

   public static int cardcost;
   public collection collection;

    public TMP_Text responce;

    private void Update()
    {
        
       
    }
    public void PurcahseTheCards()
    {
        cardcost = 250;
        if (FakeMicrotransactions.staticIngameCoins >= cardcost)
        {
            FakeMicrotransactions.staticIngameCoins -= cardcost;

            for (int i = 0; 6 > i; i++)
            {
                int a = Random.Range(0, 151);
                static_collections.Collection[a] += 1;
                print(a.ToString());
            }
            temp_collection x = new temp_collection();
            x.temp_collection_save();
            collection.load_cards();
            responce.text = "yes... yes... come again";
        }
        else
            responce.text = "comeback when you get some more money";
    }
}
