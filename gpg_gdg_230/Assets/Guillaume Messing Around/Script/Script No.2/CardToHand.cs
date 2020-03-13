using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToHand : MonoBehaviour
{
    public GameObject hand;
    public GameObject cardObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hand = GameObject.Find("PlayerHand Version2");
        cardObject.transform.SetParent(hand.transform);
        cardObject.transform.localScale = Vector3.one;
        cardObject.transform.position = new Vector3(transform.position.x, transform.position.y, -48);
        cardObject.transform.eulerAngles = new Vector3(25, 0, 0);
    }
}
