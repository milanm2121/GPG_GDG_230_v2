using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSortingLayer : MonoBehaviour
{
    Hand playerHand;
    // Start is called before the first frame update
    void Start()
    {
        playerHand = GameObject.FindGameObjectWithTag("player hand").GetComponent<Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((playerHand.selectedCard != null && playerHand.selectedCard.gameObject != gameObject))
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
    }
}
