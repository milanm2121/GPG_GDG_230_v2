using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSortingLayer : MonoBehaviour
{
    Hand playerHand;
    //public bool inspected;
    // Start is called before the first frame update
    void Start()
    {
        playerHand = GameObject.FindGameObjectWithTag("player hand").GetComponent<Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHand.selectedCard != null || playerHand.selectedCard != GetComponent<card>())
        {

            if (GetComponent<Canvas>())
            {
                GetComponent<Canvas>().sortingOrder = (int)-GetComponentInParent<Transform>().position.z;
            }
            else
            {
                GetComponent<SpriteRenderer>().sortingOrder = (int)-GetComponentInParent<Transform>().position.z;
            }
        }
        else //if (playerHand.selectedCard == GetComponent<card>())
        {
            if (GetComponent<Canvas>())
            {
                GetComponent<Canvas>().sortingOrder = 11;
            }
            else
            {
                GetComponent<SpriteRenderer>().sortingOrder = 10;
            }
        }
    }
}
