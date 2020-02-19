using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{

    //name of the card
    public string Name;
    //1=entity card, 2 magic card
    public bool IsMagic;
    //mana cost
    public int Mana_cost;
    //gold cost
    public int gold_cost;
    //entity health
    public int health;
    //public Health health;
    public bool defenfing;
    //entity damage
    public int attack_dmg;
    //current target
    public card target;
    //public ablity
    //ablity script
    //class1-3
    public int Class;

    public pass_card_stats PCS;


    bool inspected;

    void Awake()
    {
        PCS = GetComponent<pass_card_stats>();
       
    }

    // Start is called before the first frame update
    void Start()
    {
        PCS.IsMagic = IsMagic;
        StartCoroutine(secondframe());
    }

    void Update()
    {
        PCS.UI_attack_dmg.text = attack_dmg.ToString();
        PCS.UI_health.text = health.ToString();
        if (IsMagic)
        {
            PCS.cost.text = Mana_cost.ToString();
        }
        else
        {
            PCS.cost.text = gold_cost.ToString();
        }

        if (inspected == true)
        {

        }


    }
    IEnumerator secondframe()
    {
        yield return new WaitForEndOfFrame();
        PCS.Name = Name;
    }
}
