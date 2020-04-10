using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsOnTheField : MonoBehaviour
{

    public List<GameObject> fieldCards;

    public static int[] staticAttackList;

    public int[] attackList;

    public static int monsterAttack;

    public static bool beingSummoned = false;

    public void Start()
    {
        staticAttackList = attackList;
    }

    public void Update()
    {
        staticAttackList = attackList;

        if (beingSummoned == true)
            CheckList();
    }

    public void CheckList()
    {
        for (int i = 0; i < fieldCards.Count; i++)
        {
            if (attackList[i] < 1)
                attackList[i] = monsterAttack;
        }

        beingSummoned = false;
    }
}
