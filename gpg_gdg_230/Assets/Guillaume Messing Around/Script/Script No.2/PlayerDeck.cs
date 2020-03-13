using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{

    public List<CardVersion2> deck = new List<CardVersion2>();
    public List<CardVersion2> container = new List<CardVersion2>();

    public int x;
    public int deckSize;

    public GameObject cardsInDeck1;
    public GameObject cardsInDeck2;
    public GameObject cardsInDeck3;
    public GameObject cardsInDeck4;


    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        deckSize = 40;
        
        for (int i = 0; i < deckSize; i++)
        {
            x = Random.Range(1, 6);
            deck[i] = CardDataBase.cardList[x];
        }
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
