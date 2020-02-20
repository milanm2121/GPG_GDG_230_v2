using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The main purpose of this sc 
 */

public class TurnBaseScript : MonoBehaviour
{
    //To make sure that the turn are played properly.
    public enum TurnState { StartTurn, PlayerTurn, Response, Attack, End, Nothing, TimeWasted  }
    public TurnState state = TurnState.Nothing;

    public Hand player1Hand;
    public Hand player2Hand;

    //The main players health base.
    public int player1Health = 20;
    public int player2Health = 20;

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

    // Start is called before the first frame update
    void Start()
    {
        whoGoesFirst = Random.Range(1, 10);
        if (whoGoesFirst <= 5)
            playerTurn = true;
        else
            playerTurn = false;

        state = TurnState.StartTurn;

    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case (TurnState.StartTurn):
                if (startOfTheGame == true)
                {
                    Draw7();
                }
                else
                {
                    //need to make the player draw 1
                    Debug.Log("Play drew a card");
                }
                GainManaAndCoin();
                state = TurnState.PlayerTurn;
                break;
            case (TurnState.PlayerTurn):
                if (playerTurn == true)
                {
                    player1Hand.active = true;
                    player2Hand.active = false;
                }
                else
                {
                    player1Hand.active = false;
                    player2Hand.active = true;
                }
                //Making the turn timer, we need some more work on.
                if (timerIsOn == false)
                {
                    StartCoroutine("CountDown");
                    timerIsOn = true;
                }
                if(turnTimer <= 0)
                {
                    StopCoroutine("CountDown");
                    timerIsOn = false;

                    state = TurnState.TimeWasted;
                }
                break;
            case (TurnState.Attack):

                break;
            case (TurnState.Response):
                PlayerResponeToAction();
                break;
            case (TurnState.End):
                EndPlayerTurn();
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
        }
    }

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
        /*
        Debug.Log("Player 1 gold " + player1Hand.playerGold);
        Debug.Log("Player 1 mana " + player1Hand.playerMana);
        Debug.Log("Player 2 gold " + player2Hand.playerGold);
        Debug.Log("Player 2 Mama " + player2Hand.playerMana);
        Debug.Log("Default Gold " + defaultGold);
        Debug.Log("Default Mana 1 " + defaultMana1);
        Debug.Log("Default Mana 2 " + defaultMana2);
        */
    }

    void PlayerResponeToAction()
    {
        if (playerTurn == true)
            playerTurn = false;
        else
            playerTurn = true;
    }

    public void EndPlayerTurn()
    {
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

        state = TurnState.StartTurn;
    }

    //This is use for when the player is AFK 
    //If there are AFK for too long, then they will lose.
    public void TimerEndTurn()
    {
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
            Debug.Log("Player 1 lose");
        if (player2AFKStrike == 3)
            Debug.Log("Player 2  lose");


        state = TurnState.Nothing;
    }

    //This is so that the player doesn't take too long
    IEnumerator CountDown()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            turnTimer--;
        }

    }



}
