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
        if (TBS.state == TurnBaseScript.TurnState.EndofBattle)
        {
            StartCoroutine(CombatPhase());
        }

    }

    IEnumerator CombatPhase()
    {
        Debug.Log("I am working");

        for (int i = 0; attack.Count > i; i++)
        {
            if (defend.Count != 0 && defend[i] != null)
            {
                int newHealth = defend[i].GetComponent<CardDisplay>().card.health - attack[i].GetComponent<CardDisplay>().card.attack;
                defend[i].GetComponent<CardDisplay>().card.health = newHealth;
                Debug.Log("I am working Part 2");
            }
            else
            {
                if (TBS.playerTurn == false)
                {
                    TBS.player1Health -= attack[i].GetComponent<CardDisplay>().card.attack;
                    Debug.Log("I am working part 3");
                }
                else
                {
                    TBS.player2Health -= attack[i].GetComponent<CardDisplay>().card.attack;
                    Debug.Log("I am working Part 4");
                }
            }

            yield return new WaitForSeconds(0);
        }
        attack.Clear();
        defend.Clear();
        TBS.state = TurnBaseScript.TurnState.Nothing;
    }
}
