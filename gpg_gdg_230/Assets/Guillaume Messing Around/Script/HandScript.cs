using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class HandScript : MonoBehaviour
{
    public bool isInHand = true;

    public GameObject cardsInhand;

    public void EnlargeCard()
    {
        Debug.Log("Hello There");
        
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
}
