using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsOnTheField : MonoBehaviour
{

    public List<GameObject> fieldCards;

    public static int[] staticAttackList;

    public int[] attackList;

    public ThisCard cardATK;

    public static bool beingSummoned = false;

    public void Start()
    {
        staticAttackList = attackList;
    }

    public void Update()
    {
        staticAttackList = attackList;

        if (beingSummoned == true)
            Invoke("CheckList", 2f);
    }

    public void CheckList()
    {
        for (int i = 0; i < fieldCards.Count; i++)
        { 
            attackList[i] = cardATK.thisCardAttack;
        }

        beingSummoned = false;
    }
}
