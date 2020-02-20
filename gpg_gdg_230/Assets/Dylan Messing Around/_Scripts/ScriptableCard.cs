using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Creatures")]
public class ScriptableCard : ScriptableObject
{

    public new string name;
    public string description;

    public Sprite artwork;

    public int manaCost;
    public int attack;
    public int health;

}

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Spells")]
public class ScriptableSpellCard : ScriptableObject
{

    public new string name;
    public string description;

    public Sprite artwork;

    public int manaCost;
    public int attack;
}

