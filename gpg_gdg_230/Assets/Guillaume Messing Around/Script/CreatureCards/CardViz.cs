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

        private void Start()
        {
            LoadCard(card);
        }

        public void LoadCard(GuillaumeCard c)
        {
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
    }
}