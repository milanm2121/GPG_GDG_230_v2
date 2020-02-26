using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_functions : MonoBehaviour
{
    public Hand hand;
    public Hand hand2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void select_card()
    {
        hand.selectedCard = gameObject;
        GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);
    }
    public void deselectCard()
    {
        if (hand.selectedCard = gameObject)
        {
            hand.selectedCard = null;
            GetComponent<RectTransform>().localScale = new Vector2(1, 1);

        }
    }

    //To detect which turn it is in order to play a card.
    public void PlayACard()
    {
        if (hand.active == true)
        {
            hand.pickCard();
        }

        if (hand2.active == true)
        {
            hand2.pickCard();
        }
    }

        
}
