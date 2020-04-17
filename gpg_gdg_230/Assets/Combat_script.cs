using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_script : MonoBehaviour
{
    public TurnSystem TS;
    public GameObject[] defending = new GameObject[5];
    public List<GameObject> attacking = new List<GameObject>();

    bool turn=false;
    // Start is called before the first frame update
    void Start()
    {
        TS = GetComponent<TurnSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (TS.state == TurnSystem.TurnState.Battle)
        {
            for (int x = 0; attacking.Count > x; x++)
            {
                for (int i = 0; defending.Length > i; i++)
                {
                    if (defending[i] != null)
                    {
                        defending[i].GetComponent<ThisCard>().thisCardHealth -= attacking[i].GetComponent<ThisCard>().thisCardAttack;
                        attacking[i].GetComponent<ThisCard>().thisCardAttack -= defending[i].GetComponent<ThisCard>().thisCardHealth;
                        x++;
                    }
                }
                attacking[x].GetComponent<ThisCard>().Attack();
            }
        }
        attacking.Clear();
        for(int i=0;defending.Length>i; i++)
        {
            defending[i] = null;
        }
    }
}
