using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class deck_stats : MonoBehaviour
{
    public collection col;
    serilisable_deak selected_deck;
    bool loadTick=false;
    int avrage_cost=0;
    int avrage_spell_cost=0;
    int number_of_creature_card=0;
    int number_of_spell_cards=0;
    public TMP_Text deckStats;

    public GameObject deleteButton;
    // Start is called before the first frame update
    void Start()
    {
        deckStats = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (col.selected_deck !=null && col.selected_deck.deck[0] != 0 && col.selected_deck.deck[1] != 0 && col.selected_deck.deck[2] != 0&& col.selected_deck.deck[3] != 0 && col.selected_deck.deck[4] != 0)
        {
            if (selected_deck != col.selected_deck)
            {
                selected_deck = col.selected_deck;
                loadTick = false;
            }
            if (loadTick == false)
            {  
                loadTick = true;
                loadStats();
            }
        }
        else
        {
            deckStats.text = "";
            deleteButton.SetActive(false);
        }
    }
    void loadStats()
    {

        avrage_cost = 0;
        avrage_spell_cost = 0;
        number_of_creature_card = 0;
        number_of_spell_cards = 0;

        for (int i = 0; selected_deck.deck.Length > i; i++)
        {
            ScriptableCard sc = col.id[selected_deck.deck[i]];
            if (sc.isSpell==true)
            {
                number_of_spell_cards++;
                avrage_spell_cost+=sc.manaCost;
            }
            else
            {
                number_of_creature_card++;
                avrage_cost+= sc.manaCost;
            }
        }
        if(number_of_creature_card!=0)
            avrage_cost=avrage_cost / number_of_creature_card;
        if(number_of_spell_cards!=0)
            avrage_spell_cost = avrage_spell_cost / number_of_spell_cards;
        deckStats.text = "number of units: " + number_of_creature_card +"\n" + "avrage unit cost: " + avrage_cost +"\n"+ "number of spell cards: " + number_of_spell_cards+"\n" + "avrage spell cost: " + avrage_cost;
        deleteButton.SetActive(true);
    }
}
