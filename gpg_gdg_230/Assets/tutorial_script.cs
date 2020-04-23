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
    bool growtick1=false;
    bool growtick2 = false;
    bool growtick3 = false;
    bool growtick4 = false;
    // Update is called once per frame
    private void Start()
    {
        transform.localScale = new Vector3(2, 2, 0);
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), 0.3f);
        if(tbs.state== TurnBaseScript.TurnState.PlayerTurn && tbs.playerTurn==true)
        {
            Playerturn.SetActive(true);
            if (growtick1 == false)
            {
                transform.localScale = new Vector3(2, 2, 0);
                growtick1 = true;
            }
        }
        else
        {
            Playerturn.SetActive(false);
            growtick1 = false;
        }
        if (tbs.state == TurnBaseScript.TurnState.CardPlayed)
        {
            CardPlayed.SetActive(true);
            if (growtick2 == false)
            {
                transform.localScale = new Vector3(2, 2, 0);
                growtick2 = true;
            }
        }
        else
        {
            CardPlayed.SetActive(false);
            growtick2 = false;
        }
        if(tbs.state == TurnBaseScript.TurnState.Response && tbs.playerTurn == false)
        {
            deffend.SetActive(true);
            if (growtick3 == false)
            {
                transform.localScale = new Vector3(2, 2, 0);
                growtick3 = true;
            }
        }
        else
        {
            deffend.SetActive(false);
            growtick3 = false;
        }
        if (tbs.state == TurnBaseScript.TurnState.Attack && Playerturn == true)
        {
            Attack.SetActive(true);
            if (growtick4 == false)
            {
                transform.localScale = new Vector3(2, 2, 0);
                growtick4 = true;
            }
        }
        else
        {
            Attack.SetActive(false);
            growtick4 = false;
        }
    }
}
