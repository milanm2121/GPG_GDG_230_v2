using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public List<CardVersion2> deck = new List<CardVersion2>();
    public List<CardVersion2> container = new List<CardVersion2>();
    public static List<CardVersion2> staticEnemyDeck = new List<CardVersion2>();

    public List<CardVersion2> cardsInHand = new List<CardVersion2>();
    public bool aiCanPlay;

    public GameObject hand;
    public GameObject fieldZone;
    public GameObject spellZone;

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

        if (aiCanPlay == true)
        {
            for (int i = 0; i < 40; i++)
            {
                if (AICardToHand.cardsInHandStatic[i].cardID != 0)
                {
                    cardsInHand[i] = AICardToHand.cardsInHandStatic[i];
                }
            }
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
        yield return new WaitForSeconds(1);
        aiCanPlay = true;
    }
}
