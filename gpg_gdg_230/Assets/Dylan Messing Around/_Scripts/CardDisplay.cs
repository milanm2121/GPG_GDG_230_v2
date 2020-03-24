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

    public Text manaText;
    public Text attackText;
    public Text healthText;

    public Color sickness;
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
        if (card.monsterSickness == true)
        {
            gameObject.transform.GetChild(1).GetComponent<Image>().color = sickness;
            
        }
        else
        {
            gameObject.transform.GetChild(1).GetComponent<Image>().color = Color.white;

        }
    }

}
