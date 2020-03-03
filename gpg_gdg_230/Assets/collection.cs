using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class collection : MonoBehaviour
{
    public struct cardGroup
    {
        public GameObject card;
        public int count;
    };
    public GameObject cardtemp;
    public cardGroup[] Collection = new cardGroup[150];

    public RectTransform origonalTransform;

    public Scrollbar sb;
    // Start is called before the first frame update
    void Start()
    {
        //load collection
        for (int x = 0; 30 > 5; x++)
        {
            for (int y = 0; 5 > y; y++)
            {
                Collection[(x * 5) + y].card = Instantiate(cardtemp, new Vector2(origonalTransform.position.x, origonalTransform.position.y) + new Vector2(y*300,-x*300), Quaternion.identity);
                Collection[(x * 5) + y].card.transform.parent = origonalTransform;
                Collection[(x * 5) + y].card.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        origonalTransform.position = new Vector2(origonalTransform.position.x ,30*300 * sb.value);
    }
}
