using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GB
{
    public class SpellCardViz : MonoBehaviour
    {
        public Text title;
        public Text detail;
        public Text type;
        public Image art;
        public Text cost;

        public GuillaumeSpellCard spellCard;

        private void Start()
        {
            LoadSpellCard(spellCard);
        }


        public void LoadSpellCard(GuillaumeSpellCard sc)
        {
            if (sc == null)
                return;

            spellCard = sc;

            title.text = sc.cardName;
            detail.text = sc.cardDetail;
            type.text = sc.cardtype;
            art.sprite = sc.art;
            cost.text = sc.cardManaCost.ToString();

        }
    }
}
