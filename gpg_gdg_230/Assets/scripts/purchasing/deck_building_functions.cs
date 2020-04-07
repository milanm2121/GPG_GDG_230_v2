using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deck_building_functions : MonoBehaviour
{
    public collection col;
    public int ID;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        col = GameObject.Find("collection maneger").GetComponent<collection>();
        ID = gameObject.GetComponent<CardDisplay>().card.ID;
        ResetCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCard()
    {
        if (col.cardsInCreateDeak < 40 && count>0)
        {          
            col.deackBeingCreated.deck[col.cardsInCreateDeak] = ID;
            col.cardsInCreateDeak += 1;
            count -= 1;
        }
    }
    public void ResetCount()
    {
        count = Mathf.Clamp(col.Collection[ID].count,0,4);
    }
}
