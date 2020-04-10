using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenToField : MonoBehaviour
{
    public GameObject cardObject;
    public ThisTokenCard tokenCard;
    public GameObject field;

    private void Start()
    {
        if (cardObject.tag == "Token")
        {
            field = GameObject.Find("Field");
            cardObject.transform.SetParent(field.transform);
            cardObject.transform.localScale = Vector3.one;
            cardObject.transform.position = new Vector3(transform.position.x, transform.position.y, -48);
            cardObject.transform.eulerAngles = new Vector3(25, 0, 0);
            this.tag = tokenCard.thisCard[0].cardType;
        }
    }
}
