using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{

    public List<CardVersion2> deck = new List<CardVersion2>();
    public List<CardVersion2> container = new List<CardVersion2>();
    public static List<CardVersion2> staticDeck = new List<CardVersion2>();

    public int x;
    public static int deckSize;

    public GameObject cardsInDeck1;
    public GameObject cardsInDeck2;
    public GameObject cardsInDeck3;
    public GameObject cardsInDeck4;

    public GameObject cardToHand;
    public GameObject cardBack;
    public GameObject theDeck;

    public GameObject[] clones;

    public GameObject hand;

    // Start is called before the first frame update
    private void Awake()
    {
        //GameObject.Find("Player")
    }
    void Start()
    {
        x = 0;
        deckSize = 40;
        
        for (int i = 0; i < deckSize; i++)
        {
            x = Random.Range(1, 10);
            deck[i] = CardDataBase.cardList[x];
        }

        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        staticDeck = deck;

        if (deckSize < 30)
        {
            cardsInDeck1.SetActive(false);
        }
        if (deckSize < 20)
        {
            cardsInDeck2.SetActive(false);
        }
        if (deckSize < 2)
        {
            cardsInDeck3.SetActive(false);
        }
        if (deckSize < 1)
        {
            cardsInDeck4.SetActive(false);
        }

        if(ThisCard.drawX > 0)
        {
            StartCoroutine(Draw(ThisCard.drawX));
            ThisCard.drawX = 0;
        }

        if (TurnSystem.startTurn == true)
        {
            StartCoroutine(Draw(1));
            TurnSystem.startTurn = false;
        }

    }

    
    IEnumerator Example()
    {
        yield return new WaitForSeconds(1);
        clones = GameObject.FindGameObjectsWithTag("Clone");

        foreach (GameObject clone in clones)
        {
           Destroy(clone);
        }
    }
    


    IEnumerator StartGame()
    {
        for (int i = 0; i <= 4; i++)
        {
            yield return new WaitForSeconds(1);
            Instantiate(cardToHand, transform.position, transform.rotation);
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
        StartCoroutine(Example());
    }

    IEnumerator Draw(int x)
    {
        for (int i = 0; i < x; i++)
        {
            yield return new WaitForSeconds(1);
            Instantiate(cardToHand, transform.position, transform.rotation);
        }
    }

    public void loadDeack(List<int> loadeddeck)
    {
        
    }
}
