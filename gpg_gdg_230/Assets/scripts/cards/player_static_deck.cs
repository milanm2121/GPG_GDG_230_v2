using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_static_deck : MonoBehaviour
{
    public int Class;

    public ScriptableCard[] deak = new ScriptableCard[40];


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    public void loadDeck()
    {
        if(SceneManager.GetActiveScene().name == "card_tabel")
        {
            Deak activeDeck = GameObject.Find("player1").GetComponent<Deak>();
            activeDeck.Class = Class;
            deak.CopyTo(activeDeck.deak, 0);
        }
    }
}
