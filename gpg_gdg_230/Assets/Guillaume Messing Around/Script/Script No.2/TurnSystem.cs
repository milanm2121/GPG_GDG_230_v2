using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public bool isYourTurn;
    public int yourTurn;
    public int yourOpponentTurn;
    public Text turnText;

    public int maxMana;
    public static int currentMana;
    public Text manaText;

    public int maxCoin;
    public static int currentCoin;
    public Text coinText;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (isYourTurn == true)
            turnText.text = "Your Turn";
        else
            turnText.text = "Opponent Turn";

        coinText.text = currentCoin + "/" + maxCoin;
        manaText.text = currentMana + "/" + maxMana;
    }

    public void EndYourTurn()
    {
        isYourTurn = false;
        yourOpponentTurn += 1;

        if (maxCoin > 10)
            maxCoin = 10;
        else
            maxCoin += 1;

        currentCoin = maxCoin;
    }

    public void EndYourOpponentTurn()
    {
        isYourTurn = true;
        yourTurn += 1;

        maxMana += 1;
        currentMana = maxMana;

        if (maxCoin > 10)
            maxCoin = 10;
        else
            maxCoin += 1;

        currentCoin = maxCoin;
    }

}
