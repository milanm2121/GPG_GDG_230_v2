using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class collection : MonoBehaviour
{
    public Text texttemp;
    public struct cardGroup
    {
        public GameObject card;
        public Text text;
        public int count;
    };
    public GameObject cardtemp;
    public cardGroup[] Collection = new cardGroup[150];
    public ScriptableCard[] id = new ScriptableCard[150];

    public RectTransform origonalTransform;

    public Scrollbar sb;
    public float origonalsbYvalue;

    public List<serilisable_deak> deaks = new List<serilisable_deak>();
    // Start is called before the first frame update
    void Start()
    {
        

        load_cards();


        origonalsbYvalue = origonalTransform.GetComponent<RectTransform>().position.y;
        //load collection
        for (int x = 0; 30 > x; x++)
        {
            for (int y = 0; 5 > y; y++)
            {
                Collection[(x * 5) + y].card = Instantiate(cardtemp, new Vector2(origonalTransform.position.x, origonalTransform.position.y) + new Vector2(y*220,-x*400), Quaternion.identity);
                Collection[(x * 5) + y].text = Instantiate(texttemp, new Vector2(origonalTransform.position.x, origonalTransform.position.y) + new Vector2(y * 220, -x * 400 -150), Quaternion.identity);
                Collection[(x * 5) + y].count = static_collections.Collection[(x * 5) + y];
                Collection[(x * 5) + y].text.text = "cards you can use:" + Collection[(x * 5) + y].count.ToString();
                Collection[(x * 5) + y].text.transform.parent = origonalTransform;
                Collection[(x * 5) + y].card.transform.parent = origonalTransform;
                Collection[(x * 5) + y].card.transform.localScale = new Vector2(0.8f, 0.8f);
                Collection[(x * 5) + y].card.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                Collection[(x * 5) + y].card.GetComponent<CardDisplay>().card = id[(x * 5) + y];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        origonalTransform.position = new Vector2(origonalTransform.position.x ,9000 * sb.value+origonalsbYvalue);
        sb.size = 0;
    }
   void load_cards()
    {
        //load cards
        temp_collection tmpc=new temp_collection();
        tmpc.temp_collection_load();

    }
}






