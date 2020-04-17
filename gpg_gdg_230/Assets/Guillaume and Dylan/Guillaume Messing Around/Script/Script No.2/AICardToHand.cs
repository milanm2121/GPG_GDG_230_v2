using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AICardToHand : MonoBehaviour
{

    public List<CardVersion2> thisAICard = new List<CardVersion2>();

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

    //Setting up spell mechanic
    public bool spellCanBeUse;
    public bool spellPlay;
    public bool thisIsASpellCard;

    //This is to be use for our abilities.
    //Need to make a summoning one.
    public static int drawX;
    public int drawXCards;
    public int addXMaxCoin;
    public int buffXATK;
    public int buffXHealth;
    public int summoningMonsters;
    public bool token = true;
    public GameObject tokenObject;
    public List<CardVersion2> tokenCards = new List<CardVersion2>();
    public int buffOtherCardsATK;
    public bool buffingOtherCardsATKBool = false;
    public int buffOtherCardsHealth;
    public bool buffingOtherCardsHealthBool = false;
    public bool dontBuffThisUnit;
    public int healXHealth;
    public bool canHeal;
    public bool isSpellCard;

    public GameObject hand;

    public int z = 0;
    public GameObject cardObject;

    public int numberOfCardInDeck;

    public bool summoned;

    public GameObject monsterCardTemplate;
    public GameObject spellCardTemplate;

    public GameObject attackTextObject;
    public GameObject healthTextObject;

    public GameObject cardBack;
    public GameObject aiZone;
    public CardsOnTheField enemyField;
    public GameObject spellField;


    // Start is called before the first frame update
    void Start()
    {
        //cardsInHandStatic = cardsInHand;

        thisAICard[0] = CardDataBase.cardList[thisID];
        numberOfCardInDeck = AI.deckSize;
        hand = GameObject.Find("EnemyHand Version2");

        z = 0;

        summoned = false;

        aiZone = GameObject.Find("EnemyField");
        enemyField = aiZone.GetComponent<CardsOnTheField>();
        spellField = GameObject.Find("EnemySpellField");
    }

    // Update is called once per frame
    void Update()
    {
        if (z == 0)
        {
            hand = GameObject.Find("EnemyHand Version2");
            cardObject.transform.SetParent(hand.transform);
            cardObject.transform.localScale = Vector3.one;
            cardObject.transform.position = new Vector3(transform.position.x, transform.position.y, -48);
            cardObject.transform.eulerAngles = new Vector3(25, 0, 0);
            z = 1;
        }

        id = thisAICard[0].cardID;
        thisCardName = thisAICard[0].cardName;
        thisCardDetails = thisAICard[0].cardDetail;
        thisCardType = thisAICard[0].cardType;
        thisCardCost = thisAICard[0].cardCoinCost;
        thisCardSprite = thisAICard[0].cardImage;

        //This is so that the card ATK and Health will be able to change
        // when it is on the battle feild and either takes damage from 
        // opponent's card, or getting buff by your card.
        if (summoned == false)
        {
            thisCardAttack = thisAICard[0].cardAttack;
            thisCardHealth = thisAICard[0].cardHealth;
            buffOtherCardsATK = thisAICard[0].buffOtherATK;
            buffOtherCardsHealth = thisAICard[0].buffOtherHealth;
            dontBuffThisUnit = thisAICard[0].dontBuffThisUnit;
        }

        drawXCards = thisAICard[0].drawXCards;
        addXMaxCoin = thisAICard[0].addXMaxCoin;
        buffXATK = thisAICard[0].buffATK;
        buffXHealth = thisAICard[0].buffHealth;
        summoningMonsters = thisAICard[0].summonMonster;
        healXHealth = thisAICard[0].healXHealth;
        isSpellCard = thisAICard[0].isSpellCard;

        nameText.text = "" + thisCardName;
        deatilText.text = "" + thisCardDetails;
        typeText.text = "" + thisCardType;
        costText.text = "" + thisCardCost;
        attackText.text = "" + thisCardAttack;
        healthText.text = "" + thisCardHealth;
        cardImage.sprite = thisCardSprite;


        if (this.tag == "Clone")
        {
            thisAICard[0] = AI.staticEnemyDeck[numberOfCardInDeck - 1];
            numberOfCardInDeck -= 1;
            AI.deckSize -= 1;
            this.tag = thisAICard[0].cardType;
        }

        if (thisCardAttack == 0 && thisCardHealth == 0)
        {
            monsterCardTemplate.SetActive(false);
            attackTextObject.SetActive(false);
            healthTextObject.SetActive(false);
            spellCardTemplate.SetActive(true);
        }
        else
        {
            monsterCardTemplate.SetActive(true);
            attackTextObject.SetActive(true);
            healthTextObject.SetActive(true);
            spellCardTemplate.SetActive(false);
        }

        if (this.transform.parent == hand.transform)
        {
            cardBack.SetActive(true);
        }
        
        if(this.transform.parent == aiZone.transform || this.transform.parent == spellField)
        {
            cardBack.SetActive(false);
        }

        if (this.transform.parent == aiZone)
        {
            Summon();
        }
    }

    public void Summon()
    {
        Debug.Log("summoning");
        TurnSystem.currentCoin -= thisCardCost;
        summoned = true;
        //AddToken(summoningMonsters);

        SummoningTheMonster(summoningMonsters);


        MaxCoin(addXMaxCoin);
        drawX = drawXCards;
        BuffAttack(buffXATK);
        BuffHealth(buffXHealth);

        buffingOtherCardsATKBool = false;
        buffingOtherCardsHealthBool = false;
        dontBuffThisUnit = false;
        buffOtherCardsATK = 0;
        buffOtherCardsHealth = 0;
    }

    public void MaxCoin(int x)
    {
        TurnSystem.maxCoin += x;
        if (TurnSystem.maxCoin >= 10)
        {
            TurnSystem.maxCoin = 10;
        }
    }

    public void BuffAttack(int x)
    {
        if (buffOtherCardsATK > 0)
        {
            buffingOtherCardsATKBool = true;
            Debug.Log("Working Part 1");
            enemyField.BuffOtherCardsATKStats(buffOtherCardsATK);
            enemyField.BuffOtherTokenCardsATKStats(buffOtherCardsATK);
        }

        if (buffingOtherCardsATKBool == false)
        {
            thisCardAttack += x;
            attackText.text = thisCardAttack.ToString();
        }
    }

    public void BuffHealth(int x)
    {
        if (buffOtherCardsHealth > 0)
        {
            buffingOtherCardsHealthBool = true;
            Debug.Log("Yes");
            enemyField.BuffOtherCardsHealthStat(buffOtherCardsHealth);
            enemyField.BuffOtherTokenCardsHealthStat(buffOtherCardsHealth);
        }

        if (buffingOtherCardsHealthBool == false)
        {
            thisCardHealth += x;
            healthText.text = "" + thisCardHealth;
        }
    }

    public void SummoningTheMonster(int x)
    {
        for (int i = 0; i < tokenCards.Count; i++)
        {
            if (enemyField.fieldCards.Count >= 5)
            {
                break;
            }
            else if (enemyField.fieldCards.Count < 5)
            {
                Debug.Log("creating card: " + tokenObject);
                Instantiate(tokenObject);
                tokenCards.Remove(CardDataBase.cardList[0]);
                //if (!field.fieldCards.Contains(tokenObject))
                //field.fieldCards.Add(tokenObject);
            }
        }
    }

    public void Heal(int x)
    {
        if (canHeal == true)
        {
            PlayerHp.staticHp = x;
            canHeal = false;
        }
    }
}
