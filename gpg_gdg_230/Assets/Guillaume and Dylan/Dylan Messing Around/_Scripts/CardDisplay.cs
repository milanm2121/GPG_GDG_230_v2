using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public ScriptableCard card;

    public Text nameText;
    public Text descriptionText;

    public Image artworkImage;
    public Image cardTemplate;

    public Text manaText;
    public Text attackText;
    public Text healthText;

    public GameObject backofCard;

    public Color sickness;

    public bool hide=false;

    public bool active=false;

    //0=none, 1==attackig, 2== defending
    public byte attack_defend = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;

        artworkImage.sprite = card.artwork;
        
        manaText.text = card.manaCost.ToString();


        artworkImage.preserveAspect = true;
    }
    private void Update()
    {

        healthText.text = card.health.ToString();
        attackText.text = card.attack.ToString();

        if (active && attack_defend==0)
        {
            if (card.monsterSickness == true)
            {
                cardTemplate.color = sickness;
            }
            else
            {
                cardTemplate.color = Color.white;
            }
        }
        else if(attack_defend==1)
        {
            cardTemplate.color = Color.red;
        }
        else if (attack_defend == 2)
        {
            cardTemplate.color = Color.blue;
        }

        if (hide == true)
        {
            backofCard.transform.SetAsLastSibling();
        }
        else
        {
            backofCard.transform.SetAsFirstSibling();
        }
        
    }

}
