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
    //is this a player or AI used for loading cards
    public bool player;
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
    //cards in combat
    public int cards_in_combat;
    //the array of cards in combat
    public card[] combat_card_slots = new card[5];
    //the transform position of cards
    public Transform[] cambat_slots;

    //list of attacking card
    public List<card> attackingCards = new List<card>();
    //list of defending cards
    public List<card> defendingCards = new List<card>();


    //this is a deak(deck) it holds 40 cards
    public Deak deck;
    //a script that holds and dictaes the truns.
    public TurnBaseScript TBS;

    //these are resorses for using cards
    public int playerMana;
    public int playerGold;
    //the card that is selected (placeholder)
    public card selectedCard;
    //are you in the attack phase
    bool attacking;
    //this is a placeholder
    card attacking_card;
    //the template card for creating cards from scratch
    public GameObject cardTmp;
    //for mousepos raycasting
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        deck = GetComponent<Deak>();
        TBS = GameObject.Find("maneger object").GetComponent<TurnBaseScript>();
        if (player == true)
        {
            GameObject.Find("player deck").GetComponent<player_static_deck>().loadDeck();
            //pick7();
        }
        else
        {
            //pick 7 cards at the start of a game for your deck
            //pick7();
        }
        print("picked7");

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; cards_in_hand > i; i++)
        {
            hand[i].transform.position = Vector3.Lerp(hand[i].transform.position, hand_slots[i].position, 0.5f);
        }

        if (player)
        {
            //inspect card

            Ray mousepos = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit mousehover;
            Physics.Raycast(mousepos, out mousehover);
            //shiriinking of cards
            if (selectedCard != null && (mousehover.collider == null || mousehover.collider != selectedCard.GetComponent<Collider>()))
            {
                selectedCard.transform.localScale = new Vector3(0.4f, 0.4f, 1);
                //selectedCard.GetComponent<SpriteRenderer>().sortingOrder = (int)transform.position.z;
                selectedCard = null;

            }
            //groth of cards for view
            if (mousehover.collider != null && mousehover.collider.gameObject.tag == "card")
            {
                selectedCard = mousehover.collider.GetComponent<card>();
                mousehover.collider.gameObject.transform.localScale = Vector3.Lerp(new Vector3(Mathf.Clamp(mousehover.collider.gameObject.transform.localScale.x, 0.4f, 0.6f), Mathf.Clamp(mousehover.collider.gameObject.transform.localScale.y, 0.4f, 0.6f), Mathf.Clamp(mousehover.collider.gameObject.transform.localScale.z, 0.4f, 0.6f)), new Vector3(0.6f, 0.6f, 0.6f), 0.2f);
                selectedCard.GetComponent<SpriteRenderer>().sortingOrder = 10;

            }

            if (selectedCard != null && Input.GetMouseButtonDown(0))
            {
                for (int i = 0; cards_in_hand > i; i++)
                {
                    if (selectedCard == hand[i])
                    {
                        Use_card(i);
                        break;
                    }
                }
                for (int i = 0; active_cards > i; i++)
                {
                    if (selectedCard = active_cards_slots[i])
                    {
                        combat_card_slots[cards_in_combat] = selectedCard;
                        cards_in_combat += 1;
                        //visual change in card goes here
                        if (active == true)
                        {
                            SetToAttack(selectedCard);
                        }
                        else
                        {
                            SetToDefend(selectedCard);
                        }
                    }
                }
            }



        }

        //is it your turn
        if (active == true)
        {
            //testing for using cards
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Use_card(1);

            }
        }

    }

    //replaces hand with 7 new cards
    public void pick7()
    {
        //sets cards in hand to 0
        cards_in_hand = 0;
        for (int i = 0; hand.Length > i; i++)
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
        if (picked_card.Mana_cost <= playerMana && picked_card.gold_cost <= playerGold)
        {
            //takes away the cost
            playerMana -= picked_card.Mana_cost;
            playerGold -= picked_card.gold_cost;

            //cheaks if the card is magic or a unit
            if (picked_card.IsMagic == false && active_cards <= 5)
            {
                //moves the card from the hand into the feild
                active_cards_slots[active_cards] = picked_card;
                hand[picked_card_index] = null;
                //updates card count values
                cards_in_hand -= 1;
                active_cards += 1;
                //need to fix this(this sould visualy move the card to the feild)
                print(active_cards - 1);
                picked_card.gameObject.transform.position = active_slots[active_cards - 1].position;

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

    public void SetToAttack(card card)
    {
        attackingCards.Add(card);
    }

    public void SetToDefend(card card)
    {
        card.transform.rotation = Quaternion.Euler(0, 0, 90);
        defendingCards.Add(card);
    }
}