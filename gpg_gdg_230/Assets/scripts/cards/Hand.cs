using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * this is the hand script in charge of controlling the options of what the player and cards can do here you will find the resorses and function of what you can do with cards
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
    public GameObject[] hand = new GameObject[7];
    //the public visual transform of your hand
    public Transform[] hand_slots;

    //the amount of active cards you have in your deck
    public int active_cards;
    //the array that holds the active cards in your deck
    public GameObject[] active_cards_slots = new GameObject[5];
    //the transforms of active positions in your side of the feild

    public Transform[] active_slots;
    //cards in combat
    public int cards_in_combat;
    //the array of cards in combat
    public GameObject[] combat_card_slots = new GameObject[5];
    //the transform position of cards
    public Transform[] combat_slots;


    //this is a deak(deck) it holds 40 cards
    public Deak deck;
    //a script that holds and dictaes the truns.
    public TurnBaseScript TBS;

    //these are resorses for using cards
    public int playerMana;
    public int playerGold;
    //the card that is selected (placeholder)
    public GameObject selectedCard;
    //are you in the attack phase
    bool attacking;
    //this is a placeholder
    GameObject attacking_card;
    //for mousepos raycasting
    Camera cam;


    public combat_maneger cm;

    public List<GameObject> fieldCard;

    public bool firstAttack = true;

    bool stateTick=false;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        deck = GetComponent<Deak>();
        TBS = GameObject.Find("maneger object").GetComponent<TurnBaseScript>();
        cm = GameObject.Find("maneger object").GetComponent<combat_maneger>();

        if (player == true)
        {
            //copies static deck to the player ingame deck
            GameObject.Find("player deck").GetComponent<player_static_deck>().loadDeck();

        }

        

    }

    // Update is called once per frame
    void Update()
    {
        //repositions the cards
        for (int i = 0; cards_in_hand > i; i++)
        {
            hand[i].transform.position = Vector3.Lerp(hand[i].transform.position, hand_slots[i].position, 0.5f);
        }
        if (TBS.state != TurnBaseScript.TurnState.EndofBattle)
        {
            for (int i = 0; active_cards > i; i++)
            {
                active_cards_slots[i].transform.position = Vector3.Lerp(active_cards_slots[i].transform.position, active_slots[i].position, 0.5f);
                //card deaths
                if (active_cards_slots[i].GetComponent<CardDisplay>().card.health <= 0)
                    SendToGrave(active_cards_slots[i], i);
            }
        }


        if (player)
        {
            //repositions the selected card
            for (int i = 0; cards_in_hand > i; i++)
            {
                if (selectedCard != hand[i])
                    hand[i].transform.SetSiblingIndex(i);
            }

            if (selectedCard != null)
            {
                selectedCard.transform.SetAsLastSibling();
                if (Input.GetKey(KeyCode.I))
                {
                    selectedCard.GetComponent<RectTransform>().localScale = new Vector2(1f, 1f);
                }
                else
                {
                    selectedCard.GetComponent<RectTransform>().localScale = new Vector2(0.6f, 0.6f);

                }

            }
            


            //clikink cards
            if (selectedCard != null && Input.GetMouseButtonDown(0))
            {
                //for clicking cards in hand to move to the feild
                if (active == true && TBS.state == TurnBaseScript.TurnState.PlayerTurn)
                {
                    for (int i = 0; cards_in_hand > i; i++)
                    {
                        if (selectedCard == hand[i])
                        {
                            Use_card(i);
                            break;
                        }
                    }
                }

                //for seting cards from feild to actack or defend
                for (int i = 0; active_cards > i; i++)
                {
                    if (selectedCard == active_cards_slots[i])
                    {
                        combat_card_slots[i] = selectedCard;
                        cards_in_combat += 1;
                        //visual change in card goes here
                        if (active == true && TBS.state == TurnBaseScript.TurnState.Attack && cm.attack.Contains(selectedCard) == false)
                        {
                            //this function adds the cards to a list that the combat maneger uses
                            SetToAttack(selectedCard);
                        }
                        else if (active == false && TBS.state == TurnBaseScript.TurnState.Response && cm.defend.Contains(selectedCard) == false)
                        {
                            //this function adds the cards to a list that the combat maneger uses and rtates the card 90 degres
                            SetToDefend(selectedCard);
                        }
                    }
                   
                }
            }
        }
        else if (TBS.playerTurn == false)//if AI
        {
            if (stateTick == false)
            {
                if (TBS.state == TurnBaseScript.TurnState.PlayerTurn ){
                    if (cards_in_hand >= 1 && active_cards <= 4)
                    {
                        {
                            print("hand phase");

                            for (int i = 0; 20 > i; i++)
                            {
                                Use_card(Random.Range(0, cards_in_hand));
                            }
                        }
                    }
                    StartCoroutine(Ai_turn_control(TurnBaseScript.TurnState.Attack));
                }

            }
            if (TBS.state == TurnBaseScript.TurnState.Attack)
            {
                print("attack phase");

                if (stateTick == false)
                {
                    if (TBS.player1Hand.active_cards < 1)
                    {
                        for (int i = 0; active_cards > i; i++)
                        {
                            if (active_cards_slots[i].GetComponent<CardDisplay>().card.attack > 0 && cm.attack.Contains(active_cards_slots[i]) == false)
                            {
                                SetToAttack(active_cards_slots[i]);
                            }
                        }
                    }
                    else
                    {
                        for (int i = active_cards+1-TBS.player1Hand.active_cards; active_cards > i || i<0; i++)
                        {
                            print(i);
                            if (i > 0 && active_cards_slots[i].GetComponent<CardDisplay>().card.attack > 0 && cm.attack.Contains(active_cards_slots[i]) == false)
                            {
                                SetToAttack(active_cards_slots[i]);
                            }
                        }
                    }
                    StartCoroutine(Ai_turn_control(TurnBaseScript.TurnState.Response));
                }
            }
        }
        else if (TBS.playerTurn == true && TBS.state == TurnBaseScript.TurnState.Response)
        {
            print("defence phase");

            int defending_cards = 0;
            for (int i = 0; TBS.player1Hand.active_cards > i; i++)
            {
                if (active_cards>i && active_cards_slots[i].GetComponent<CardDisplay>().card.health > 1 && cm.defend.Contains(active_cards_slots[i]) == false)
                    SetToDefend(active_cards_slots[i]);
                defending_cards++;
            }
            if (defending_cards <= 2)
            {
                for (int i = 0; active_cards > i; i++)
                {
                    if (active_cards_slots[i]!=null && cm.defend.Contains(active_cards_slots[i]) == false)
                        SetToDefend(active_cards_slots[i]);
                    defending_cards++;
                    if (defending_cards >= 3)
                        break;
                }
            }
            if (stateTick == false)
            {
                TBS.state = TurnBaseScript.TurnState.EndofBattle;
            //    StartCoroutine(Ai_turn_control(TurnBaseScript.TurnState.End));
            }

        }

        if (TBS.state == TurnBaseScript.TurnState.End || TBS.state == TurnBaseScript.TurnState.TimeWasted)
        {
            firstAttack = true;
        }


    }

    //replaces hand with 7 new cards
    public void pick7()
    {
        //sets cards in hand to 0
        cards_in_hand = 0;
        for (int i = 0; 5 > i; i++)
        {

            //clears the hand
            if (hand[i] != null)
                Destroy(hand[i]);
            hand[i] = null;
            //picks random card from the deak to place it in the hand
            hand[i] = deck.Pick_random(this);
            //creates a card crom the script heald in the deack
            
            hand[i].transform.position = hand_slots[cards_in_hand].position;

            //adds a count to the cards in hand script
            cards_in_hand += 1;
        }
    }

    public void pickCard()
    {
        int i =cards_in_hand;
        //cheask for slots
        if (cards_in_hand <= 6)
        {
            if (hand[i] == null)
            {
                //picks random card from the deak to place it in the hand
                hand[i] = deck.Pick_random(GetComponent<Hand>());
                
                hand[i].transform.position = hand_slots[cards_in_hand].position;
              
                //adds a count to the cards in hand script
                cards_in_hand += 1;
            }
        }
        
        
    }


    //a function used to use a card from the hand
    public void Use_card(int picked_card_index)
    {
        //uses an index int to select a card in the hand and put it in a place holder
        GameObject picked_card = hand[picked_card_index];
        //cheaks if you have enough gold or mana to use tha card
        if (picked_card.GetComponent<CardDisplay>().card.manaCost <= playerMana || picked_card.GetComponent<CardDisplay>().card.manaCost <= playerGold)
        {

            //cheaks if the card is magic or a unit
            if (picked_card.GetComponent<CardDisplay>().card.isSpell == false && active_cards < 5)
            {
                playerGold -= picked_card.GetComponent<CardDisplay>().card.manaCost;
                //moves the card from the hand into the feild
                active_cards_slots[active_cards] = picked_card;
                hand[picked_card_index] = null;
                //updates card count values
                cards_in_hand -= 1;
                active_cards += 1;

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
                fieldCard.Add(picked_card);

                picked_card.GetComponent<card_functions>().isInHand = false;
                TBS.state = TurnBaseScript.TurnState.CardPlayed;
            }
            else
            {
                //magic stuff spell efects are put here



                cards_in_hand -= 1;
                playerMana -= picked_card.GetComponent<CardDisplay>().card.manaCost;

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
            TBS.state = TurnBaseScript.TurnState.CardPlayed;
            TBS.ReadTheCard(picked_card.GetComponent<CardDisplay>().card);
            picked_card.GetComponent<CardDisplay>().card.monsterSickness = true;

            StartCoroutine(unsick(picked_card,this));
        }
    }

    public void SetToAttack(GameObject card)
    {
        print(card.GetComponent<CardDisplay>().card.monsterSickness);
        if (card.GetComponent<CardDisplay>().card.monsterSickness == false)
        {
            card.transform.rotation = Quaternion.Euler(0, 0, 90);
            cm.attack.Add(card);
  //          Debug.Log("Can Attack");
            card.GetComponent<CardDisplay>().card.monsterSickness = true;
        }
    //    else
  //          Debug.Log("Can't Attack");
    }

    public void SetToDefend(GameObject card)
    {
        if (card.GetComponent<CardDisplay>().card.monsterSickness == false)
        {
            card.transform.rotation = Quaternion.Euler(0, 0, 90);
            cm.defend.Add(card);
        }
    }

    public void SendToGrave(GameObject card,int i)
    {
        deck.graveyard.Add(card.GetComponent<CardDisplay>().card);
        active_cards_slots[i]=null;
        active_cards--;
        for(int x=i;active_cards>x; x++)
        {
            active_cards_slots[x] = active_cards_slots[x + 1];
        }
        if (cm.defend.Contains(card))
        {
            cm.DelayedRemoval.Add(card);
        }
        fieldCard.Remove(card);
        Destroy(card);

    }

    public void MonsterSicknessIsOver()
    {
        for (int i = 0; fieldCard.Count > i; i++)
        {
            fieldCard[i].GetComponent<CardDisplay>().card.monsterSickness = false;
        }
    }

    public void UntapTheCards()
    {
        if (fieldCard.Count > 0)
        {
            for (int i = 0; fieldCard.Count > i; i++)
            {
                fieldCard[i].transform.rotation = Quaternion.identity;

            }
        }

        TBS.state = TurnBaseScript.TurnState.PlayerTurn;
    }
    IEnumerator Ai_turn_control(TurnBaseScript.TurnState state)
    {

 
            stateTick = true;
            yield return new WaitForSeconds(0.2f);
            TBS.state = state;
            stateTick = false;
     


    }
    IEnumerator unsick(GameObject card,Hand hand)
    {
        if (hand == TBS.player1Hand) {
            yield return new WaitUntil(()=>TBS.playerTurn==false);
            card.GetComponent<CardDisplay>().card.monsterSickness = false;

        }
        else if((hand == TBS.player2Hand))
        {
            yield return new WaitUntil(()=>TBS.playerTurn==true);
            card.GetComponent<CardDisplay>().card.monsterSickness = false;
        }
    }

}