using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CardVersion2 
{
    public int cardID;
    public string cardName;
    public string cardDetail;
    public string cardType;

    public int cardCoinCost;
    public int cardAttack;
    public int cardHealth;

    //Making the spell card cost.


    //Ability Lists
    public int drawXCards;
    public int addXMaxCoin;
    public int buffATK;
    public int buffHealth;
    public int summonMonster;
    public bool blocker;
    public int buffOtherATK;
    public int buffOtherHealth;
    public bool dontBuffThisUnit;

    public Sprite cardImage;

    public CardVersion2()
    {

    }

    public CardVersion2(int ID, string Name, string Detail, string Type, int Cost, int Attack, int Health, Sprite Image, int DrawXCards, int AddXMaxCoin, int BuffATK, int BuffHealth, int SummonMonster, bool Blocker, int BuffOtherATK, int BuffOtherHealth, bool DontBuffThisUnit)
    {
        cardID = ID;
        cardName = Name;
        cardDetail = Detail;
        cardType = Type;
        cardCoinCost = Cost;
        cardAttack = Attack;
        cardHealth = Health;

        cardImage = Image;

        drawXCards = DrawXCards;
        addXMaxCoin = AddXMaxCoin;
        buffATK = BuffATK;
        buffHealth = BuffHealth;
        summonMonster = SummonMonster;
        blocker = Blocker;
        buffOtherATK = BuffOtherATK;
        buffOtherHealth = BuffOtherHealth;
        dontBuffThisUnit = DontBuffThisUnit;
    }
}
