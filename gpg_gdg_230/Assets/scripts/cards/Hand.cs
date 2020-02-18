using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * this is the hand script in charge controling the options of what the player and cards can do here you will find the resorses and function of what you can do with cards
 * this script also acts as a maneger of the hand and feild.
 * this script is directly linked to the deak class which is an array of 40 cards.
 * made by milan
 */

public class Hand : MonoBehaviour
{
    //this dictates weither a hand is active or not
    public bool active;
    //the nuber of cards in your hand
    public int cards_in_hand; 
    //the array that holds the cards that are your hands
    public card[] hand = new card[7];
    //the public visual transform of your hand
    public Transform[] hand_slots;
    //the amount of active cards you have in your deck
    public int active_cards;
    //the array that holds the active cards in your deck
    public card[] active_cards_slots = new card[5];
    //the transforms of active positions in your side of the feild
    public Transform[] active_slots;
    //this is a deak(deck) it holds 40 cards
    public Deak deck;
    //a script that holds and dictaes the truns.
    public TurnBaseScript TBS;
    
    //these are resorses for using cards
    public int player_mana;
    public int player_gold;
    //the card that is selected (placeholder)
    card selectedCard;
    //are you in the attack phase
    bool attacking;
    //this is a placeholder
    card attacking_card;
    //the template card for creating cards from scratch
    public GameObject cardTmp;

    // Start is called before the first frame update
    void Start()
    {
        deck = GetComponent<Deak>();
        TBS = GameObject.Find("maneger object").GetComponent<TurnBaseScript>();
        //pick 7 cards at the start of a game for your deck
        pick7();
        print("picked7");
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; cards_in_hand > i; i++)
        {
            hand[i].transform.position = Vector2.Lerp(hand[i].transform.position, hand_slots[i].position,0.5f);
        }


        //is it your turn
        if (active == true)
        {
            //testing for using cards
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Use_card(1);

            }


            switch (TBS.state)
            {
                case (TurnState.StartTurn):
                    selectedCard = null;
                    break;
                case (TurnState.PlayerTurn):
                    
                    break;
                case (TurnState.Attack):
                    if (selectedCard == null)
                    {
                        //allows you to clik your sellected card
                        if (Input.GetMouseButtonDown(0) == true)
                        {
                            RaycastHit target;
                            Physics.Raycast(Camera.current.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out target);
                            selectedCard = target.collider.GetComponent<card>();
                        }
                    }
                    else
                    {
                        //i shoudent exsit
                        if (Input.GetKeyDown(KeyCode.A))
                        {
                            Card_attack(selectedCard);
                        }
                        if (Input.GetKeyDown(KeyCode.D))
                        {
                            Card_deffend(selectedCard);
                        }
                        if (Input.GetKeyDown(KeyCode.Backspace))
                        {
                            selectedCard = null;
                        }
                    }
                        
                        break;
                case (TurnState.Response):
                    
                    break;
                case (TurnState.End):
                    break;
                case (TurnState.Nothing):

                    break;

            }
        }
        if (attacking == true)
        {
            if (Input.GetMouseButtonDown(0) == true)
            {
                RaycastHit target;
                Physics.Raycast(Camera.current.ScreenToWorldPoint(Input.mousePosition), new Vector3(0,0,1),out target);
                attacking_card.target=target.collider.gameObject.GetComponent<card>();

            }
        }
    }

    //replaces hand with 7 new cards
    public void pick7()
    {
        //sets cards in hand to 0
        cards_in_hand = 0;
        for (int i=0; hand.Length > i; i++)
        {

            //clears the hand
            if (hand[i] != null)
                Destroy(hand[i].gameObject);
            hand[i] = null;
            //picks random card from the deak to place it in the hand
            hand[i] = deck.Pick_random();
            //creates a card crom the script heald in the deack
            GameObject card = Instantiate(cardTmp);
            card cardstats = card.AddComponent<card>();
            cardstats.Name = hand[i].Name;
            cardstats.IsMagic = hand[i].IsMagic;
            cardstats.gold_cost = hand[i].gold_cost;
            cardstats.Mana_cost = hand[i].Mana_cost;
            cardstats.attack_dmg = hand[i].attack_dmg;
            cardstats.health = hand[i].health;
            cardstats.Class = hand[i].Class;
            card.transform.position = hand_slots[cards_in_hand].position;
            hand[i] = card.GetComponent<card>();
            //adds a count to the cards in hand script
            cards_in_hand += 1;

        }
    }
    //a function used to use a card from the hand
    public void Use_card(int picked_card_index)
    {
        //uses an index int to select a card in the hand and put it in a place holder
        card picked_card = hand[picked_card_index];
        //cheaks if you have enough gold or mana to use tha card
        if (picked_card.Mana_cost <= player_mana && picked_card.gold_cost <= player_gold)
        {
            //takes away the cost
            player_mana -= picked_card.Mana_cost;
            player_gold -= picked_card.gold_cost;

            //cheaks if the card is magic or a unit
            if (picked_card.IsMagic==false && active_cards <=5)
            {
                //moves the card from the hand into the feild
                active_cards_slots[active_cards] = picked_card;
                hand[picked_card_index] = null;
                //updates card count values
                cards_in_hand -= 1;
                active_cards += 1;
                //need to fix this(this sould visualy move the card to the feild)
                print(active_cards-1);
                picked_card.gameObject.transform.position = active_slots[active_cards - 1].position;

                //cleans up the hand array
                for (int i=picked_card_index; cards_in_hand>i; i++)
                {
                    hand[i] = hand[i + 1];
                }
                if (cards_in_hand != 7)
                {
                    hand[cards_in_hand] = null;
                }
            }
            else
            {
                //magic stuff spell efects are put here
                cards_in_hand -= 1;
                //cleans up the hand array
                for (int i = picked_card_index; cards_in_hand > i; i++)
                {
                    hand[i] = hand[i + 1];
                }
                if (cards_in_hand != 7)
                {
                    hand[cards_in_hand] = null;
                }
            }
        }
    }
    //this shouldent exist i think?
    public void Card_deffend(card chosen_card)
	{
        chosen_card.GetComponent<Health>().defending = true;
        //need to trun this off later some how
	}
    //used for attacking
    public void Card_attack(card chosen_card)
    {
        attacking = true;
        StartCoroutine(chossetarget(chosen_card));
        attacking_card = chosen_card;
        
        
    }
    //attacks the target
    IEnumerator chossetarget(card chosen_card)
    {
        //waits for target to be chosen
        yield return new WaitUntil(()=>(chosen_card.target != null) || active==false);

        if (chosen_card.target != null)
        {

            if (chosen_card.target.defenfing == true)
            {
                //this is just a filler
                chosen_card.target.health -= chosen_card.attack_dmg / 2;
            }
            else
            {
                chosen_card.target.health -= chosen_card.attack_dmg;
            }
        }
        attacking = false;
        chosen_card.target = null;
        attacking_card = null;
    }
}