using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GB
{

    public class CardViz : MonoBehaviour
    {
        public Text title;
        public Text detail;
        public Text type;
        public Image art;
        public Text cost;
        public Text attack;
        public Text health;

        public GuillaumeCard card;
        public GuillaumeSpellCard spellCard;

        private void Start()
        {
            if (tag == "Creature")
                LoadCard(card);
            else
                LoadSpellCard(spellCard);
        }

        public void LoadCard(GuillaumeCard c)
        {
            //Debug.Log("Play Creature");

            if (c == null)
                return;

            card = c;

            title.text = c.cardName;
            detail.text = c.cardDetail;
            type.text = c.cardtype;
            art.sprite = c.art;
            cost.text = c.cardCoinCost.ToString();
            attack.text = c.cardAttack.ToString();
            health.text = c.cardHealth.ToString();

        }

        public void LoadSpellCard(GuillaumeSpellCard sc)
        {
            Debug.Log("Play Spell");

            if (sc == null)
                return;

            spellCard = sc;

            title.text = sc.cardName;
            detail.text = sc.cardDetail;
            type.text = sc.cardtype;
            art.sprite = sc.art;
            cost.text = sc.cardManaCost.ToString();

            if (string.IsNullOrEmpty(sc.cardAttack.ToString()))
            {
                attack.gameObject.SetActive(false);
            }

            if (string.IsNullOrEmpty(sc.cardHealth.ToString()))
            {
                health.gameObject.SetActive(false);
            }
        }
    }
}