using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GB
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Creature Card Ver.2")]
    public class GuillaumeCard : ScriptableObject
    {
        public int cardID;
        public string cardName;
        public Sprite art;
        public string cardDetail;
        public string cardtype;

        public int cardCoinCost;
        public int cardAttack;
        public int cardHealth;
    }

    [CreateAssetMenu(menuName = "Spell Card Ver. 2")]
    public class GuillaumeSpellCard: ScriptableObject
    {
        public string cardName;
        public Sprite art;
        public string cardDetail;
        public string cardtype;

        public int cardManaCost;
        public int cardAttack;
        public int cardHealth;
    }
}
