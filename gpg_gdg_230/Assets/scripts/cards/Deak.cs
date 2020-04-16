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
    public CardLoading[] deck = new CardLoading[40];

    //the list of desroyed cards
    public List<ScriptableCard> graveyard=new List<ScriptableCard>();
    public List<CardLoading> newGraveyard = new List<CardLoading>();
    //the template used for creating cards from the template
    public GameObject cardTemp;
    public GameObject spellCardTemp;

    public AudioSource AS;
    public AudioClip cardDraw;

    public GameObject UIdeck;

    void Start()
    {
        AS = GetComponent<AudioSource>();
    }
    public GameObject cardfeild;
    private void Update()
    {
        if (deak[0] == null)
        {
            for(int i = 0; deak.Length > i+1; i++)
            {
                deak[i] = deak[i + 1];
            }
        }
        if (Cards_active_deak == 32 && UIdeck.transform.childCount==5)
        {
            Destroy(UIdeck.transform.GetChild(4).gameObject);
        }
        if (Cards_active_deak == 24 && UIdeck.transform.childCount ==4)
        {
            Destroy(UIdeck.transform.GetChild(3).gameObject);

        }
        if (Cards_active_deak == 16 && UIdeck.transform.childCount ==3)
        {
            Destroy(UIdeck.transform.GetChild(2).gameObject);

        }
        if (Cards_active_deak == 8 && UIdeck.transform.childCount ==2)
        {
            Destroy(UIdeck.transform.GetChild(1).gameObject);

        }
        if (Cards_active_deak == 0 && UIdeck.transform.childCount ==1)
        {
            Destroy(UIdeck.transform.GetChild(0).gameObject);

        }

    }

    //generates and spwans cards
    public GameObject Pick_random(Hand hand)
    {
        ScriptableCard Random_card_index;
        int card_index_picked = Random.Range(0, Cards_active_deak);
        Random_card_index = deak[card_index_picked];
        GameObject Random_card = new GameObject();
        if (Random_card_index.isSpell == false)
        {
            Random_card = Instantiate(cardTemp);
        }
        else
        {
            Random_card = Instantiate(spellCardTemp);
        }
        Random_card.transform.localScale = new Vector3(1, 1, 1);
        
        ScriptableCard sc = new ScriptableCard
        {
            artwork = Random_card_index.artwork,
            name = Random_card_index.name,
            health = Random_card_index.health,
            attack = Random_card_index.attack,
            manaCost = Random_card_index.manaCost,
            description = Random_card_index.description,
            isSpell = Random_card_index.isSpell
        };
        

        if (Random_card.GetComponent<CardDisplay>().card == null) {
            print(hand.name);
        }
        Random_card.GetComponent<CardDisplay>().card = sc;

        

        Cards_active_deak--;
        for(int i = card_index_picked; Cards_active_deak-1 > i; i++)
        {
            deak[i] = deak[i+1];
        }
        for (int i = Cards_active_deak; deak.Length > i; i++)
            deak[i] = null;

        Random_card.transform.parent= cardfeild.transform;

        Vector3 x = UIdeck.transform.GetChild(UIdeck.transform.childCount-1).transform.position;
        Random_card.transform.position = x;

        Random_card.GetComponent<RectTransform>().localScale = new Vector2(0.6f,0.6f);
        if (hand.player == false)
        {
            Random_card.GetComponent<CardDisplay>().hide = true;
        }
        AS.clip = cardDraw;
        AS.Play();

        return Random_card;

        
    }

}
