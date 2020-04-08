using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    public static List<CardVersion2> cardList = new List<CardVersion2>();

    private void Awake()
    {
        cardList.Add(new CardVersion2(0, "Nobody", "I dont exist in the deck", "French", 1, 3, 3, Resources.Load <Sprite>("0"), 0, 0, 0, 0, 0));
        cardList.Add(new CardVersion2(1, "Bob", "Draw 1 Card", "French", 4, 2, 5, Resources.Load <Sprite>("1"), 1, 0, 0, 0, 0));
        cardList.Add(new CardVersion2(2, "Bobo", "Add 1 Max Mana", "French", 5, 4, 4, Resources.Load <Sprite>("2"), 0, 1, 0, 0, 0));
        cardList.Add(new CardVersion2(3, "Ned", "Buff 3 ATK and 3 DEF", "French", 1, 2, 1, Resources.Load <Sprite>("3"), 0, 0, 3, 3, 0));
        cardList.Add(new CardVersion2(4, "Neddy", "Buff 1 ATK", "French", 0, 1, 1, Resources.Load <Sprite>("4"), 0, 0, 1, 0, 0));
        cardList.Add(new CardVersion2(5, "Mulan", "Buff 3 Defence", "French", 7, 8, 8, Resources.Load <Sprite>("5"), 0, 0, 0, 3, 0));
        cardList.Add(new CardVersion2(6, "Frank", "Summon 3 Monster", "French", 1, 1, 1, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 3));
    }
}
