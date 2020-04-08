using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_reffence : MonoBehaviour
{
    public ScriptableCard[] id= new ScriptableCard[150];
    public GameObject Unit_card_temp;
    public GameObject cardfeild;

    public GameObject create_card(string unit)
    {
        GameObject card = Instantiate(Unit_card_temp);
        for (int i=0;id.Length>i; i++)
        {
            if (id[i].name == unit)
            {
                card.transform.localScale = new Vector3(1, 1, 1);

                ScriptableCard sc = new ScriptableCard
                {
                    artwork = id[i].artwork,
                    name = id[i].name,
                    health = id[i].health,
                    attack = id[i].attack,
                    manaCost = id[i].manaCost,
                    description = id[i].description,
                    monsterSickness = false

                };
                card.GetComponent<CardDisplay>().card = sc;
                card.transform.parent = cardfeild.transform;
                card.GetComponent<RectTransform>().localScale = new Vector2(0.6f, 0.6f);
                card.GetComponent<card_functions>().isInHand = false;
                return card;

            }
        }
        return card;
    }
}
