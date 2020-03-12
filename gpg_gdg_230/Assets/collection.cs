using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class collection : MonoBehaviour
{
    public Text texttemp;
    [System.Serializable]
    public struct cardGroup
    {
        public GameObject card;
        public Text text;
        public int count;
    };
    public GameObject cardtemp;
    public GameObject cardtempcreation;

    public cardGroup[] Collection = new cardGroup[150];
    public ScriptableCard[] id = new ScriptableCard[150];

    public List<cardGroup> cards_you_have = new List<cardGroup>();

    public RectTransform origonalTransform;
    public RectTransform origonalTransform2;
    public Scrollbar sb;
    public float origonalsbYvalue;
    public Scrollbar sb2;
    public float origonalsbYvalue2;
    public List<serilisable_deak> deaks = new List<serilisable_deak>();


    public serilisable_deak deackBeingCreated;
    public int cardsInCreateDeak=0;
    
    // Start is called before the first frame update
    void Start()
    {


        load_cards();


        origonalsbYvalue = origonalTransform.GetComponent<RectTransform>().position.y;
        origonalsbYvalue2 = origonalTransform2.GetComponent<RectTransform>().position.y;
        //load collection
        for (int x = 0; 30 > x; x++)
        {
            for (int y = 0; 5 > y; y++)
            {
                Collection[(x * 5) + y].card = Instantiate(cardtemp, new Vector2(origonalTransform.position.x, origonalTransform.position.y) + new Vector2(y * 180, -x * 220), Quaternion.identity);
                Collection[(x * 5) + y].text = Instantiate(texttemp, new Vector2(origonalTransform.position.x, origonalTransform.position.y) + new Vector2(y * 180, -x * 220 - 110), Quaternion.identity);
                Collection[(x * 5) + y].count = static_collections.Collection[(x * 5) + y];
                Collection[(x * 5) + y].text.text = "cards you can use:" + Collection[(x * 5) + y].count.ToString();
                Collection[(x * 5) + y].text.transform.parent = origonalTransform;
                Collection[(x * 5) + y].card.transform.parent = origonalTransform;
                Collection[(x * 5) + y].card.transform.localScale = new Vector2(0.8f, 0.8f);
                Collection[(x * 5) + y].card.GetComponent<RectTransform>().localScale = new Vector2(0.7f, 0.7f);
                Collection[(x * 5) + y].card.GetComponent<CardDisplay>().card = id[(x * 5) + y];

                if (Collection[(x * 5) + y].count != 0)
                {
                    cards_you_have.Add(Collection[(x * 5) + y]);
                }
            }
        }
        int a = (cards_you_have.Count / 3);
        for (int x = 0; (int)a+2 > x; x++)
        {
            for (int y = 0; 3 > y; y++)
            {
                if ((x * a) + y < cards_you_have.Count)
                {
                    cardGroup cg = new cardGroup();
                    cg.card = Instantiate(cardtempcreation, new Vector2(origonalTransform2.position.x, origonalTransform2.position.y) + new Vector2(y * 220, -x * 300), Quaternion.identity);
                    cg.text = Instantiate(texttemp, new Vector2(origonalTransform2.position.x, origonalTransform2.position.y) + new Vector2(y * 220, -x * 300 - 150), Quaternion.identity);
                    cg.count = cards_you_have[(x * 3) + y].count;
                    cg.text.text = "cards you can use:" + cards_you_have[(x * 5) + y].count.ToString();
                    cg.text.transform.parent = origonalTransform2;
                    cg.card.transform.parent = origonalTransform2;
                    cg.card.transform.localScale = new Vector2(0.8f, 0.8f);
                    cg.card.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                    cg.card.GetComponent<CardDisplay>().card = cards_you_have[(x * 5) + y].card.GetComponent<CardDisplay>().card;
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        origonalTransform.position = new Vector2(origonalTransform.position.x ,6500 * sb.value+origonalsbYvalue);
        sb.size = 0;

        origonalTransform2.position = new Vector2(origonalTransform2.position.x, 6500 * sb2.value + origonalsbYvalue2);
        sb2.size = 0;

        for(int i = 0; Collection.Length > i; i++)
        {
            Collection[i].text.text="cards you can use:" + Collection[i].count.ToString();
        }
    }
    public void save_cards()
    {
        //load cards
        for (int i = 0; deaks.Count > i; i++) {
            static_collections.Deaks[i].Class = deaks[i].Class;
            for (int x = 0; 40 > x; x++)
            {
                static_collections.Deaks[i].deck[x] = deaks[i].deck[i];
            }
        }
        for (int i = 0; id.Length > i; i++)
        {
            static_collections.Collection[i] = Collection[i].count;
        }

        temp_collection tmpc = new temp_collection();
        tmpc.temp_collection_save();
        

    }

    public void load_cards()
    {
        save_system.LoadSaveData();
        for(int i=0; static_collections.Collection.Length > i; i++)
        {
            Collection[i].count= static_collections.Collection[i];
        }
        for(int i=0;static_collections.Deaks.Count > i; i++)
        {
            Deak loadeddeack = new Deak();
            loadeddeack.Class = static_collections.Deaks[i].Class;
            for (int x=0; 40>x; x++)
            {

                loadeddeack.deak[x] = id[static_collections.Deaks[i].deck[x]];
            }
        }
        
    }

    public void createNewDeck()
    {
        deackBeingCreated = new serilisable_deak();
        cardsInCreateDeak = 0;
        StartCoroutine(waitForFullDeck());
    }

    IEnumerator waitForFullDeck()
    {
        yield return new WaitUntil(() => cardsInCreateDeak == 40);
        //finish building deack
        print("deack created");
    }
}






