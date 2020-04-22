using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_script : MonoBehaviour
{
    public TurnBaseScript tbs;

    public GameObject Playerturn;
    public GameObject Attack;
    public GameObject deffend;
    public GameObject CardPlayed;
    // Update is called once per frame
    void Update()
    {
        if(tbs.state== TurnBaseScript.TurnState.PlayerTurn && tbs.playerTurn==true)
        {
            Playerturn.SetActive(true);
        }
        else
        {
            Playerturn.SetActive(false);
        }
        if (tbs.state == TurnBaseScript.TurnState.CardPlayed)
        {
            CardPlayed.SetActive(true);
        }
        else
        {
            CardPlayed.SetActive(false);
        }
        if(tbs.state == TurnBaseScript.TurnState.Response && tbs.playerTurn == false)
        {
            deffend.SetActive(true);
        }
        else
        {
            deffend.SetActive(false);
        }
        if (tbs.state == TurnBaseScript.TurnState.Attack && Playerturn == true)
        {
            Attack.SetActive(true);
        }
        else
        {
            Attack.SetActive(false);
        }
    }
}
