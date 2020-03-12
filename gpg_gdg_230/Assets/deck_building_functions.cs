using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deck_building_functions : MonoBehaviour
{
    public collection col;
    // Start is called before the first frame update
    void Start()
    {
        col = GameObject.Find("collection maneger").GetComponent<collection>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCard()
    {
        if (col.cardsInCreateDeak < 40)
        {
            col.deackBeingCreated.deck[col.cardsInCreateDeak] = gameObject.GetComponent<CardDisplay>().card.ID;
            col.cardsInCreateDeak += 1;
        }
    }
}
