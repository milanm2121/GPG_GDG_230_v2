using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deck_building_functions : MonoBehaviour
{
    public collection col;
    public int ID;
    public int count;
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        col = GameObject.Find("collection maneger").GetComponent<collection>();
        ID = gameObject.GetComponent<CardDisplay>().card.ID;
        ResetCount();
        button = GetComponent<Button>();
    }

    public void addCard()
    {
        if (col.cardsInCreateDeak < 40 && count>0)
        {          
            col.deackBeingCreated.deck[col.cardsInCreateDeak] = ID;
            col.cardsInCreateDeak += 1;
            count -= 1;
            if(count<=0)
                button.interactable = false;
        }
        
            
        
    }
    public void ResetCount()
    {
        count = Mathf.Clamp(col.Collection[ID-1].count,0,4);
    }
}
