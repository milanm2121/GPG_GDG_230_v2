using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deak : MonoBehaviour
{
    public int Cards_active_deak=40;

    public int Class;

    public card[] deak = new card[40];

   
    private void Update()
    {
        if (deak[0] == null)
        {
            for(int i = 0; deak.Length > i+1; i++)
            {
                deak[i] = deak[i + 1];
            }
        }
        
    }

    public card Pick_random()
    {
        card Random_card;
        int card_index_picked = Random.Range(0, Cards_active_deak);
        Random_card = deak[card_index_picked];
        Cards_active_deak--;
        for(int i = card_index_picked; Cards_active_deak-1 > i; i++)
        {
            deak[i] = deak[i+1];
        }

        return Random_card;
    }

}
