using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class effect_text : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        transform.localScale = new Vector3(1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Color c = text.color;
        c.a -= (0.5f * Time.deltaTime);
        text.color = c;
        if (c.a <= 0)
        {
            Destroy(gameObject);
        }
        transform.position += new Vector3(0, 0.5f, 0) * Time.deltaTime;
    }
}
