using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pass_card_stats : MonoBehaviour
{

    //[HideInInspector]
    public string Name;
    [HideInInspector]
    public bool IsMagic;


    //suff for card UI
    public TMP_Text UI_name;
    public TMP_Text UI_attack_dmg;
    public TMP_Text UI_health;
    public Image cost_type;
    public Sprite gold;
    public Sprite mana;
    public TMP_Text cost;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(secondframe());
    }

    void Update()
    {
       // UI_attack_dmg.text = attack_dmg.ToString();
       // UI_health.text = health.ToString();
    }
    IEnumerator secondframe()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        UI_name.text = Name;
        if (IsMagic == false)
        {
            cost_type.sprite = gold;
            cost.text = cost.ToString();
        }
        else
        {
            cost_type.sprite = mana;
            cost.text = cost.ToString();
        }
    }
}
