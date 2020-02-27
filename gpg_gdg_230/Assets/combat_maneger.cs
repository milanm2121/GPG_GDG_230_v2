using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combat_maneger : MonoBehaviour
{
    public TurnBaseScript TBS;

    public List<GameObject> attack= new List<GameObject>();
    public List<GameObject> defend = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        TBS = GameObject.Find("maneger object").GetComponent<TurnBaseScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (TBS.state == TurnBaseScript.TurnState.End)
        {
            StartCoroutine(CombatPhase());
        }

    }

    IEnumerator CombatPhase()
    {
        for (int i = 0; attack.Count > i; i++)
        {
            if (defend.Count != 0 && defend[i] != null)
            {
                int newHealth = defend[i].GetComponent<CardDisplay>().card.health - attack[i].GetComponent<CardDisplay>().card.attack;
                defend[i].GetComponent<CardDisplay>().card.health = newHealth;
            }
            else
            {
                if (TBS.playerTurn == false)
                {
                    TBS.player1Health -= attack[i].GetComponent<CardDisplay>().card.attack;
                }
                else
                {
                    TBS.player2Health -= attack[i].GetComponent<CardDisplay>().card.attack;

                }
            }
            yield return new WaitForSeconds(1);
        }
        attack.Clear();
        defend.Clear();
    }
}
