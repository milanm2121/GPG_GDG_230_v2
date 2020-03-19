using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardLoading : MonoBehaviour
{
    public Text cardID;
    public Text cardName;
    public Text cardDescriptions;
    public Image cardArt;
    public Text cost;
    public Text attack;
    public Text health;

    public ScriptableCard creatureCard;
    public ScriptableSpellCard spellCard;

    public void LoadCreatureCard(ScriptableCard cc)
    {
        if (cc == null)
            return;

        creatureCard = cc;

        cardID.text = cc.ID.ToString();
        cardName.text = cc.name;
        cardDescriptions.text = cc.description;
        cardArt.sprite = cc.artwork;
        cost.text = cc.manaCost.ToString();
        attack.text = cc.attack.ToString();
        health.text = cc.health.ToString();
    }

    public void LoadSpellCard(ScriptableSpellCard sc)
    {
        if (sc == null)
            return;

        spellCard = sc;

        cardID.text = sc.ID.ToString();
        cardName.text = sc.name;
        cardDescriptions.text = sc.description;
        cardArt.sprite = sc.artwork;
        cost.text = sc.manaCost.ToString();
    }
}
