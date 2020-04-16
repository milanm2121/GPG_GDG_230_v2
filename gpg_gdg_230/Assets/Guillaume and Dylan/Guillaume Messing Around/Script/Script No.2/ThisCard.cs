using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//This is where most of the action takes place for this card game.
//There is it states in here, abilities it can use and if able to attack.
//Done by Guillaume Blanchard 
//Reference to where I got this help from is in this link:
//https://www.youtube.com/watch?v=KZ9VNbFP8Pg&list=PLOoQ0JTWjALQGkiDWw_ws21fanM2za02B
//Though I did manage to do a lot without it since it didn't show me how like:
//Buffing units, summoning Tokens, preventing of playing too many cards on the field,
// preventing of playing card during your opponent's turn (even thought there is a video
// of it, I didnt't watch it).
public class ThisCard : MonoBehaviour
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
    public GameObject hand;

    public int numberOfCardsInDeck;

    //Seting up the summoning mechanic
    public bool canBeSummon;
    public bool summoned;
    public GameObject battleZone;
    public GameObject spellZone;

    //Setting up spell mechanic
    public bool spellCanBeUse;
    public bool spellPlay;
    public bool thisIsASpellCard;

    //This is to be use for our abilities.
    //Need to make a summoning one.
    public static int drawX;
    public int drawXCards;
    public int addXMaxCoin;
    public int buffXATK;
    public int buffXHealth;
    public int summoningMonsters;
    public bool token = true;
    public GameObject tokenObject;
    public List<CardVersion2> tokenCards = new List<CardVersion2>();
    public int buffOtherCardsATK;
    public bool buffingOtherCardsATKBool = false;
    public int buffOtherCardsHealth;
    public bool buffingOtherCardsHealthBool = false;
    public bool dontBuffThisUnit;
    public int healXHealth;
    public bool canHeal;
    public bool isSpellCard;

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

    //Thi is to make the player know when they are able to summon/use their card.
    public GameObject ableToUseCard;

    public CardsOnTheField field;
    public GameObject fieldObject;
    public GameObject cardObject;

    public GameObject monsterCardTemplate;
    public GameObject spellCardTemplate;

    public GameObject attackTextObject;
    public GameObject healthTextObject;

    // Start is called before the first frame update
    void Start()
    {
        //Here is to make sure things are setup properly.
        thisCard[0] = CardDataBase.cardList[thisID];
        numberOfCardsInDeck = PlayerDeck.deckSize;

        canBeSummon = false;
        summoned = false;

        drawX = 0;

        canAttack = false;
        summoningSickness = true;

        enemy = GameObject.Find("EnemyHealth");

        targeting = false;
        targetingEnemy = false;
        thisIsASpellCard = false;

        fieldObject = GameObject.Find("Field");
        field = fieldObject.GetComponent<CardsOnTheField>();

        canHeal = false;
    }

    #region ThisCardUpdate
    // Update is called once per frame
    void Update()
    {
        hand = GameObject.Find("PlayerHand Version2");
        if (this.transform.parent == hand.transform.parent)
        {
            cardBack = false;
        }

        id = thisCard[0].cardID;
        thisCardName = thisCard[0].cardName;
        thisCardDetails = thisCard[0].cardDetail;
        thisCardType = thisCard[0].cardType;
        thisCardCost = thisCard[0].cardCoinCost;
        thisCardSprite = thisCard[0].cardImage;

        //This is so that the card ATK and Health will be able to change
        // when it is on the battle feild and either takes damage from 
        // opponent's card, or getting buff by your card.
        if (summoned == false)
        {
            thisCardAttack = thisCard[0].cardAttack;
            thisCardHealth = thisCard[0].cardHealth;
            buffOtherCardsATK = thisCard[0].buffOtherATK;
            buffOtherCardsHealth = thisCard[0].buffOtherHealth;
            dontBuffThisUnit = thisCard[0].dontBuffThisUnit;
        }

        drawXCards = thisCard[0].drawXCards;
        addXMaxCoin = thisCard[0].addXMaxCoin;
        buffXATK = thisCard[0].buffATK;
        buffXHealth = thisCard[0].buffHealth;
        summoningMonsters = thisCard[0].summonMonster;
        healXHealth = thisCard[0].healXHealth;
        isSpellCard = thisCard[0].isSpellCard;

        nameText.text = "" + thisCardName;
        deatilText.text = "" + thisCardDetails;
        typeText.text = "" + thisCardType;
        costText.text = "" + thisCardCost;
        attackText.text = "" + thisCardAttack;
        healthText.text = "" + thisCardHealth;
        cardImage.sprite = thisCardSprite;

        //staticCardBack = cardBack;

        if (this.tag == "Clone")
        {
            thisCard[0] = PlayerDeck.staticDeck[numberOfCardsInDeck - 1];
            numberOfCardsInDeck -= 1;
            PlayerDeck.deckSize -= 1;
            cardBack = false;
            //this.tag = "Untagged";
            this.tag = thisCard[0].cardType;
        }


        if (thisCardAttack == 0 && thisCardHealth == 0)
        {
            monsterCardTemplate.SetActive(false);
            attackTextObject.SetActive(false);
            healthTextObject.SetActive(false);
            spellCardTemplate.SetActive(true);
        }
        else
        {
            monsterCardTemplate.SetActive(true);
            attackTextObject.SetActive(true);
            healthTextObject.SetActive(true);
            spellCardTemplate.SetActive(false);
        }

        //So this was at the bottom of update to begin with which was one of my major problems in which
        // it kept summoning tokens over the limit of the field limit I have put in which is 5.
        //So at first I put it here and things were working but it still had another problem to where
        // I could delete them if it went over the field limit so I did make a new void to to it own Token summon,
        // but then release that having the tokens with this card script is a bit too much since it doesn't need
        // abilities in it. So I just made a new script to where I just copy, pasted and then just remove what 
        // the token don't need.
        /*
        //Changing the tag of Token in order to make it be able to summon it to the field.
        //Instead of it being summoned to the hand.
        if (thisCard[0].cardID == 0)
        {
            this.tag = "Token";
        }
        else
            this.tag = thisCard[0].cardType;
            */

        //So right here it make sures that all the Requirements for this game to summon are in the perfect.
        //While also making sure that I dont flood the field.
        if (TurnSystem.currentCoin >= thisCardCost && summoned == false && TurnSystem.isYourTurn == true && field.fieldCards.Count < 5 && this.tag != "Spell")
            canBeSummon = true;

        else 
            canBeSummon = false;

        if (TurnSystem.currentMana >= thisCardCost && spellPlay == false && TurnSystem.isYourTurn == true && this.tag == "Spell")
            spellCanBeUse = true;
        else
            spellCanBeUse = false;

        if (thisCardAttack == 0 && thisCardHealth == 0)
            thisIsASpellCard = true;
        else
            thisIsASpellCard = false;

        if (canBeSummon == true || spellCanBeUse == true)
            gameObject.GetComponent<DraggableCard>().enabled = true;
        else
            gameObject.GetComponent<DraggableCard>().enabled = false;

        battleZone = GameObject.Find("Field");
        spellZone = GameObject.Find("SpellField");

        if (spellPlay == false && this.transform.parent == spellZone.transform && this.tag == "Spell" && isSpellCard == true)
            PlaySpellCard();

        if (summoned == false && this.transform.parent == battleZone.transform && field.fieldCards.Count < 5 && this.tag != "Spell")
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

        //Making it visable for the Player to know if there able to summon or not.
        if (canBeSummon == true && TurnSystem.isYourTurn == true)
            ableToUseCard.SetActive(true);
        else
            ableToUseCard.SetActive(false);

        //Making a list for the token Ability.
        if (summoningMonsters > 0 && token == true)
            AddToken(summoningMonsters);
        if (tokenCards == null)
            return;

        //Killing the monster when it reaches 0 Health. 
        //Though might need to change this later as we have a card that revives cards
        // that were destroy in battle.
        if (thisCardHealth <= 0 && thisIsASpellCard == false)
        {
            Invoke("DestroyMonster", 1.5f);
        }

    }
    #endregion

    public void AddToken(int x)
    {
        for (int i = 0; i < x; i++)
        {
            tokenCards.Add(CardDataBase.cardList[0]);
        }
        token = false;
    }

    //This summons the card from the hand and check all the abilities to see if the card has one.
    public void Summon()
    {
        Debug.Log("summoning");
        TurnSystem.currentCoin -= thisCardCost;
        summoned = true;
        this.tag = thisCard[0].cardType;
        field.fieldCards.Add(cardObject);
        field.cardStats.Add(this);
        AddToken(summoningMonsters);

        SummoningTheMonster(summoningMonsters);


        MaxCoin(addXMaxCoin);
        drawX = drawXCards;
        BuffAttack(buffXATK);
        BuffHealth(buffXHealth);

        buffingOtherCardsATKBool = false;
        buffingOtherCardsHealthBool = false;
        dontBuffThisUnit = false;
        buffOtherCardsATK = 0;
        buffOtherCardsHealth = 0;
    }

    public void PlaySpellCard()
    {
        Debug.Log("Playing Spell Card" + thisCardName);
        TurnSystem.currentMana -= thisCardCost;
        spellPlay = true;
        AddToken(summoningMonsters);

        SummoningTheMonster(summoningMonsters);

        MaxCoin(addXMaxCoin);
        drawX = drawXCards;
        BuffAttack(buffXATK);
        BuffHealth(buffXHealth);
        Heal(healXHealth);

        buffingOtherCardsATKBool = false;
        buffingOtherCardsHealthBool = false;
        dontBuffThisUnit = false;
        buffOtherCardsATK = 0;
        buffOtherCardsHealth = 0;
        Invoke("DestroySpellCard", 1.5f);
    }

    //This is where all the abilities will be put in.
    //Make sure when making the next one to put then in here.
    #region Abilities List
    public void MaxCoin(int x)
    {
        TurnSystem.maxCoin += x;
        if (TurnSystem.maxCoin >= 10)
        {
            TurnSystem.maxCoin = 10;
        }
    }
    
    public void BuffAttack(int x)
    {
        if (buffOtherCardsATK > 0)
        {
            buffingOtherCardsATKBool = true;
            Debug.Log("Working Part 1");
            field.BuffOtherCardsATKStats(buffOtherCardsATK);
            field.BuffOtherTokenCardsATKStats(buffOtherCardsATK);
        }

        if (buffingOtherCardsATKBool == false)
        {
            thisCardAttack += x;
            attackText.text = thisCardAttack.ToString();
        }
    }

    public void BuffHealth(int x)
    {
        if (buffOtherCardsHealth > 0)
        {
            buffingOtherCardsHealthBool = true;
            Debug.Log("Yes");
            field.BuffOtherCardsHealthStat(buffOtherCardsHealth);
            field.BuffOtherTokenCardsHealthStat(buffOtherCardsHealth);
        }

        if (buffingOtherCardsHealthBool == false)
        {
            thisCardHealth += x;
            healthText.text = "" + thisCardHealth;
        }
    }

    public void SummoningTheMonster(int x)
    {
        for (int i = 0; i < tokenCards.Count; i++)
        {
            if (field.fieldCards.Count >= 5)
            {
                break;
            }
            else if (field.fieldCards.Count < 5)
            {
                Debug.Log("creating card: " + tokenObject);
                Instantiate(tokenObject);
                tokenCards.Remove(CardDataBase.cardList[0]);
                //if (!field.fieldCards.Contains(tokenObject))
                    //field.fieldCards.Add(tokenObject);
            }
        }
    }

    public void Heal(int x)
    {
        if (canHeal == true)
        {
            PlayerHp.staticHp = x;
            canHeal = false;
        }
    }
    #endregion

    #region Battling
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
    #endregion

    public void DestroyMonster()
    {
        field.fieldCards.Remove(cardObject);
        field.cardStats.Remove(this);
        Destroy(gameObject);
    }

    public void DestroySpellCard()
    {
        Destroy(gameObject);
    }
}
