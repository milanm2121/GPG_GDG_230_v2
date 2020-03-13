using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThisCard : MonoBehaviour
{
    public List<CardVersion2> thisCard = new List<CardVersion2>();
    public int thisID;

    public int id;
    public string thisCardName;
    public string thisCardDetails;
    public string thisCardType;
    public int thisCardCost;
    public int thisCardAttack;
    public int thisCardHealth;
   

    public Text nameText;
    public Text deatilText;
    public Text typeText;
    public Text costText;
    public Text attackText;
    public Text healthText;

    public Sprite thisCardSprite;
    public Image cardImage;

    public bool cardBack;
    //public static bool staticCardBack;

    public GameObject hand;

    public int numberOfCardsInDeck;

    // Start is called before the first frame update
    void Start()
    {
        thisCard[0] = CardDataBase.cardList[thisID];
        numberOfCardsInDeck = PlayerDeck.deckSize;
    }

    // Update is called once per frame
    void Update()
    {
        hand = GameObject.Find("PlayerHand Version2");
        if (this.transform.parent == hand.transform.parent)
        {
            cardBack = false;
        }

        id = thisCard[0].cardID;
        thisCardName = thisCard[0].cardName;
        thisCardDetails = thisCard[0].cardDetail;
        thisCardType = thisCard[0].cardType;
        thisCardCost = thisCard[0].cardCoinCost;
        thisCardAttack = thisCard[0].cardAttack;
        thisCardHealth = thisCard[0].cardHealth;
        thisCardSprite = thisCard[0].cardImage;

        nameText.text = "" + thisCardName;
        deatilText.text = "" + thisCardDetails;
        typeText.text = "" + thisCardType;
        costText.text = "" + thisCardCost;
        attackText.text = "" + thisCardAttack;
        healthText.text = "" + thisCardHealth;
        cardImage.sprite = thisCardSprite;

        //staticCardBack = cardBack;

        if (this.tag == "Clone")
        {
            thisCard[0] = PlayerDeck.staticDeck[numberOfCardsInDeck - 1];
            numberOfCardsInDeck -= 1;
            PlayerDeck.deckSize -= 1;
            cardBack = false;
            this.tag = "Untagged";
        }
    }
}
