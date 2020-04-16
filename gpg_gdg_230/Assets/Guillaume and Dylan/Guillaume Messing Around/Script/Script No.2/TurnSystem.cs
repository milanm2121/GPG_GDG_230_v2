using System.Collections;
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

    public static bool startTurn;

    public GameObject textObject;
    public GameObject spellField;
    public Text victoryText;

    // Start is called before the first frame update
    void Start()
    {
        isYourTurn = true;
        yourTurn = 1;
        yourOpponentTurn = 0;

        maxCoin = 1;
        currentCoin = 1;
        maxMana = 1;
        currentMana = 1;

        startTurn = false;

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

        startTurn = true;
    }

}
