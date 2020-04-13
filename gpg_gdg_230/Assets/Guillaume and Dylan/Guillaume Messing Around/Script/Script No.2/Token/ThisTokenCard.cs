using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThisTokenCard : MonoBehaviour
{
    //This is for the card deatils in our game.
    public List<CardVersion2> thisCard = new List<CardVersion2>();
    public int thisID;

    public int id;
    public string thisCardName;
    public string thisCardDetails;
    public string thisCardType;
    public int thisCardCost;
    public int thisCardAttack;
    public int thisCardHealth;


    public Text nameText;
    public Text deatilText;
    public Text typeText;
    public Text costText;
    public Text attackText;
    public Text healthText;

    public Sprite thisCardSprite;
    public Image cardImage;

    public bool cardBack;
    //public static bool staticCardBack;

    //This is to make sure the card go to the hand.
    public GameObject field;

    //Seting up the summoning mechanic
    public bool canBeSummon;
    public bool summoned;
    public GameObject battleZone;

    //These are forbeing able to attack or not
    // and which one to attack.
    public GameObject ableToAttackObject;
    public GameObject target;
    public GameObject enemy;

    public bool summoningSickness;
    public bool cantAttack;
    public bool canAttack;

    public static bool staticTargeting;
    public static bool staticTargetingEnemy;

    public bool targeting;
    public bool targetingEnemy;

    public bool onlyThisCardAttack;

    public CardsOnTheField cardsOnThefield;
    public GameObject fieldObject;
    public GameObject cardObject;

    // Start is called before the first frame update
    void Start()
    {
        thisCard[0] = CardDataBase.cardList[0];
        //Changing the tag of Token in order to make it be able to summon it to the field.
        //Instead of it being summoned to the hand.
        if (thisCard[0].cardID == 0)
        {
            this.tag = "Token";
        }
        else
            this.tag = thisCard[0].cardType;

        canBeSummon = false;
        summoned = false;

        canAttack = false;
        summoningSickness = true;

        enemy = GameObject.Find("EnemyHealth");

        targeting = false;
        targetingEnemy = false;

        fieldObject = GameObject.Find("Field");
        cardsOnThefield = fieldObject.GetComponent<CardsOnTheField>();
    }

    // Update is called once per frame
    void Update()
    {
        field = GameObject.Find("Field");
        if (this.transform.parent == field.transform.parent)
        {
            cardBack = false;
        }

        id = thisCard[0].cardID;
        thisCardName = thisCard[0].cardName;
        thisCardDetails = thisCard[0].cardDetail;
        thisCardType = thisCard[0].cardType;
        thisCardCost = thisCard[0].cardCoinCost;
        thisCardSprite = thisCard[0].cardImage;

        if (summoned == false)
        {
            thisCardAttack = thisCard[0].cardAttack;
            thisCardHealth = thisCard[0].cardHealth;
        }

        nameText.text = "" + thisCardName;
        deatilText.text = "" + thisCardDetails;
        typeText.text = "" + thisCardType;
        costText.text = "" + thisCardCost;
        attackText.text = "" + thisCardAttack;
        healthText.text = "" + thisCardHealth;
        cardImage.sprite = thisCardSprite;

        //So right here it make sures that all the Requirements for this game to summon are in the perfect.
        //While also making sure that I dont flood the field.
        if (TurnSystem.currentCoin >= thisCardCost && summoned == false && TurnSystem.isYourTurn == true)
            canBeSummon = true;

        else
            canBeSummon = false;


        if (canBeSummon == true)
            gameObject.GetComponent<DraggableCard>().enabled = true;

        else
            gameObject.GetComponent<DraggableCard>().enabled = false;


        battleZone = GameObject.Find("Field");

        if (summoned == false && this.transform.parent == battleZone.transform)
            Summon();

        //This is the setup for the battle System.
        //I still need to fix it where the player chose to attack and the opponent must who to defend with.
        if (canAttack == true)
            ableToAttackObject.SetActive(true);
        else
            ableToAttackObject.SetActive(false);

        if (TurnSystem.isYourTurn == false && summoned == true)
        {
            summoningSickness = false;
            cantAttack = false;
        }

        if (TurnSystem.isYourTurn == true && summoningSickness == false && cantAttack == false)
            canAttack = true;
        else
            canAttack = false;

        targeting = staticTargeting;
        targetingEnemy = staticTargetingEnemy;

        if (targetingEnemy == true)
            target = enemy;
        else
            target = null;

        if (targeting == true && targetingEnemy == true && onlyThisCardAttack == true)
            Attack();

        if (thisCardHealth <= 0)
        {
            Invoke("DestroyMonster", 1.5f);
        }
    }

    public void Summon()
    {
        summoned = true;
        cardsOnThefield.fieldCards.Add(cardObject);
        cardsOnThefield.tokenCardStats.Add(this);
        CheckFieldLimit();

    }

    public void CheckFieldLimit()
    {
        if (cardsOnThefield.fieldCards.Count >= 6)
        {
            cardsOnThefield.fieldCards.Remove(cardObject);
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        if (canAttack == true)
        {
            if (target != null)
            {
                if (target == enemy)
                {
                    EnemyHp.staticHp -= thisCardAttack;
                    targeting = false;
                    cantAttack = true;
                }

                if (target.name == "CardToHand(Clone)")
                {
                    canAttack = true;
                }
            }
        }
    }

    public void UntargetedEdEnemy()
    {
        staticTargetingEnemy = false;
    }

    public void TargetedEnemy()
    {
        staticTargetingEnemy = true;
    }

    public void StartAttack()
    {
        staticTargeting = true;
    }

    public void StopAttack()
    {
        staticTargeting = false;
    }

    public void OneCardAttack()
    {
        onlyThisCardAttack = true;
    }

    public void OneCardAttackStop()
    {
        onlyThisCardAttack = false;
    }

    public void DestroyMonster()
    {
        cardsOnThefield.fieldCards.Remove(cardObject);
        cardsOnThefield.tokenCardStats.Remove(this);
        Destroy(gameObject);
    }
}
