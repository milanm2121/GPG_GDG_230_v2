﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class load_deck : MonoBehaviour
{
    public int deck;
    public player_static_deck psd;
    public collection col;
    private void Start()
    {
        psd = GameObject.Find("player deck").GetComponent<player_static_deck>();
        col = GameObject.Find("collection maneger").GetComponent<collection>();
        gameObject.transform.GetChild(0).GetComponent<Text>().text = "deak: " + deck;
    }

    public void loadDeack()
    {
        for(int i=0;40>i; i++)
        {
            psd.deak[i] = col.id[col.deaks[deck].deck[i]];
        }
    }
}
