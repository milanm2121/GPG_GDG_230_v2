using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    public static List<CardVersion2> cardList = new List<CardVersion2>();

    private void Awake()
    {
        cardList.Add(new CardVersion2(0, "None", "Doesn't", "French", 2, 3, 3, Resources.Load <Sprite>("0") ));
        cardList.Add(new CardVersion2(1, "Bob", "Hi", "French", 4, 2, 5, Resources.Load <Sprite>("1") ));
        cardList.Add(new CardVersion2(2, "Bobo", "Why", "French", 5, 4, 4, Resources.Load <Sprite>("2") ));
        cardList.Add(new CardVersion2(3, "Ned", "Please", "French", 1, 2, 1, Resources.Load <Sprite>("3") ));
        cardList.Add(new CardVersion2(4, "Neddy", "Stop", "French", 0, 1, 1, Resources.Load <Sprite>("4") ));
        cardList.Add(new CardVersion2(5, "Mulan", "This", "French", 7, 8, 8, Resources.Load <Sprite>("5") ));
    }
}
