using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState { StartTurn, PlayerTurn, Response, Attack, End, Nothing }

/*
 * The main purpose of this sc 
 */

public class TurnBaseScript : MonoBehaviour
{
    //To make sure that the turn are played properly.
    //i moved it above so all scripts can acsses it /|\
   // public enum TurnState { StartTurn, PlayerTurn, Response, Attack, End, Nothing }
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
    //To prevent this from overlaping
    public bool timerIsOn = false;

    //To see which player goes first when the game starts.
    public int whoGoesFirst;

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
                //Draw7();
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
                    turnTimer = 30 / 2;
                    state = TurnState.Nothing;
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
        }
    }

    void Draw7()
    {
        player2Hand.pick7();
        player1Hand.pick7();
        state = TurnState.PlayerTurn;
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
        if (playerTurn == true)
            playerTurn = false;
        else
            playerTurn = true;
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
