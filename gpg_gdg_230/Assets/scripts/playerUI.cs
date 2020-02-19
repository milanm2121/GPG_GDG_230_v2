using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerUI : MonoBehaviour
{
    public TMP_Text health;
    public TMP_Text gold;
    public TMP_Text mana;

    public Hand hand;
    public TurnBaseScript TBS;
    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<Hand>();
        TBS = GameObject.Find("maneger object").GetComponent<TurnBaseScript>();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = "enemy health:" + "\n" + TBS.player2Health.ToString() + "\n" + "\n" + "\n" + "health" + "\n" + TBS.player1Health.ToString();
        gold.text = "gold:" + "\n" + hand.playerGold.ToString();
        mana.text = "mana:" + "\n" + hand.playerMana.ToString();
    }
}