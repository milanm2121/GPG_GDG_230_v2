﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    public static List<CardVersion2> cardList = new List<CardVersion2>();

    private void Awake()
    {
        cardList.Add(new CardVersion2(0, "None", "Doesn't", "French", 2, 3, 3, Resources.Load <Sprite>("0"), 0, 0 ));
        cardList.Add(new CardVersion2(1, "Bob", "Draw 1 Card", "French", 4, 2, 5, Resources.Load <Sprite>("1"), 1, 0));
        cardList.Add(new CardVersion2(2, "Bobo", "Add 1 Max Mana", "French", 5, 4, 4, Resources.Load <Sprite>("2"), 0, 1));
        cardList.Add(new CardVersion2(3, "Ned", "Add 3 Max Coin", "French", 1, 2, 1, Resources.Load <Sprite>("3"), 0, 3));
        cardList.Add(new CardVersion2(4, "Neddy", "Draw 2 Cards", "French", 0, 1, 1, Resources.Load <Sprite>("4"), 2, 0));
        cardList.Add(new CardVersion2(5, "Mulan", "Add 10 Max Coin", "French", 7, 8, 8, Resources.Load <Sprite>("5"), 0, 10));
    }
}