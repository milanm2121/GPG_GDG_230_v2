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

    public bool canBeSummon;
    public bool summoned;
    public GameObject battleZone;

    public static int drawX;
    public int drawXCards;
    public int addXMaxCoin;

    public GameObject target;
    public GameObject enemy;

    public bool summoningSickness;
    public bool cantAttack;
    public bool canAttack;

    public static bool staticTargeting;
    public static bool staticTargetingEnemy;

    public bool targeting;
    public bool targetingEnemy;

    public bool onlyThisCardAttack;

    // Start is called before the first frame update
    void Start()
    {
        thisCard[0] = CardDataBase.cardList[thisID];
        numberOfCardsInDeck = PlayerDeck.deckSize;

        canBeSummon = false;
        summoned = false;

        drawX = 0;

        canAttack = false;
        summoningSickness = true;

        enemy = GameObject.Find("Enemy HP");

        targeting = false;
        targetingEnemy = false;
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

        drawXCards = thisCard[0].drawXCards;
        addXMaxCoin = thisCard[0].addXMaxCoin;

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

        if (TurnSystem.currentCoin >= thisCardCost && summoned == false)
            canBeSummon = true;

        else
            canBeSummon = false;


        if (canBeSummon == true)
            gameObject.GetComponent<DraggableCard>().enabled = true;

        else
            gameObject.GetComponent<DraggableCard>().enabled = false;


        battleZone = GameObject.Find("Field");


        if (summoned == false && this.transform.parent == battleZone.transform)
            Summon();

        if (canAttack == true)
        {
            //Add something to indecate that the card is able to attack
        }
        else
        {
            //Add something to indecate that the card is unable to attack
        }

        if (TurnSystem.isYourTurn == false && summoned == true)
        {
            summoningSickness = false;
            cantAttack = false;
        }

        if (TurnSystem.isYourTurn == true && summoningSickness == false && cantAttack == false)
        {
            cantAttack = true;
        }
        else
        {
            canAttack = false;
        }

        targeting = staticTargeting;
        targetingEnemy = staticTargetingEnemy;

        if (targetingEnemy == true)
        {
            target = enemy;
        }
        else
        {
            target = null;
        }

        if (targeting == true && targetingEnemy == true && onlyThisCardAttack == true)
        {
            Attack();
        }
    }

    public void Summon()
    {
        TurnSystem.currentCoin -= thisCardCost;
        summoned = true;

        MaxCoin(addXMaxCoin);
        drawX = drawXCards;
    }

    public void MaxCoin(int x)
    {
        TurnSystem.maxCoin += x;
        if (TurnSystem.maxCoin >= 10)
        {
            TurnSystem.maxCoin = 10;
        }
    }

    public void Attack()
    {
        if (cantAttack == true)
        {
            if (target != null)
            {
                if (target == enemy)
                {
                    canAttack = false;
                }

                if (target.name == "CardToHand(Clone)")
                {
                    canAttack = true;
                }
            }
        }
    }

    public void UntargetedEdEnemy()
    {
        staticTargetingEnemy = false;
    }

    public void TargetedEnemy()
    {
        staticTargetingEnemy = true;
    }

    public void StartAttack()
    {
        staticTargeting = true;
    }

    public void StopAttack()
    {
        staticTargeting = false;
    }

    public void OneCardAttack()
    {
        onlyThisCardAttack = true;
    }

    public void OneCardAttackStop()
    {
        onlyThisCardAttack = false;
    }


}
