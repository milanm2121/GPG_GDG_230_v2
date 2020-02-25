using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_functions : MonoBehaviour
{
    public Hand hand;
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
        GetComponent<RectTransform>().localScale = new Vector2(2, 2);
    }
    public void deselectCard()
    {
        if (hand.selectedCard = gameObject)
        {
            hand.selectedCard = null;
            GetComponent<RectTransform>().localScale = new Vector2(1, 1);

        }
    }

        
}
