using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public List<CardVersion2> deck = new List<CardVersion2>();
    public List<CardVersion2> container = new List<CardVersion2>();
    public static List<CardVersion2> staticEnemyDeck = new List<CardVersion2>();

    public List<CardVersion2> cardsInHand = new List<CardVersion2>();
    //public bool aiCanPlay;

    public GameObject hand;
    public GameObject fieldZone;
    public GameObject spellZone;
    public bool isMonsterCard;
    public bool isSpellCard;

    public int x;
    public static int deckSize;

    public GameObject cardInDeck1;
    public GameObject cardInDeck2;
    public GameObject cardInDeck3;
    public GameObject cardInDeck4;

    public GameObject cardToHand;

    public GameObject[] clones;

    public static bool draw;

    public GameObject cardBack;

    public int currentCoin;
    public int currentMana;

    public bool[] aiCanSummon;

    public bool drawPhase;
    public bool summonPhase;
    public bool attackPhase;
    public bool endPhase;

    public int[] cardID;

    public int summonThisID;

    public AICardToHand aiCardToHand;

    public int summonID;

    public int howManyCards;

    public CardsOnTheField fieldCards;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(WaitFiveSeconds());

        x = 0;
        deckSize = 40;

        draw = true;

        for (int i = 0; i < deckSize; i++)
        {
            x = Random.Range(1, 10);
            deck[i] = CardDataBase.cardList[x];
        }

        StartCoroutine(StartGame());

        hand = GameObject.Find("EnemyHand Version2");
        fieldZone = GameObject.Find("EnemyField");
        spellZone = GameObject.Find("EnemySpellField");

        isMonsterCard = false;
        isSpellCard = false;
    }

    // Update is called once per frame
    void Update()
    {
        staticEnemyDeck = deck;
        
        if (deckSize < 30)
        {
            cardInDeck1.SetActive(false);
        }
        if (deckSize < 20)
        {
            cardInDeck2.SetActive(false);
        }
        if (deckSize < 2)
        {
            cardInDeck3.SetActive(false);
        }
        if (deckSize < 1)
        {
            cardInDeck3.SetActive(false);
        }

        if (ThisCard.drawX > 0)
        {
            StartCoroutine(Draw(ThisCard.drawX));
            ThisCard.drawX = 0;
        }

        if (TurnSystem.startTurn == false && draw == false)
        {
            StartCoroutine(Draw(1));
            draw = true;
        }

        currentCoin = TurnSystem.enemyCurrentCoin;
        currentMana = TurnSystem.enemyCurrentMana;

        if (0 == 0)
        {
            int j = 0;
            howManyCards = 0;
            foreach (Transform child in hand.transform)
            {
                howManyCards++;
            }
            foreach (Transform child in hand.transform)
            {
                cardsInHand[j] = child.GetComponent<AICardToHand>().thisAICard[0];
                j++;
            }

            for (int i = 0; i < 40; i++)
            {
                if (i >= howManyCards)
                {
                    cardsInHand[i] = CardDataBase.cardList[0];
                }
            }
            j = 0;
        }

        if (TurnSystem.isYourTurn == false)
        {
            for (int i = 0; i < 40; i++)
            {
                if (cardsInHand[i].cardID != 0)
                {
                    if (currentCoin >= cardsInHand[i].cardCoinCost)
                    {
                        aiCanSummon[i] = true;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < 40; i++)
            {
                aiCanSummon[i] = false;
            }
        }

        if (TurnSystem.isYourTurn == false)
        {
            drawPhase = true;
        }

        if (drawPhase == true && summonPhase == false && attackPhase == false)
        {
            StartCoroutine(WaitForSummonPhase());
        }

        if (TurnSystem.isYourTurn == true)
        {
            drawPhase = false;
            summonPhase = false;
            attackPhase = false;
            endPhase = false;
        }

        if (summonPhase == true)
        {
            summonID = 0;
            summonThisID = 0;

            int index = 0;
            for (int i = 0; i < 40; i++)
            {
                if (aiCanSummon[i] == true)
                {
                    cardID[index] = cardsInHand[i].cardID;
                }
            }

            for (int i = 0; i < 40; i++)
            {
                if (cardID[i] > summonID)
                {
                    summonID = cardID[i];
                }
            }

            summonThisID = summonID;

            foreach (Transform child in hand.transform)
            {
                if (child.GetComponent<AICardToHand>().id == summonThisID && currentCoin >= CardDataBase.cardList[summonThisID].cardCoinCost && fieldCards.fieldCards.Count < 5 && CardDataBase.cardList[summonThisID].cardType != "Spell")
                {
                    isMonsterCard = true;
                    Debug.Log("Monster Card");
                    if (isMonsterCard == true)
                    {
                        child.transform.SetParent(fieldZone.transform);
                        TurnSystem.enemyCurrentCoin -= CardDataBase.cardList[summonThisID].cardCoinCost;
                        fieldCards.fieldCards.Add(cardToHand);
                        isMonsterCard = false;
                        break;
                    }
                }
            }

            foreach (Transform child in hand.transform)
            {
                if (child.GetComponent<AICardToHand>().id == summonThisID && currentMana >= CardDataBase.cardList[summonThisID].cardCoinCost && fieldCards.fieldCards.Count < 5 && CardDataBase.cardList[summonThisID].cardType == "Spell")
                {
                    isSpellCard = true;
                    Debug.Log("Spell Card");
                    if (isSpellCard == true)
                    {
                        child.transform.SetParent(fieldZone.transform);
                        TurnSystem.enemyCurrentCoin -= CardDataBase.cardList[summonThisID].cardCoinCost;
                        fieldCards.fieldCards.Add(cardToHand);
                        isSpellCard = false;
                        break;
                    }
                }
            }

            summonPhase = false;
            attackPhase = true;
        }
    }

    public void Shuffle()
    {
        for (int i = 0; i < deckSize; i++)
        {
            container[0] = deck[i];
            int randomIndex = Random.Range(i, deckSize);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container[0];
        }

        //Instantiate(cardBack, transform.position, transform.rotation);

        StartCoroutine(ShuffleNow());
    }

    IEnumerator StartGame()
    {
        for (int i = 0; i <= 4; i++)
        {
            yield return new WaitForSeconds(1);
            Instantiate(cardToHand, transform.position, transform.rotation);
        }
    }

    IEnumerator ShuffleNow()
    {
        yield return new WaitForSeconds(1);
        clones = GameObject.FindGameObjectsWithTag("Clone");

        foreach (GameObject clone in clones)
        {
            Destroy(clone);
        }
    }

    IEnumerator Draw(int x)
    {
        for (int i = 0; i < x; i++)
        {
            yield return new WaitForSeconds(1);
            Instantiate(cardToHand, transform.position, transform.rotation);
        }
    }

    IEnumerator WaitFiveSeconds()
    {
        yield return new WaitForSeconds(5);
    }

    IEnumerator WaitForSummonPhase()
    {
        yield return new WaitForSeconds(6);
        summonPhase = true;
    }

    
}
