﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public enum TurnState {Start, End, Battle, PlayerLost, EnemyLost, DoNothing};
    public TurnState state = TurnState.Start;

    public static bool isYourTurn;
    public int yourTurn;
    public int yourOpponentTurn;
    public Text turnText;

    public static int maxMana;
    public static int currentMana;
    public Text manaText;

    public static int maxCoin;
    public static int currentCoin;
    public Text coinText;

    public static int enemyMaxMana;
    public static int enemyCurrentMana;
    public Text enemyManaText;

    public static int enemyMaxCoin;
    public static int enemyCurrentCoin;
    public Text enemyCoinText;

    public static bool startTurn;

    public GameObject textObject;
    public GameObject spellField;
    public Text victoryText;

    public int whoGoesFirst;

    // Start is called before the first frame update
    void Start()
    {
        whoGoesFirst = Random.Range(1, 10);

        if (whoGoesFirst <= 5)
        {
            isYourTurn = true;
            yourTurn = 1;
            yourOpponentTurn = 0;

            maxCoin = 1;
            currentCoin = 1;
            maxMana = 1;
            currentMana = 1;

            enemyCurrentCoin = 1;
            enemyMaxCoin = 1;
            enemyMaxMana = 0;
            enemyCurrentMana = 0;

            startTurn = true;
        }
        else
        {
            isYourTurn = false;
            yourTurn = 0;
            yourOpponentTurn = 1;

            maxCoin = 1;
            currentCoin = 1;
            maxMana = 0;
            currentMana = 0;

            enemyCurrentCoin = 1;
            enemyMaxCoin = 1;
            enemyCurrentMana = 1;
            enemyMaxMana = 1;

            startTurn = false;
        }


        textObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case (TurnState.Start):
                break;
            case (TurnState.Battle):
                break;
            case (TurnState.End):
                break;
            case (TurnState.PlayerLost):
                textObject.SetActive(true);
                victoryText.text = "DEFEATED";
                spellField.SetActive(false);
                state = TurnState.DoNothing;
                break;
            case (TurnState.EnemyLost):
                textObject.SetActive(true);
                victoryText.text = "VICTORY";
                spellField.SetActive(false);
                state = TurnState.DoNothing;
                break;
        }
        if (isYourTurn == true)
            turnText.text = "Your Turn";
        else
            turnText.text = "Opponent Turn";

        coinText.text = currentCoin + "/" + maxCoin;
        manaText.text = currentMana + "/" + maxMana;
        enemyCoinText.text = enemyCurrentCoin + "/" + enemyMaxCoin;
        enemyManaText.text = enemyCurrentMana + "/" + enemyMaxMana;

        if (PlayerHp.staticHp <= 0)
            state = TurnState.PlayerLost;

        if (EnemyHp.staticHp <= 0)
            state = TurnState.EnemyLost;


    }

    public void EndYourTurn()
    {
        isYourTurn = false;
        yourOpponentTurn += 1;

        if (maxCoin >= 10)
            maxCoin = 10;
        else
            maxCoin += 1;

        currentCoin = maxCoin;
        currentMana = maxMana;

        if (enemyMaxCoin >= 10)
            enemyMaxCoin = 10;
        else
            enemyMaxCoin += 1;

        enemyCurrentCoin = enemyMaxCoin;

        if (enemyMaxMana >= 10)
            enemyMaxMana = 10;
        else
            enemyMaxMana += 1;

        enemyCurrentMana = enemyMaxMana;

        startTurn = false;
        AI.draw = false;
    }

    public void EndYourOpponentTurn()
    {
        isYourTurn = true;
        yourTurn += 1;

        if (maxMana >= 10)
            maxMana = 10;
        else
            maxMana += 1;

        currentMana = maxMana;

        if (maxCoin >= 10)
            maxCoin = 10;
        else
            maxCoin += 1;

        currentCoin = maxCoin;

        if (enemyMaxCoin >= 10)
            enemyMaxCoin = 10;
        else
            enemyMaxCoin += 1;

        enemyCurrentCoin = enemyMaxCoin;
        enemyCurrentMana = enemyMaxMana;

        startTurn = true;
    }

}
