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
    //used for clickand hold
    float hold;

    //script that manages combat
    public combat_maneger cm;

    //list of game objects in the game canvas
    public List<GameObject> fieldCard;

    //if this hand attacked first
    public bool firstAttack = true;

    //used so the AI dosnt brake the TBS state machine
    bool stateTick=false;
    bool tick = false;

    //used for sellecting groups of cards for spells
    bool selectingCards;
    bool selectingEnemyCards;
    List<GameObject> selectedCards;
    string unitType;

    //sounds
    public AudioSource AS;
    public AudioClip cardDestroy;
    public AudioClip unitplay;
    public AudioClip spellPlay;

    //chosen magic card
    public GameObject chosen_magic_card;
    bool graveTick=false;
    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        deck = GetComponent<Deak>();
        TBS = GameObject.Find("maneger object").GetComponent<TurnBaseScript>();
        cm = GameObject.Find("maneger object").GetComponent<combat_maneger>();

        if (player == true && GameObject.Find("player deck")!=null)
        {
            //copies static deck to the player ingame deck
            GameObject.Find("player deck").GetComponent<player_static_deck>().loadDeck();

        }

        

    }

    // Update is called once per frame
    private void Update()
    {
        if (chosen_magic_card != null)
        {
            chosen_magic_card.transform.position = Vector2.Lerp(chosen_magic_card.transform.position, Vector2.zero, 0.5f);

            if (Vector2.Distance(chosen_magic_card.transform.position, Vector2.zero) < 0.1f && graveTick==false) {
                deck.graveyard.Add(chosen_magic_card.GetComponent<CardDisplay>().card);
                Destroy(chosen_magic_card, 3);
                graveTick = true;
            }
            chosen_magic_card.transform.localScale = Vector3.Lerp(chosen_magic_card.transform.localScale, new Vector3(1,1,0), 0.5f);
            chosen_magic_card.transform.SetAsLastSibling();
        }

        //click and drag check
        if (selectedCard!=null && active == true && TBS.state == TurnBaseScript.TurnState.PlayerTurn && !Input.GetMouseButton(0) && selectedCard.transform.position.y >= active_slots[0].transform.position.y)
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

        //repositions the cards
        for (int i = 0; cards_in_hand > i; i++)
        {
            if (hand[i] == selectedCard && Input.GetMouseButton(0) && player==true)
            {
                Vector3 v = Input.mousePosition;
                v.z = 10;
                hand[i].transform.position = cam.ScreenToWorldPoint(v);
            }
            else
            {
                hand[i].transform.position = Vector3.Lerp(hand[i].transform.position, hand_slots[i].position, 0.5f);
            }
        }
    }



    void LateUpdate()
    {
        
        

        if(TBS.state != TurnBaseScript.TurnState.EndofBattle)
        {
            for (int i = 0; active_cards > i; i++)
            {

                active_cards_slots[i].transform.position = Vector3.Lerp(active_cards_slots[i].transform.position, active_slots[i].position, 0.5f);

                //card deaths
                if (active_cards_slots[i].GetComponent<CardDisplay>().card.health <= 0 && active_cards_slots[i].GetComponent<CardDisplay>().card.isSpell == false)
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
            if (selectedCard != null && chosen_magic_card==null && TBS.player2Hand.chosen_magic_card==null)
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

            //for selecting groups of cards
            if (selectingCards == true)
            {
                if(selectedCard != null && Input.GetMouseButtonDown(0))
                {
                    for (int i=0; active_cards_slots.Length > i; i++)
                    {
                        if (active_cards_slots[i] == selectedCard)
                        {
                            if (unitType != "unit")
                            {
                                string Tags;
                                Tags = selectedCard.GetComponent<CardDisplay>().card.Tags;
                                string[] splitTags = Tags.Split(' ','_');
                                for (int x = 0; splitTags.Length > x; x++)
                                {
                                    if (splitTags[x] == unitType)
                                    {
                                        selectedCards.Add(selectedCard);
                                    }
                                }
                            }
                            else
                            {
                                selectedCards.Add(selectedCard);
                            }
                        }
                    }
                }
            }
            if (selectingEnemyCards == true)
            {
                if (selectedCard != null && Input.GetMouseButtonDown(0))
                {
                    for (int i = 0; TBS.player2Hand.active_cards > i; i++)
                    {
                        if (TBS.player2Hand.active_cards_slots[i] == selectedCard)
                        {
                            selectedCards.Add(selectedCard);
                        }
                    }
                }
            }
            else
            {

                

                //clikink cards
                if (selectedCard != null && Input.GetMouseButtonDown(0))
                {
                    

                    //for seting cards from feild to actack or defend
                    for (int i = 0; active_cards > i; i++)
                    {
                        if (selectedCard == active_cards_slots[i])
                        {
                            
                            //visual change in card goes here
                            if (active == true && TBS.state == TurnBaseScript.TurnState.Attack && cm.attack.Contains(selectedCard) == false)
                            {
                                //this function adds the cards to a list that the combat maneger uses
                                SetToAttack(selectedCard);
                            }
                            else if (active == false && TBS.state == TurnBaseScript.TurnState.Response && cm.deffendingCardsRef.Contains(selectedCard) == false)
                            {
                                //this function adds the cards to a list that the combat maneger uses and rtates the card 90 degres
                                StartCoroutine(WaitToDefend(selectedCard));
                                print("working");
                            }
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
                   
                    if (cards_in_hand >= 1 && tick==false)
                    {

                        StartCoroutine(DelayAIUsecard());
                        tick = true;

                    }
                    else
                    {

                        
                      // StartCoroutine(Ai_turn_control(TurnBaseScript.TurnState.Attack));

                    }


                }

            }
            if (TBS.state == TurnBaseScript.TurnState.Attack)
            {
                //    print("attack phase");
                tick = false;
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
                            if (i > 0 && active_cards_slots[i].GetComponent<CardDisplay>().card.attack > 0 && cm.attack.Contains(active_cards_slots[i]) == false)
                            {
                                SetToAttack(active_cards_slots[i]);
                            }
                        }
                    }
                    if (cm.attack.Count != 0)
                    {
                        StartCoroutine(Ai_turn_control(TurnBaseScript.TurnState.Response));
                    }
                    else
                    {
                        TBS.state = TurnBaseScript.TurnState.End;
                    }
                }
            }
        }
        else if (TBS.playerTurn == true && TBS.state == TurnBaseScript.TurnState.Response)
        {
        //    print("defence phase");

            int defending_cards = 0;
            for (int i = 0; TBS.player1Hand.active_cards > i; i++)
            {
                if (active_cards>i && active_cards_slots[i].GetComponent<CardDisplay>().card.health > 1 && cm.deffendingCardsRef.Contains(active_cards_slots[i]) == false)
                    SetToDefend(active_cards_slots[i],Random.Range(0,cm.attack.Count));
                defending_cards++;
            }
            if (defending_cards <= 2)
            {
                for (int i = 0; active_cards > i; i++)
                {
                    if (active_cards_slots[i] != null && cm.deffendingCardsRef.Contains(active_cards_slots[i]) == false)
                    {
                        SetToDefend(active_cards_slots[i], Random.Range(0, cm.attack.Count + 1));
                        defending_cards++;
                    }
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
            
           // hand[i].transform.position = hand_slots[cards_in_hand].position;

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
        if (TBS.state == TurnBaseScript.TurnState.PlayerTurn )
        {

            //cheaks if the card is magic or a unit
            if (picked_card.GetComponent<CardDisplay>().card.isSpell == false && active_cards < 5 && picked_card.GetComponent<CardDisplay>().card.manaCost <= playerGold)
            {

                AS.clip = unitplay;
                AS.Play();


                playerGold -= picked_card.GetComponent<CardDisplay>().card.manaCost;
                //moves the card from the hand into the feild
                active_cards_slots[active_cards] = picked_card;
                hand[picked_card_index] = null;
                //updates card count values
                cards_in_hand -= 1;
                active_cards += 1;

                picked_card.gameObject.transform.position = Vector2.zero;

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

                string Decription = picked_card.GetComponent<CardDisplay>().card.description;
                string[] x = Decription.Split(' ');
                bool Haste = false;
                for (int i = 0; x.Length > i; i++)
                {
                    if (x[i] == "Haste")
                    {
                        picked_card.GetComponent<CardDisplay>().card.monsterSickness = false;
                        Haste = true;
                    }
                }
                if (Haste == false)
                {
                    picked_card.GetComponent<CardDisplay>().card.monsterSickness = true;
                }

                StartCoroutine(unsick(picked_card, this));
                picked_card.GetComponent<CardDisplay>().hide = false;
                picked_card.GetComponent<CardDisplay>().active = true;
                TBS.ReadTheCard(picked_card.GetComponent<CardDisplay>().card);
            

            }
            else if (picked_card.GetComponent<CardDisplay>().card.isSpell == true && picked_card.GetComponent<CardDisplay>().card.manaCost <= playerMana)
            {
                
                AS.clip = spellPlay;
                AS.Play();
                //magic stuff spell efects are put here
                playerMana -= picked_card.GetComponent<CardDisplay>().card.manaCost;

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


                TBS.state = TurnBaseScript.TurnState.CardPlayed;
                TBS.ReadTheCard(picked_card.GetComponent<CardDisplay>().card);
                picked_card.GetComponent<CardDisplay>().hide = false;
                picked_card.GetComponent<CardDisplay>().active = true;

                chosen_magic_card = picked_card;

                graveTick = false;
            }
        } 
    }

    public void SetToAttack(GameObject card)
    {
        string Decription = card.GetComponent<CardDisplay>().card.description;
        string[] y = Decription.Split(' ');
        bool imobile = false;
        for (int i = 0; y.Length > i; i++)
        {
            if (y[i] == "Immobile")
                imobile = true;
        }
        if (imobile == false)
        {

            if (card.GetComponent<CardDisplay>().card.monsterSickness == false)
            {
                string Decriptionx = card.GetComponent<CardDisplay>().card.description;
                string[] x = Decriptionx.Split(' ');
                bool Charged = false;
                for (int i = 0; x.Length > i; i++)
                {
                    if (x[i] == "Charged")
                    {
                        Charged = true;
                    }
                }
                if (Charged == false)
                {
                    
               
                    card.GetComponent<CardDisplay>().card.monsterSickness = true;

                }
                card.transform.rotation = Quaternion.Euler(0, 0, 90);

                cm.attack.Add(card);

                card.GetComponent<CardDisplay>().attack_defend = 1;
            }
        }
    //    else
  //          Debug.Log("Can't Attack");
    }

    public void SetToDefend(GameObject card, int card_to_defend)
    {
        if (card.GetComponent<CardDisplay>().card.monsterSickness == false && cm.defend[card_to_defend]==null)
        {
            card.transform.rotation = Quaternion.Euler(0, 0, 90);
            cm.deffendingCardsRef.Add(card);
            cm.defend[card_to_defend] = card;
            card.GetComponent<CardDisplay>().attack_defend = 2;
        }
    }

    public void SendToGrave(GameObject card, int i)
    {
        deck.graveyard.Add(card.GetComponent<CardDisplay>().card);
        active_cards_slots[i] = null; 
        active_cards--;

        for (int x = i; active_cards > x; x++)
        {
            active_cards_slots[x] = active_cards_slots[x + 1];
        }
        
        fieldCard.Remove(card);
        AS.clip = cardDestroy;
        AS.Play();

        Destroy(card);


    }

    public void MonsterSicknessIsOver()
    {
        for (int i = 0; active_cards > i; i++)
        {
            if (active_cards_slots[i].GetComponent<CardDisplay>().card.disabeled == false)
            {
                active_cards_slots[i].GetComponent<CardDisplay>().card.monsterSickness = false;
            }
            else
            {
                active_cards_slots[i].GetComponent<CardDisplay>().card.disabeled = false;
            }
        }
    }

    public void UntapTheCards()
    {
        if (active_cards > 0)
        {
            for (int i = 0; active_cards > i; i++)
            {
                active_cards_slots[i].transform.rotation = Quaternion.identity;

            }
        }

        TBS.state = TurnBaseScript.TurnState.PlayerTurn;
    }
    IEnumerator Ai_turn_control(TurnBaseScript.TurnState state)
    {

        if (active == true)
        {
            int x = 0;
            if (state != TurnBaseScript.TurnState.Attack)
            {
                
                x = 1;
            }
            stateTick = true;
            yield return new WaitForSeconds(x);
            TBS.state = state;
            stateTick = false;
        }
            


    }
    public IEnumerator unsick(GameObject card,Hand hand)
    {
        if (hand == TBS.player1Hand) {
            yield return new WaitUntil(()=>TBS.playerTurn==false);
            if (card != null)
            {
                card.GetComponent<CardDisplay>().card.monsterSickness = false;
            }
        }
        else if((hand == TBS.player2Hand))
        {
            yield return new WaitUntil(()=>TBS.playerTurn==true);
            if (card != null)
            {
                card.GetComponent<CardDisplay>().card.monsterSickness = false;
            }
        }
    }

    public IEnumerator sellectCards(List<GameObject> cardSelection,string cardtype)
    {
        unitType = cardtype;
        selectedCards = cardSelection;
        selectingCards = true;
        yield return new WaitUntil(() => TBS.state != TurnBaseScript.TurnState.CardPlayed);
        selectingCards = false;
    }

    public IEnumerator sellectEnemyCards(List<GameObject> cardSelection)
    {
        selectedCards = cardSelection;
        selectingEnemyCards = true;
        yield return new WaitUntil(() => TBS.state != TurnBaseScript.TurnState.CardPlayed);
        selectingEnemyCards = false;
    }

    IEnumerator WaitToDefend(GameObject defendingCard)
    {
        print("waiting");
        List<GameObject> attackingcard = new List<GameObject>();
        StartCoroutine(selectAttackingCard(attackingcard));
        yield return new WaitUntil(()=> attackingcard.Count==1);
        GameObject ac =attackingcard[0];

        for(int i=0;cm.attack.Count>i; i++)
        {
            if(cm.attack[i]==ac)
                SetToDefend(defendingCard, i);
        }
    }

    IEnumerator selectAttackingCard(List<GameObject> cardselected)
    {
        selectedCards = cardselected;
        selectingEnemyCards = true;
        yield return new WaitUntil(() =>cardselected.Count==1);
        selectingEnemyCards = false;
    }

    IEnumerator DelayAIUsecard()
    {
        for (int i = 0; 10 > i; i++)
        {
            Use_card(Random.Range(0, cards_in_hand));
            yield return new WaitForSeconds(0.2f);
        }

        if (stateTick == false)
        {
            print("1");
            StartCoroutine(Ai_turn_control(TurnBaseScript.TurnState.Attack));
        }


    }
}