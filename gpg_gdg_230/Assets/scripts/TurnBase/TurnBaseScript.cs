using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * The main purpose of this script is so that everything works in order.
 */

public class TurnBaseScript : MonoBehaviour
{
    #region Data
    //To make sure that the turn are played properly.
    public enum TurnState { StartTurn, PlayerTurn, Untap, CardPlayed, Response, Attack, End, Nothing, TimeWasted, EndofBattle }
    public TurnState state = TurnState.Nothing;

    public card_reffence Cr;

    public Hand player1Hand;
    public Hand player2Hand;

    //The main players health base.
    public int player1Health = 20;
    public int player2Health = 20;

    public Text player1HealthText;
    public Text player2HealthText;

    //Making sure to see which player turn it is.
    public bool playerTurn = false;

    //The timer to make sure players don't spend too long on it.
    public int turnTimer = 30;
    private int reduceTime1 = 30;
    private int reduceTime2 = 30;
    private int player1AFKStrike;
    private int player2AFKStrike;

    //To prevent this from overlaping
    public bool timerIsOn = false;

    //To see which player goes first when the game starts.
    public int whoGoesFirst;

    public bool startOfTheGame = true;

    private int defaultGold;
    private int defaultMana1;
    private int defaultMana2;

    public Text player1CoinText;
    public Text player2CoinText;
    public Text player1ManaText;
    public Text player2ManaText;

    //Using so that there is a delay when it comes to see what it does.
    private int actiontime = 1 ;

    public GameObject attackButton;
    private bool hasAttack = false;
    public GameObject[] buttons;

    public GameObject player_active_ui;

    public GameObject AI_active_ui;
    //added by milan
    public int turns = 0;


    public AudioSource AS;

    public AudioClip win;
    public AudioClip lose;

    // Start is called before the first frame update
    void Start()
    {
        player1HealthText.text = player1Health.ToString();
        player2HealthText.text = player2Health.ToString();

        whoGoesFirst = Random.Range(1, 10);
        if (whoGoesFirst <= 5)
            playerTurn = true;
        else
            playerTurn = false;

        state = TurnState.StartTurn;

    }
    #endregion

    #region Update
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            //For when the turn starts for a player.
            case (TurnState.StartTurn):


                if (startOfTheGame == true)
                {
                    if (playerTurn == true)
                    {
                        player1Hand.active = true;
                        player2Hand.active = false;
                        player_active_ui.SetActive(true);
                        AI_active_ui.SetActive(false);
                    }
                    else
                    {
                        player1Hand.active = false;
                        player2Hand.active = true;
                        player_active_ui.SetActive(false);
                        AI_active_ui.SetActive(true);
                    }
                    Draw7();
                }
                else
                {
                    //need to make the player draw 1
                    if (playerTurn == true)
                    {
                        player1Hand.active = true;
                        player2Hand.active = false;
                        player1Hand.pickCard();
                        //                        Debug.Log("Player 1 drew a card");
                    }
                    else
                    {
                        player1Hand.active = false;
                        player2Hand.active = true;
                        player2Hand.pickCard();
                        //                        Debug.Log("Player 2 drew a card");
                    }
                    if (playerTurn == true)
                    {
                        player_active_ui.SetActive(true);
                        AI_active_ui.SetActive(false);
                        if (turns >= 2)
                            player1Hand.MonsterSicknessIsOver();

                    }
                    else
                    {
                        player_active_ui.SetActive(false);
                        AI_active_ui.SetActive(true);
                        if (turns >= 2)
                            player2Hand.MonsterSicknessIsOver();

                    }
                }
                GainManaAndCoin();
                state = TurnState.Untap;
                break;
            case (TurnState.Untap):
                UntapCard();
                // state = TurnBaseScript.TurnState.PlayerTurn;

                //-------------------------------------------------------------------------------------------added stuff here
                // player2Hand.MonsterSicknessIsOver();
                // player1Hand.MonsterSicknessIsOver();

                break;
            case (TurnState.PlayerTurn):
                if (playerTurn == true)
                {
                    buttons[2].gameObject.SetActive(true);
                    buttons[0].gameObject.SetActive(true);
                    buttons[1].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(false);
                }
                else
                {
                    buttons[2].gameObject.SetActive(false);
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(false);
                }
                if (hasAttack == true)
                {
                    buttons[2].gameObject.SetActive(false);
                }
                //Making the turn timer, we need some more work on.
                if (timerIsOn == false)
                {
                    StartCoroutine("CountDown");
                    timerIsOn = true;
                }
                if (turnTimer <= 0)
                {
                    StopCoroutine("CountDown");
                    timerIsOn = false;

                    state = TurnState.TimeWasted;
                }
                //attackButton.SetActive(true);



                break;
            case (TurnState.Attack):
                if (playerTurn == true)
                {
                    buttons[2].gameObject.SetActive(false);
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(true);
                }
                else
                {
                    buttons[2].gameObject.SetActive(false);
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(false);
                }
                hasAttack = true;
                break;

            case (TurnState.Response):
                if (playerTurn == false)
                {
                    buttons[2].gameObject.SetActive(false);
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(true);
                    buttons[3].gameObject.SetActive(false);
                }
                else
                {
                    buttons[2].gameObject.SetActive(false);
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(false);
                }
                PlayerResponeToAction();

                break;

            case (TurnState.End):

                hasAttack = false;

                turns += 1;
                turnTimer = 30;

                if (playerTurn == true)
                {
                    player1AFKStrike = 0;
                    reduceTime1 = 30;

                    playerTurn = false;

                }
                else
                {
                    player2AFKStrike = 0;
                    reduceTime2 = 30;

                    playerTurn = true;

                }
                //       Debug.Log("Player 2 Health is " + player2Health);
                state = TurnState.StartTurn;
                break;

            case (TurnState.Nothing):

                break;

            case (TurnState.TimeWasted):
                if (playerTurn == true)
                    player1AFKStrike += 1;
                else
                    player2AFKStrike += 1;
                TimerEndTurn();
                break;

            case (TurnState.CardPlayed):
                if (actiontime <= 0)
                {
                    StopCoroutine("ActionCountDown");
                    if (state == TurnState.CardPlayed)
                    {
                        state = TurnState.PlayerTurn;
                    }
                
                }
                break;

        }

        if (player1Health <= 0 || player2Health <= 0)
        {
            if (state != TurnState.Nothing)
            {
                GameIsOver();
            }
        }

        
    }
    #endregion

    #region Actions
    void Draw7()
    {
        player2Hand.pick7();
        player1Hand.pick7();
        startOfTheGame = false;
    }

    //This is to so that each player will gain Gold each turn while only gain Mana at the start of the player's turn.
    void GainManaAndCoin()
    {
        player1Hand.playerGold = defaultGold;
        player2Hand.playerGold = defaultGold;

        player1Hand.playerGold += 1;
        player2Hand.playerGold += 1;
        defaultGold += 1;

        player1Hand.playerMana = defaultMana1;
        player2Hand.playerMana = defaultMana2;

        if (playerTurn == true)
        {
            player1Hand.playerMana += 1;
            defaultMana1 += 1;
        }
        else
        {
            player2Hand.playerMana += 1;
            defaultMana2 += 1;
        }

        if (player1Hand.playerGold >= 10)
            player1Hand.playerGold = 10;
        if (player2Hand.playerGold >= 10)
            player2Hand.playerGold = 10;
        if (player1Hand.playerMana >= 5)
            player1Hand.playerMana = 5;
        if (player2Hand.playerMana >= 5)
            player2Hand.playerMana = 5;

        player1CoinText.text = player1Hand.playerGold.ToString();
        player1ManaText.text = player1Hand.playerMana.ToString();
        player2CoinText.text = player2Hand.playerGold.ToString();
        player2ManaText.text = player2Hand.playerMana.ToString();

    }

    void PlayerResponeToAction()
    {
        turnTimer = 30;
    }

    public void ResponeToAttacking()
    {
        state = TurnState.Response;
    }

    public void GoingForAttack()
    {
        state = TurnState.Attack;
    }

    public void Battle()
    {
        state = TurnState.EndofBattle;
    }

    public void EndPlayerTurn()
    {
        state = TurnState.End;
    }

    //This is use for when the player is AFK 
    //If there are AFK for too long, then they will lose.
    public void TimerEndTurn()
    {
        hasAttack = false;

        if (player1AFKStrike == 3)
            GameIsOver();
        if (player2AFKStrike == 3)
            GameIsOver();

        if (state == TurnState.TimeWasted)
        {
            turnTimer = 30;
            if (playerTurn == true)
            {
                playerTurn = false;
                if (player2AFKStrike >= 1)
                {
                    reduceTime2 = reduceTime2 / 2;
                    turnTimer = reduceTime2;
                }
                state = TurnState.StartTurn;
            }
            else if (playerTurn == false)
            {
                playerTurn = true;
                if (player1AFKStrike >= 1)
                {
                    reduceTime1 = reduceTime1 / 2;
                    turnTimer = reduceTime1;
                }
                state = TurnState.StartTurn;
            }
        }

    }

    //Once the game is done.
    public void GameIsOver()
    {
        if (player1AFKStrike == 3)
        {
            Debug.Log("Player 1 lose");
            AS.clip = lose;
            AS.loop = false;
            AS.Play();

            for (int i = 0; buttons.Length > i; i++)
            {
                buttons[i].SetActive(false);
            }
        }
        if (player2AFKStrike == 3)
        {
            AS.clip = win;
            AS.loop = false;
            AS.Play();
            Debug.Log("Player 2  lose");
            for (int i = 0; buttons.Length > i; i++)
            {
                buttons[i].SetActive(false);
            }
        }

        if (player1Health <= 0)
        {
            AS.clip = lose;
            AS.loop = false;
            AS.Play();
            Debug.Log("Player 1 Lose");
            for (int i = 0; buttons.Length > i; i++)
            {
                buttons[i].SetActive(false);
            }
        }
        if (player2Health <= 0)
        {
            AS.clip = win;
            AS.loop = false;
            AS.Play();
            Debug.Log("Player 2 Lose");
            for (int i = 0; buttons.Length > i; i++)
            {
                buttons[i].SetActive(false);
            }
        }

        state = TurnState.Nothing;
    }

    //This is so that the player doesn't take too long
    IEnumerator CountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            turnTimer--;
        }

    }
    IEnumerator ActionCountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            actiontime--;
        }

    }



    public void UntapCard()
    {
        if (playerTurn == true)
            player1Hand.UntapTheCards();
        else
            player2Hand.UntapTheCards();
    }

    public void HasAttack()
    {
        player1Hand.firstAttack = false;
    }
    #endregion

    //This will be use in order to not only read the card effect, 
    // but to also use it if it has one.
    public void ReadTheCard(ScriptableCard card)
    {
        StopCoroutine("CountDown");
        timerIsOn = false;
        
        buttons[0].gameObject.SetActive(false);
        buttons[2].gameObject.SetActive(false);

        player1CoinText.text = player1Hand.playerGold.ToString();
        player1ManaText.text = player1Hand.playerMana.ToString();
        player2CoinText.text = player2Hand.playerGold.ToString();
        player2ManaText.text = player2Hand.playerMana.ToString();
        string[] message = card.description.Split(' ');

        if(message[0]=="Spell:" || message[0]== "sacrifice:")
        {
            actiontime = 3;
        }
        else if(message[0]=="On" && message[1] == "Play:")
        {
            actiontime = 1;
        }
        else
        {
            actiontime = 0;
        }

        StartCoroutine("ActionCountDown");

        FutureFur_Uniqe_pram();

        
        for (int i = 0; message.Length > i; i++)
        {
            switch (message[i])
            {
                case ".":
                    switch (message[i + 1])
                    {
                        //unitytype(e.g. "Trooper") / "for" / damage
                        case "Damage":
                            typeDamage(message[i + 2], message[i + 4]);
                            break;

                        //"all"/ unitytype / attack / "attack" / deffence / "deffence"
                        case "Boost":
                            boost(message[i + 3], message[i + 4], message[i + 6]);
                            break;

                        // unit count/ unit name    
                        case "Summon":
                            PasSummon(message[i + 2], message[i + 3]);
                            break;
                        //gold / "Gold" / mana / "Power"
                        case "Earn":
                            earn(message[i + 2], message[i + 4]);
                            break;
                        //"all" / unit type/ "with" / enhancement
                        case "Enhance":
                            Enhance(message[i + 2], message[i + 4]);
                            break;

                        //number of units / units / for / damage
                        case "damage":
                            spellDamage(message[i + 1], message[i + 4]);
                            break;

                        //number of units / unit type / "attack" / damage / "defence" / deffence
                        case "upgrade":
                            upgrade(message[i + 1], message[i + 2], message[i + 4], message[i + 6]);
                            break;

                        //unit count / unit name
                        case "summon":
                            summon(message[i + 1], message[i + 2]);
                            break;
                        //gold / "Gold" / mana / "Power"
                        case "earn":
                            earn(message[i + 1], message[i + 3]);
                            break;
                        // number of units
                        case "convert":
                            convert(message[i + 1]);
                            break;
                        // number of units
                        case "dissable":
                            disable(message[i + 1]);
                            break;
                    }
                    break;

                case "On":

                    switch (message[i + 1])
                    {
                        case "Play:":
                            switch (message[i + 2])
                            {
                                //unitytype(e.g. "Trooper") / "for" / damage
                                case "Damage":
                                    typeDamage(message[i + 3], message[i + 5]);
                                    break;

                                //"all"/ unitytype / attack / "attack" / deffence / "deffence"
                                case "Boost":
                                    boost(message[i + 4], message[i + 5], message[i + 7]);
                                    break;

                                // unit count/ unit name    
                                case "Summon":
                                    PasSummon(message[i + 3], message[i + 4]);
                                    break;
                                //gold / "Gold" / mana / "Power"
                                case "Earn":
                                    earn(message[i + 3], message[i + 5]);
                                    break;
                                //"all" / unit type/ "with" / enhancement
                                case "Enhance":
                                    Enhance(message[i + 3], message[i + 5]);
                                    break;                                
                            }
                            break;
                    }
                    break;
            
                

                case "Spell:":


                    switch (message[i+1])
                    {
                        //number of units / units / for / damage
                        case "damage":
                            spellDamage(message[i+2], message[i+5]);
                            break;

                        //number of units / unit type / "attack" / damage / "defence" / deffence
                        case "upgrade":
                            upgrade(message[i+2], message[i+3], message[i+5], message[i+7]);
                            break;

                        //unit count / unit name
                        case "summon":
                            summon(message[i+2], message[i+3]);
                            break;
                        //gold / "Gold" / mana / "Power"
                        case "earn":
                            earn(message[i + 2], message[i + 4]);
                            break;
                        // number of units
                        case "convert":
                            convert(message[i+2]);
                            break;
                        // number of units
                        case "dissable":
                            disable(message[i+2]);
                            break;

                        case "Enhance":
                            SpellEnhance();
                            break;
                    }
                    break;

                //number of units / unit type
                case "sacrifice:":
                    sacrifice(message[i+1], message[i+2], message, i);


                    break;
            }
        }
    }
    #region passives

    void Enhance(string unit_type, string enhancement)
    {
        if (playerTurn == true)
        {
            for (int i = 0; player1Hand.active_cards > i; i++)
            {
                string nam = player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.name;
                string[] brokenName = nam.Split(' ');
                for (int a = 0; brokenName.Length > a; a++)
                {
                    if (brokenName[a] == unit_type)
                    {
                        player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.description += " " + enhancement;
                        player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().descriptionText.text += " " + enhancement;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; player2Hand.active_cards > i; i++)
            {
                string nam = player2Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.name;
                string[] brokenName = nam.Split(' ');
                for (int a = 0; brokenName.Length > a; a++)
                {
                    if (brokenName[a] == unit_type)
                    {
                        player2Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.description += " " + enhancement;
                        player2Hand.active_cards_slots[i].GetComponent<CardDisplay>().descriptionText.text += " " + enhancement;
                    }
                }
            }
        }
    }

    void typeDamage(string target,string damage)
    {
        if (playerTurn == true)
        {
            if (target == "all")
            {
                for (int i = 0; player2Hand.active_cards > i; i++)
                {
                    player2Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.health -= int.Parse(damage);
                }
            }
            else
            {
                for (int i = 0; player2Hand.active_cards > i; i++)
                {
                    string nam = player2Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.name;
                    string[] brokenName = nam.Split(' ');
                    for (int a = 0; brokenName.Length > a; a++)
                    {
                        if (brokenName[a] == target)
                        {
                            player2Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.health -= int.Parse(damage);
                        }
                    }
                }
            }
            
        }
        else
        {
            if (target == "all")
            {
                for (int i = 0; player1Hand.active_cards > i; i++)
                {
                    player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.health -= int.Parse(damage);
                }
            }
            else
            {
                for (int i = 0; player1Hand.active_cards > i; i++)
                {
                    string nam = player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.name;
                    string[] brokenName = nam.Split(' ');
                    for (int a = 0; brokenName.Length > a; a++)
                    {
                        if (brokenName[a] == target)
                        {
                            player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.health -= int.Parse(damage);
                        }
                    }
                }
            }
        }
    }
    void boost(string unittype, string attack, string deffence)
    {
        if (playerTurn == true)
        {
            for (int i = 0; player1Hand.active_cards > i; i++)
            {
                string nam = player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.name;
                string[] brokenName = nam.Split(' ');
                for (int a = 0; brokenName.Length > a; a++)
                {
                    if (brokenName[a] == unittype)
                    {
                        player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.health += int.Parse(deffence);
                        player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.attack += int.Parse(attack);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; player2Hand.active_cards > i; i++)
            {
                string nam = player2Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.name;
                string[] brokenName = nam.Split(' ');
                for (int a = 0; brokenName.Length > a; a++)
                {
                    if (brokenName[a] == unittype)
                    {
                        player2Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.health += int.Parse(deffence);
                        player2Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.attack += int.Parse(attack);
                    }
                }
            }
        }
    }

    void PasSummon(string unitcount, string unit)
    {
        if (playerTurn == true)
        {
            for (int i = 0; int.Parse(unitcount) > i; i++)
            {
                if (player1Hand.active_cards < 5)
                {
                    GameObject x = Cr.create_card(unit);
                    player1Hand.active_cards_slots[player1Hand.active_cards] = x;
                    player1Hand.active_cards++;
                    StartCoroutine(player1Hand.unsick(x, player1Hand));
                }
            }
        }
        else
        {
            for (int i = 0; int.Parse(unitcount) > i; i++)
            {
                if (player2Hand.active_cards < 5)
                {
                    GameObject x = Cr.create_card(unit);
                    player2Hand.active_cards_slots[player2Hand.active_cards] = x;
                    player2Hand.active_cards++;
                    StartCoroutine(player1Hand.unsick(x, player2Hand));
                }
            }
        }
    }
    #endregion
    #region spells

    void earn(string Gold,string Mana)
    {
        if (playerTurn == true)
        {
            player1Hand.playerGold += int.Parse(Gold);
            player1Hand.playerMana += int.Parse(Mana);
        }
        else
        {
            player2Hand.playerGold += int.Parse(Gold);
            player2Hand.playerMana += int.Parse(Mana);
        }
    }

    void spellDamage(string target, string Damage)
    {
        if (playerTurn == true)
        {
            if (target == "all")
            {

                for (int i = 0; player2Hand.active_cards > i; i++)
                {
                    string Decription = player2Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.description;
                    string[] x = Decription.Split(' ');
                    bool enduring = false;
                    for (int y = 0; x.Length > y; y++)
                    {
                        if (x[y] == "Enduring")
                        {
                            enduring = true;
                        }
                    }
                    if (enduring == false)
                        player2Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.health -= int.Parse(Damage);
                }
            }
            else
            {
                List<GameObject> SelectedCards = new List<GameObject>();
                StartCoroutine(waitforDamage(int.Parse(target), SelectedCards, int.Parse(Damage)));
                StartCoroutine(pause_sellection_outher_hand(int.Parse(target), SelectedCards));
            }
        }
        else
        {
            int effectedCards = 0;
            for (int i = 0; player1Hand.active_cards > i; i++)
            {
                string Decription = player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.description;
                string[] x = Decription.Split(' ');
                bool enduring = false;
                
                for (int y = 0; x.Length > y; i++)
                {
                    if (x[i] == "Enduring")
                    {
                        enduring = true;
                    }
                }
                if (enduring == false) {
                    player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.health -= int.Parse(Damage);
                    effectedCards++;
                    if (effectedCards == int.Parse(target))
                        i = player1Hand.active_cards;
                }
            }
            
        }
    }
    void sacrifice(string nuber, string unit_type, string[] spell, int start)
    {
        if (playerTurn == true)
        {
            List<GameObject> SelectedCards = new List<GameObject>();
            StartCoroutine(pause_sellection_own_hand(int.Parse(nuber), unit_type, SelectedCards));
            StartCoroutine(player_sacrifice(int.Parse(nuber), SelectedCards,spell,start));
        }
        else
        {
            for (int i = 0; player2Hand.active_cards > i; i++)
            {
                string Decription = player2Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.name;
                string[] x = Decription.Split(' ');
                List<GameObject> SelectedCards = new List<GameObject>();
                for (int y = 0; x.Length > y; i++)
                {
                    if (x[i] == unit_type)
                    {
                        SelectedCards.Add(player2Hand.active_cards_slots[i]);
                    }
                    if (SelectedCards.Count == int.Parse(nuber))
                    {
                        i = player2Hand.active_cards;
                        scrificeComplete(spell,start);
                    }
                }
            }
        }
    }
    void scrificeComplete(string[] message,int start)
    {
        switch (message[start+2 + 1])
        {
            //number of units / damage
            case "damage":
                spellDamage(message[start+2 + 2], message[start+2 + 3]);
                break;

            //number of units / unit type / "attack" / damage / "defence" / deffence
            case "upgrade":
                upgrade(message[start+2 + 2], message[start+2 + 3], message[start+2 + 5], message[start+2 + 7]);
                break;

            //unit count / unit name
            case "summon":
                summon(message[start+2 + 2], message[start+2 + 3]);
                break;

            case "earn":
                //need refrence from G
                break;
            // number of units
            case "convert":
                convert(message[start+2 + 2]);
                break;
            // number of units
            case "dissable":
                disable(message[start+2 + 2]);
                break;
        }
    }

    void SpellEnhance()
    {
        if (playerTurn == true)
        {
            List<GameObject> SelectedCards = new List<GameObject>();
            StartCoroutine(pause_sellection_own_hand(1, "none", SelectedCards));
            StartCoroutine(WaitForEnhance(SelectedCards));

        }
    }
    void upgrade(string nuber, string unit_type, string attack, string health)
    {
        if (playerTurn == true)
        {
            if (nuber != "all")
            {
                List<GameObject> SelectedCards = new List<GameObject>();
                StartCoroutine(pause_sellection_own_hand(int.Parse(nuber), unit_type, SelectedCards));
                StartCoroutine(waitForUpgrade(int.Parse(nuber), SelectedCards, int.Parse(attack), int.Parse(health)));
            }
            else
            {
                List<GameObject> selectedCards = new List<GameObject>();
                for (int i = 0; player1Hand.active_cards > i; i++)
                {
                    if (nuber == "units")
                    {
                        selectedCards.Add(player2Hand.active_cards_slots[i]);
                    }
                    else
                    {
                        string n = player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.name;
                        string[] brokenName = n.Split(' ');
                        for (int x = 0; brokenName.Length > x; x++)
                        {
                            if (brokenName[x] == unit_type)
                                selectedCards.Add(player1Hand.active_cards_slots[i]);

                        }
                    }
                }
                for (int i = 0; selectedCards.Count > i; i++)
                {
                    selectedCards[i].GetComponent<CardDisplay>().card.health += int.Parse(health);
                    selectedCards[i].GetComponent<CardDisplay>().card.attack += int.Parse(attack);
                }
            }
        }
        else
        {
            if (nuber == "all")
            {
                List<GameObject> selectedCards = new List<GameObject>();


                for (int i = 0; player2Hand.active_cards > i; i++)
                {
                    if (nuber == "units")
                    {
                        selectedCards.Add(player2Hand.active_cards_slots[i]);
                    }
                    else
                    {
                        string n = player2Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.name;
                        string[] brokenName = n.Split(' ');
                        for (int x = 0; brokenName.Length > x; x++)
                        {
                            if (brokenName[x] == unit_type)
                                selectedCards.Add(player2Hand.active_cards_slots[i]);

                        }
                    }
                }
                for (int i = 0; selectedCards.Count > i; i++)
                {
                    selectedCards[i].GetComponent<CardDisplay>().card.health += int.Parse(health);
                    selectedCards[i].GetComponent<CardDisplay>().card.attack += int.Parse(attack);

                }
            
            }
            else
            {
                List<GameObject> selectedCards = new List<GameObject>();
                for (int i = 0; player2Hand.active_cards > i; i++)
                {
                    string n = player2Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.name;
                    string[] brokenName = n.Split(' ');
                    for (int x = 0; brokenName.Length > x; x++)
                    {
                        if (brokenName[x] == unit_type)
                            selectedCards.Add(player2Hand.active_cards_slots[i]);
                        if (selectedCards.Count == int.Parse(nuber))
                            i = player2Hand.active_cards;
                    }
                }
                for (int i = 0; selectedCards.Count > i; i++)
                {
                    selectedCards[i].GetComponent<CardDisplay>().card.health += int.Parse(health);
                    selectedCards[i].GetComponent<CardDisplay>().card.attack += int.Parse(attack);
                }
            }
        }
    }

    void convert(string units)
    {
        if (playerTurn == true)
        {
            List<GameObject> SelectedCards = new List<GameObject>();
            StartCoroutine(waitForConvert(int.Parse(units), SelectedCards));
            StartCoroutine(pause_sellection_outher_hand(int.Parse(units), SelectedCards));
        }
        else
        {
            for (int i = 0; int.Parse(units) > i; i++)
            {
                if (player2Hand.active_cards < 5)
                {
                    int card = Random.Range(0, player1Hand.active_cards);
                    player2Hand.active_cards_slots[player2Hand.active_cards - 1] = player1Hand.active_cards_slots[card];
                    player1Hand.active_cards_slots[card] = null;
                    player2Hand.active_cards++;
                    player1Hand.active_cards--;

                    for (int x = card; player1Hand.active_cards_slots.Length > x; x++)
                    {

                        player1Hand.active_cards_slots[x] = player1Hand.active_cards_slots[x + 1];

                    }
                }
            }
        }
    }

    void summon(string unitcount,string unit)
    {
        if (playerTurn == true)
        {
            for (int i = 0; int.Parse(unitcount)+1 > i; i++)
            {
                if (player1Hand.active_cards <= 4)
                {
                    GameObject x =Cr.create_card(unit);
                    player1Hand.active_cards++;
                    player1Hand.active_cards_slots[player1Hand.active_cards - 1] = x ;

                    StartCoroutine(player1Hand.unsick(x, player1Hand));
                    
                }
            }
        }
        else
        {
            for (int i = 0; int.Parse(unitcount)+1 > i; i++)
            {
                if (player2Hand.active_cards < 5)
                {
                    GameObject x = Cr.create_card(unit);
                    player2Hand.active_cards++;
                    player2Hand.active_cards_slots[player2Hand.active_cards-1] = x;
                    StartCoroutine(player2Hand.unsick(x, player2Hand));
                    
                }
            }
        }
    }

    void disable(string units)
    {
        if (playerTurn == true)
        {
            List<GameObject> SelectedCards = new List<GameObject>();
            StartCoroutine(waitForDisable(int.Parse(units), SelectedCards));
            StartCoroutine(pause_sellection_outher_hand(int.Parse(units), SelectedCards));
        }
        else
        {
            int x=0;
            for(int i=0;int.Parse(units)>i; x++)
            {
                int card = Random.Range(0, player1Hand.active_cards);
                if (player1Hand.active_cards_slots[card].GetComponent<CardDisplay>().card.monsterSickness == false)
                {
                    i++;
                    player1Hand.active_cards_slots[card].GetComponent<CardDisplay>().card.monsterSickness = true;
                }
                if (x == 10)
                {
                    i = x;
                }
            }
        }
    }

    IEnumerator WaitForEnhance(List<GameObject> selectedCards)
    {
        yield return new WaitUntil(() => selectedCards.Count == 1);
        for (int i = 0; selectedCards.Count > i; i++)
        {
            selectedCards[i].GetComponent<CardDisplay>().card.description += " Charged Swarm Enduring";
            selectedCards[i].GetComponent<CardDisplay>().descriptionText.text += " Charged Swarm Enduring";
        }
    }

    IEnumerator pause_sellection_own_hand(int cardcount, string Unitytype, List<GameObject> selectedCards)
    {
        //selectedCards = new List<GameObject>();
        timerIsOn = false;
        TurnState lastState = state;
        if (playerTurn == true) {
            state = TurnState.Nothing;
            StartCoroutine(player1Hand.sellectCards(selectedCards, Unitytype));
            yield return new WaitUntil(() => selectedCards.Count == cardcount);
            state = lastState;
            for (int i = 0; selectedCards.Count > i; i++)
                print(selectedCards[i].gameObject.GetComponent<CardDisplay>().card.name);
            timerIsOn = true;


        }
        
    }
    IEnumerator pause_sellection_outher_hand(int cardcount, List<GameObject> selectedCards)
    {
       // selectedCards = new List<GameObject>();
        timerIsOn = false;
        TurnState lastState = state;
        if (playerTurn == true)
        {
            state = TurnState.Nothing;
            StartCoroutine(player1Hand.sellectEnemyCards(selectedCards));
            yield return new WaitUntil(() => selectedCards.Count == cardcount);
            state = lastState;
            for (int i = 0; selectedCards.Count > i; i++)
                print(selectedCards[i].gameObject.GetComponent<CardDisplay>().card.name);
            timerIsOn = true;

        }
    

    }

    IEnumerator player_sacrifice(int cardcount, List<GameObject> selectedCards,string[] message, int start)
    {
        yield return new WaitUntil(() => selectedCards.Count == cardcount);
        for (int i = 0; selectedCards.Count > i; i++)
        {
            selectedCards[i].GetComponent<CardDisplay>().card.health = 0;
        }
        scrificeComplete(message,start);
    }

    IEnumerator waitforDamage(int cardcount,List<GameObject> selectedCards,int damage)
    {
        yield return new WaitUntil(() => selectedCards.Count == cardcount);
        for (int i = 0; selectedCards.Count > i; i++)
        {
            string Decription = selectedCards[i].GetComponent<CardDisplay>().card.description;
            string[] x = Decription.Split(' ');
            bool enduring = false;
            for (int y = 0; x.Length > y; y++)
            {
                if (x[y] == "Enduring")
                {
                    enduring = true;
                }
            }
            if (enduring == false)
            selectedCards[i].GetComponent<CardDisplay>().card.health -= damage;
        }
    }
    IEnumerator waitForUpgrade(int cardcount,List<GameObject> selectedCards, int damage, int health)
    {
        yield return new WaitUntil(() => selectedCards.Count == cardcount);
        for (int i = 0; selectedCards.Count > i; i++)
        {
            selectedCards[i].GetComponent<CardDisplay>().card.health += health;
            selectedCards[i].GetComponent<CardDisplay>().card.attack += damage;
        }
    }
    IEnumerator waitForConvert(int cardcount, List<GameObject> selectedCards)
    {
        yield return new WaitUntil(() => selectedCards.Count == cardcount);
        for (int i = 0; selectedCards.Count > i; i++)
        {
            if (player1Hand.active_cards < 5)
            {
                for(int x=0; player2Hand.active_cards_slots.Length > x ;x++)
                {
                    if (player2Hand.active_cards_slots[x] == selectedCards[i])
                    {
                        player2Hand.active_cards_slots[x] = null;
                        player2Hand.active_cards--;
                        
                        for (int y = x; player2Hand.active_cards > y; y++)
                        {
                            player2Hand.active_cards_slots[y] = player2Hand.active_cards_slots[y + 1];
                        }
                    }

                }

                player1Hand.active_cards_slots[player1Hand.active_cards] = selectedCards[i];
                player1Hand.active_cards++;
            }
        }
    }
    IEnumerator waitForDisable(int cardcount, List<GameObject> selectedCards)
    {
        yield return new WaitUntil(() => selectedCards.Count == cardcount);
        for (int i = 0; selectedCards.Count > i; i++)
        {
            selectedCards[i].GetComponent<CardDisplay>().card.monsterSickness = true;
        }
    }
    #endregion

    void FutureFur_Uniqe_pram()
    {
        for(int i=0;player1Hand.active_cards>i; i++)
        {
            if (player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.FuturefurePramCheak == false)
            {
                string Decription = player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().card.description;
                string[] x = Decription.Split(' ');
                for (int y = 0; x.Length > y; y++)
                {
                    if (x[y] == "is" && x[y + 1] == "the" && x[y + 2] == "only" && x[y + 3] == "unit" && x[y + 4] == "on" && x[y + 5] == "the" && x[y + 6] == "feild" && x[y + 7] == "it" && x[y + 8] == "gains")
                    {
                        StartCoroutine(futrtfurAloneBonus(player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().card, x[y + 9], x[y + 11], player1Hand));
                    }
                    else if (x[y] == "dies" && x[y + 1] == "when" && x[y + 2] == "it" && x[y + 3] == "is" && x[y + 4] == "not" && x[y + 5] == "alone")
                    {
                        StartCoroutine(futrtfurDieWhenNotAlone(player1Hand.active_cards_slots[i].GetComponent<CardDisplay>().card, player1Hand)) ;
                    }
                }
            }
        }
    }
    IEnumerator futrtfurAloneBonus(ScriptableCard card, string bonusHealth, string bonusAttack, Hand hand)
    {
        card.FuturefurePramCheak = true;
        card.health += int.Parse(bonusHealth);
        card.attack += int.Parse(bonusAttack);
        yield return new WaitUntil(() => hand.active_cards != 1);
        card.health -= int.Parse(bonusHealth);
        card.attack -= int.Parse(bonusAttack);
        card.FuturefurePramCheak = false;
    }
    IEnumerator futrtfurDieWhenNotAlone(ScriptableCard card,Hand hand)
    {
        card.FuturefurePramCheak = true;
        yield return new WaitUntil(() => hand.active_cards != 1);
        card.health = 0;
    }
}