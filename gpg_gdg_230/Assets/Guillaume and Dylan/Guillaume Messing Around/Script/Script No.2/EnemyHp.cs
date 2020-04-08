using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    public static int maxHp;
    public static int staticHp;
    public int hp;

    public Text hpText;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = 20;
        staticHp = 20;
    }

    // Update is called once per frame
    void Update()
    {
        hp = staticHp;

        if (hp >= maxHp)
        {
            hp = maxHp;
        }

        hpText.text = hp.ToString();
    }
}
