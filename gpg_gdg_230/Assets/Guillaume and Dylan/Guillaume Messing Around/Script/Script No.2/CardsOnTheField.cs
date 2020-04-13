using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is where both I keep tracks on who is on the field,
// While also making sure that my card buff other cards,
// rather than itself.
//Guillaume Blanchard
public class CardsOnTheField : MonoBehaviour
{

    public List<GameObject> fieldCards;
    public List<ThisCard> cardStats;
    public List<ThisTokenCard> tokenCardStats;

    public void BuffOtherCardsATKStats(int x)
    {
        for (int i = 0; i < cardStats.Count; i++)
        {
            Debug.Log("Working Part 2");
            if (cardStats[i].buffOtherCardsATK == 0 && cardStats[i].dontBuffThisUnit == false)
                cardStats[i].thisCardAttack += x;
        }
    }

    public void BuffOtherTokenCardsATKStats(int x)
    {
        for (int i = 0; i < tokenCardStats.Count; i++)
        {
            Debug.Log("Working Part 3");
            if (tokenCardStats.Count != 0)
                tokenCardStats[i].thisCardAttack += x;
            else
                return;
        }
    }

    public void BuffOtherCardsHealthStat(int x)
    {
        for (int i = 0; i < cardStats.Count; i++)
        {
            if (cardStats[i].buffOtherCardsHealth == 0 && cardStats[i].dontBuffThisUnit == false)
                cardStats[i].thisCardHealth += x;
        }
    }

    public void BuffOtherTokenCardsHealthStat(int x)
    {
        for (int i = 0; i < tokenCardStats.Count; i++)
        {
            if (tokenCardStats.Count != 0)
                tokenCardStats[i].thisCardHealth += x;
            else
                return;
        }
    }
}
