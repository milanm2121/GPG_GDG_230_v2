/*
 * this cript changes the lyers based on the positions of the cards
 * by milan
 */

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
        if (playerHand.selectedCard != gameObject)
        {

            GetComponent<RectTransform>().position = transform.position;
        }
        else if (playerHand.selectedCard == gameObject)
        {
            if (GetComponent<Canvas>())
            {
                GetComponent<RectTransform>().position = new Vector3(transform.position.x,transform.position.y,11);
            }
           
        }
    }
}
