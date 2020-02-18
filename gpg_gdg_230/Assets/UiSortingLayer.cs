using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSortingLayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Canvas>())
        {
            GetComponent<Canvas>().sortingOrder = (int)-GetComponentInParent<Transform>().position.z;
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder= (int)-GetComponentInParent<Transform>().position.z;
        }
    }
}
