using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * this script holds all of the scriptable cards and has functions to call cards from the deck
 * 
 * 
 */
public class Deak : MonoBehaviour
{
    //cardsActive in deck
    public int Cards_active_deak=40;
    //class of the deck
    public int Class;
    //the array of cards
    public ScriptableCard[] deak = new ScriptableCard[40];
    //the template used for creating cards from te template
    public GameObject cardTemp;
    private void Update()
    {
        if (deak[0] == null)
        {
            for(int i = 0; deak.Length > i+1; i++)
            {
                deak[i] = deak[i + 1];
            }
        }
        
    }

    //generates and spwans cards
    public GameObject Pick_random(Hand hand)
    {
        ScriptableCard Random_card_index;
        int card_index_picked = Random.Range(0, Cards_active_deak);
        GameObject Random_card = Instantiate(cardTemp);
        Random_card.transform.localScale = new Vector3(1, 1, 1);
        Random_card_index = deak[card_index_picked];
        ScriptableCard sc = new ScriptableCard
        {
            artwork = Random_card_index.artwork,
            name = Random_card_index.name,
            health = Random_card_index.health,
            attack = Random_card_index.attack,
            manaCost = Random_card_index.manaCost,
            description = Random_card_index.description
        };
        
        Random_card.GetComponent<CardDisplay>().card = sc;

        if (Random_card.GetComponent<CardDisplay>().card == null) {
            print(hand.name);
        }

            

        Cards_active_deak--;
        for(int i = card_index_picked; Cards_active_deak-1 > i; i++)
        {
            deak[i] = deak[i+1];
        }

        Random_card.transform.parent = GameObject.Find("card feild").transform;
        Random_card.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        Random_card.GetComponent<card_functions>().hand = hand;
        return Random_card;
    }

}
