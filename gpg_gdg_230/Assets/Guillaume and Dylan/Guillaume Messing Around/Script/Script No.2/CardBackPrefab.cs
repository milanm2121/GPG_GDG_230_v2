using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBackPrefab : MonoBehaviour
{

    public GameObject deck;
    public GameObject iT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deck = GameObject.Find("Deck Panel");
        iT.transform.SetParent(deck.transform);
        iT.transform.localScale = Vector3.one;
        iT.transform.position = new Vector3(transform.position.x, transform.position.y, -48);
        iT.transform.eulerAngles = new Vector3(25, 0, 0);
    }
}
