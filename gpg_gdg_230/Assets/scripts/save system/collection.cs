using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class collection : MonoBehaviour
{
    //text box object
    public Text texttemp;

    [System.Serializable]
    //the card it count and ui count indicator
    public struct cardGroup
    {
        public GameObject card;
        public Text text;
        public int count;
        public int cardsYouCanUse;
    };
    public GameObject cardtemp;
    public GameObject spelltemp;
    public GameObject cardtempcreation;
    public GameObject spelltempcreation;
    public GameObject button;

    //all of the cards that you have and dont
    public cardGroup[] Collection = new cardGroup[150];
    //all the ID used to turn cards into ints
    public ScriptableCard[] id = new ScriptableCard[150];

    //the cards you can used in deack building
    public List<cardGroup> cards_you_have = new List<cardGroup>();

    //used as the origonal transform of cards in collection
    public RectTransform origonalTransform;
    //used for scaling of cards
    public RectTransform offsetTransformX;
    public RectTransform offsetTransformY;
    float xoffset;
    float yoffset;

    //used for the transform for deackbuilding
    public RectTransform origonalTransform2;
    //used for scaling cards
    public RectTransform offsetTransformX2;
    public RectTransform offsetTransformY2;
    float xoffset2;
    float yoffset2;

    //used for deack buton positioning
    public RectTransform origonalTransform3;
    //used for scaling deackbuttos
    public RectTransform offsetTransformX3;
    float xoffset3;

    public Scrollbar sbForCollecion;
    float origonalsbYvalue;
    public Scrollbar sbForCreation;
    float origonalsbYvalue2;
    
    public List<serilisable_deak> deaks = new List<serilisable_deak>();
    public List<GameObject> deackButtons = new List<GameObject>();

    public serilisable_deak deackBeingCreated;
    public int cardsInCreateDeak=0;

    public bool cancle_deck=false;

    public GameObject deckcreation;
    public GameObject decks_screan;
    public TMP_Text deck_building_count;
    public serilisable_deak selected_deck;

    public GameObject cards_button;
    public GameObject Deck_button;

    public AudioSource AS;

    public Button[] deackbuiling_dissable;

    // Start is called before the first frame update
    void Start()
    {
        configuerScale();

        load_cards();


        origonalsbYvalue = origonalTransform.GetComponent<RectTransform>().position.y;
        origonalsbYvalue2 = origonalTransform2.GetComponent<RectTransform>().position.y;
        //load collection
        for (int x = 0; 30 > x; x++)
        {
            for (int y = 0; 5 > y; y++)
            {
                if (id[(x * 5) + y].isSpell)
                {
                    Collection[(x * 5) + y].card = Instantiate(spelltemp, new Vector2(origonalTransform.position.x, origonalTransform.position.y) + new Vector2(y * xoffset, -x * yoffset), Quaternion.identity);
                }
                else
                {
                    Collection[(x * 5) + y].card = Instantiate(cardtemp, new Vector2(origonalTransform.position.x, origonalTransform.position.y) + new Vector2(y * xoffset, -x * yoffset), Quaternion.identity);
                }
                Collection[(x * 5) + y].text = Instantiate(texttemp, new Vector2(origonalTransform.position.x, origonalTransform.position.y) + new Vector2(y * xoffset, -x * yoffset - yoffset/2), Quaternion.identity);
                if (((x * 5) + y) <= 40)
                {
                    Collection[(x * 5) + y].count = 4;
                }
                else
                {
                    Collection[(x * 5) + y].count = static_collections.Collection[(x * 5) + y];
                }
                Collection[(x * 5) + y].text.text = "cards you have:" + Collection[(x * 5) + y].count.ToString();
                Collection[(x * 5) + y].text.transform.parent = origonalTransform;
                Collection[(x * 5) + y].text.GetComponent<RectTransform>().localScale= new Vector2(0.5f, 0.5f);
                Collection[(x * 5) + y].card.transform.parent = origonalTransform;
                Collection[(x * 5) + y].card.transform.localScale = new Vector2(0.8f, 0.8f);
                Collection[(x * 5) + y].card.GetComponent<RectTransform>().localScale = new Vector2(0.5f, 0.5f);
                Collection[(x * 5) + y].card.GetComponent<CardDisplay>().card = id[(x * 5) + y];
                Collection[(x * 5) + y].card.GetComponent<CardDisplay>().card.ID = (x * 5) + y + 1;

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
                if ((x * 3) + y < cards_you_have.Count)
                {
                    cardGroup cg = new cardGroup();
                    if (cards_you_have[(x * 3) + y].card.GetComponent<CardDisplay>().card.isSpell==true)
                    {
                        cg.card = Instantiate(spelltempcreation, new Vector2(origonalTransform2.position.x, origonalTransform2.position.y) + new Vector2(y * xoffset2, -x * yoffset2), Quaternion.identity);
                    }
                    else
                    {
                        cg.card = Instantiate(cardtempcreation, new Vector2(origonalTransform2.position.x, origonalTransform2.position.y) + new Vector2(y * xoffset2, -x * yoffset2), Quaternion.identity);
                    }
                    
                    cg.text = Instantiate(texttemp, new Vector2(origonalTransform2.position.x, origonalTransform2.position.y) + new Vector2(y * xoffset2, -x * yoffset2-yoffset2/2), Quaternion.identity);
                    cg.count = cg.cardsYouCanUse = cards_you_have[(x * 3) + y].count;
                    cg.text.text = "cards you can use:" + cards_you_have[(x * 3) + y].count.ToString();
                    cg.text.transform.parent = origonalTransform2;
                    cg.card.transform.parent = origonalTransform2;
                    cg.card.transform.localScale = new Vector2(0.1f, 0.1f);
                    cg.card.GetComponent<RectTransform>().localScale = new Vector2(0.7f, 0.7f);
                    cg.card.GetComponent<CardDisplay>().card = cards_you_have[(x * 3) + y].card.GetComponent<CardDisplay>().card;
                    cards_you_have[(x * 3) + y] = cg;
                }
            }
        }
        save_cards();
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 0; 30 > x; x++)
        {
            for (int y = 0; 5 > y; y++)
            {
                Collection[(x * 5) + y].count = static_collections.Collection[(x * 5) + y];
            }
        }
        


        origonalTransform.position = new Vector2(origonalTransform.position.x ,10000 * sbForCollecion.value+origonalsbYvalue);
        sbForCollecion.size = 0;

        configuerScale();
       // origonalTransform.localScale = new Vector3(xoffset/250,xoffset/250, 0);

        origonalTransform2.position = new Vector2((int)origonalTransform2.position.x, 14000 * 2 * sbForCreation.value + origonalsbYvalue2);
        sbForCreation.size = 0;

        for(int i = 0; Collection.Length > i; i++)
        {
            Collection[i].text.text="cards you have:" + Collection[i].count.ToString();
        }

        for(int i = 0; cards_you_have.Count > i; i++)
        {
            cards_you_have[i].text.text = "cards you can use:" + cards_you_have[i].card.GetComponent<deck_building_functions>().count;
        }

        deck_building_count.text = "Cards in deck:" + "\n" + cardsInCreateDeak + "/40";
    }
    public void save_cards()
    {
        //load cards
   //     static_collections.Deaks.Clear();

        static_collections.Deaks = deaks;

        
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
        
        deaks = static_collections.Deaks;
        
        if (deaks.Count != 0)
        {
            loadDeacks();
        }
    }

    public void createNewDeck()
    {
        deackBeingCreated = new serilisable_deak();
        cardsInCreateDeak = 0;
        StartCoroutine(waitForFullDeck());
        for(int i=0;deackbuiling_dissable.Length>i; i++)
        {
            deackbuiling_dissable[i].interactable = false;
        }
    
    }
    public void cancle_deack_creation()
    {
        //StopCoroutine(waitForFullDeck());
        
        for (int i = 0; cards_you_have.Count > i; i++)
        {
            cards_you_have[i].card.GetComponent<deck_building_functions>().ResetCount();
        }
        cancle_deck = true;
        deckcreation.SetActive(false);
        decks_screan.SetActive(true);
        for (int i = 0; deackbuiling_dissable.Length > i; i++)
        {
            deackbuiling_dissable[i].interactable = true;
        }
    }

    IEnumerator waitForFullDeck()
    {
        yield return new WaitUntil(() => cardsInCreateDeak == 40 || cancle_deck==true);
        if (cancle_deck == true)
        {
            cancle_deck = false;
            cards_button.SetActive(true);
            Deck_button.SetActive(true);
        }
        else
        {
            //finish building deack
            print("deack created");
            //pick a clss goes here



            //resets count of cards
            for (int i = 0; cards_you_have.Count > i; i++)
            {
                cards_you_have[i].card.GetComponent<deck_building_functions>().ResetCount();
            }
            //saves deack
            deaks.Add(deackBeingCreated);
            //adds deack to deack screan
            loadDeacks();
            deckcreation.SetActive(false);
            AS.Play();
            decks_screan.SetActive(true);
            for (int i = 0; deackbuiling_dissable.Length > i; i++)
            {
                deackbuiling_dissable[i].interactable = true;
            }
            cards_button.SetActive(true);
            Deck_button.SetActive(true);
        }
    }
    public void loadDeacks()
    {
        for (int i=0;deackButtons.Count>i;i++)
        {
            Destroy(deackButtons[i]);
        }
        if (deaks.Count > 0)
        {
            for (int i = 0; deaks.Count > i; i++)
            {
                GameObject x = Instantiate(button, new Vector2(origonalTransform3.transform.position.x, origonalTransform3.transform.position.y) + new Vector2(i * xoffset3, 0), Quaternion.identity);
                x.GetComponent<load_deck>().deck = i;
                x.transform.parent = origonalTransform3;
                deackButtons.Add(x);
            }
        }

    }
    public void configuerScale()
    {
        xoffset = offsetTransformX.position.x- origonalTransform.position.x;
        yoffset = origonalTransform.position.y - offsetTransformY.position.y;

        xoffset2 = offsetTransformX2.position.x - origonalTransform2.position.x;
        yoffset2 = origonalTransform2.position.y - offsetTransformY2.position.y;

        xoffset3 = offsetTransformX2.position.x - origonalTransform2.position.x;
    }

    public void setScrolbar()
    {
        StartCoroutine(setSb());
    }

    public void Delete_Deck()
    {
        deaks.Remove(selected_deck);
        selected_deck = null;
        loadDeacks();
    }

    IEnumerator setSb()
    {
        yield return new WaitForEndOfFrame();
        sbForCollecion.value = 0;
        sbForCreation.value = 0;
    }
}






