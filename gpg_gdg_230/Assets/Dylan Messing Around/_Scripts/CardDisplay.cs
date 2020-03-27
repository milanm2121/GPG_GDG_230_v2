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

    
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;

        artworkImage.sprite = card.artwork;

        manaText.text = card.manaCost.ToString();
        
        

    }
    private void Update()
    {

        healthText.text = card.health.ToString();
        attackText.text = card.attack.ToString();

        if (active)
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
