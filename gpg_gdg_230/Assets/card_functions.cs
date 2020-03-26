using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_functions : MonoBehaviour
{
    public Hand hand;

    public bool isInHand = true;
    //public Hand hand2;

    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.Find("player1").GetComponent<Hand>();
    }

    public void select_card()
    {
        
        hand.selectedCard = gameObject;
        
    }
    public void deselectCard()
    {
        if (hand.selectedCard = gameObject)
        {
            hand.selectedCard = null;

        }
    }

    /*
    //To detect which turn it is in order to play a card.
    public void PlayACard()
    {
        if (hand.active == true)
        {
            //hand.pickCard();
        }

        if (hand2.active == true)
        {
            hand2.pickCard();
        }
    }
    */

    public void Attack()
    {
        if (isInHand == false)
        Debug.Log("Attack");
    }

        
}
