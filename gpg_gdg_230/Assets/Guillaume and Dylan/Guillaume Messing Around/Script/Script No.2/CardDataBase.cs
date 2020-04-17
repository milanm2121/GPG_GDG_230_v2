using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    public static List<CardVersion2> cardList = new List<CardVersion2>();

    private void Awake()
    {
        //Layout for writting cards
        //CardVersion2(int ID, string Name, string Detail, string Type, int Cost, int Attack, int Health, Sprite Image, int DrawXCards, int AddXMaxCoin, int BuffATK, int BuffHealth, int SummonMonster, bool Blocker, int BuffOtherATK, int BuffOtherHealth, bool DontBuffThisUnit, int HealXHealth, bool IsSpellCard)

        #region Guillaume
        //Tokens
        cardList.Add(new CardVersion2(001, "Trooper Token", "I dont exist in the deck", "Soldier", 1, 3, 3, Resources.Load<Sprite>("0"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));

        //Cards
        cardList.Add(new CardVersion2(1, "Sniper Trooper", "On play: Deal 1 damage", "Soldier", 2, 1, 1, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(2, "Trooper", "They've been trained to kill", "Soldier", 3, 3, 3, Resources.Load<Sprite>("2"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(3, "Shield Trooper", "Defender", "Soldier", 2, 1, 3, Resources.Load<Sprite>("3"), 0, 0, 0, 0, 0, true, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(4, "Sword Trooper", "On play: give other cards +1/+0", "Soldier", 3, 3, 2, Resources.Load<Sprite>("3"), 0, 0, 0, 0, 0, false, 1, 0, true, 0, false));
        cardList.Add(new CardVersion2(5, "Support Trooper", "On play: give other cards +0/+1", "Soldier", 4, 2, 3, Resources.Load<Sprite>("4"), 0, 0, 0, 0, 0, false, 0, 1, true, 0, false));
        cardList.Add(new CardVersion2(6, "The Commander of Trooper", "On play: summon x2 Trooper", "Soldier", 5, 4, 4, Resources.Load<Sprite>("5"), 0, 0, 0, 0, 2, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(7, "The Genral of Trooper", "On play: summon x1 Trooper and give other Soldier +1/+1", "Soldier", 5, 4, 3, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 1, false, 1, 1, true, 0, false));
        cardList.Add(new CardVersion2(8, "Frost Trooper", "On play: Stun 1 enemy unit", "Soldier", 3, 3, 2, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(9, "Air Trooper", "On play: draw 2 cards", "Soldier", 4, 4, 2, Resources.Load<Sprite>("6"), 2, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(10, "Stealth Trooper", "Heal 3", "Soldier", 1, 1, 1, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 0, 0, false, 3, false));
        cardList.Add(new CardVersion2(11, "Electric Trooper", "Summon 3 Monster", "French", 1, 1, 1, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(12, "Burner Trooper", "Draw 1 Card", "French", 4, 2, 5, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(13, "The Leader", "Add 1 Max Mana", "French", 5, 4, 4, Resources.Load<Sprite>("2"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(14, "Buff Trooper", "Buff 3 ATK and 3 DEF", "French", 1, 2, 1, Resources.Load<Sprite>("3"), 0, 0, 3, 3, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(15, "Elite Trooper", "Buff 1 ATK", "French", 0, 1, 1, Resources.Load<Sprite>("4"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(16, "Rogue Trooper", "Buff 3 Defence", "French", 7, 8, 8, Resources.Load<Sprite>("5"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(17, "The Leader's Apprentice", "Summon 3 Monster", "French", 1, 1, 1, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(18, "Security Trooper", "Summon 3 Monster", "French", 1, 1, 1, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(19, "Assassin Trooper", "Summon 3 Monster", "French", 1, 1, 1, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(20, "Super Trooper", "Summon 3 Monster", "French", 1, 1, 1, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(21, "Storm in Trooper", "Give all Solider unit +1/+1", "Spell", 3, 0, 0, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 1, 1, false, 0, true));
        cardList.Add(new CardVersion2(22, "Training Camp", "Draw 1 Card", "French", 4, 2, 5, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, true));
        cardList.Add(new CardVersion2(23, "Call for reinforcement", "Add 1 Max Mana", "French", 5, 4, 4, Resources.Load<Sprite>("2"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, true));
        cardList.Add(new CardVersion2(24, "We need to prepare", "Buff 3 ATK and 3 DEF", "French", 1, 2, 1, Resources.Load<Sprite>("3"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, true));
        cardList.Add(new CardVersion2(25, "Uprgade", "Buff 1 ATK", "French", 0, 1, 1, Resources.Load<Sprite>("4"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, true));

        #endregion



        #region Aiden
        //Tokens


        //Cards
        cardList.Add(new CardVersion2(26, "AI Tank", "A futuristic tank that functions on it's own Enduring", "Vehicle", 6, 4, 5, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(27, "Mercenary", "A futuristic mercenary soldier, Deal 2 damage to unit and earn one gold", "Soldier", 3, 2, 2, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(28, "Undercover Cop", "They have the power of disguise, Haste, Charged - Player earns 1 gold and 1 mana", "Cop", 3, 1, 2, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(29, "Helicopter", "They have the high ground", "Vehicle", 4, 3, 4, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(30, "Robotic AI Worm", "Burrowing is the best fun", "Creature - AI", 6, 3, 3, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(31, "Stationary AI Turret", "You are always within sight, Immbobile, Enduring", "Vehicle", 5, 4, 3, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(32, "Super Magnet Car", "Attracts enemy resources using magnets, Immobile", "Vehicle", 5, 2, 2, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(33, "Robot Biker Troop", "Zooms past enemy bullets", "Vehicle", 3, 3, 2, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(34, "Transport Jeep", "Transports soldiers and reinforcements, Summon 4 Trooper Cards on play", "Vehicle", 2, 2, 2, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(35, "Hoverboard Troop", "Too tricky for your foes", "Vehicle", 3, 2, 1, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(36, "AI Artillery Gun", "Fires of it's own accord, Swarm - Deals 3 damage to all units", "Vehicle", 4, 4, 2, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(37, "Robotic AI Scorpion", "The sting of the 21st century, Deals 4 damage only to enemy vehicle units", "Creature - AI", 3, 2, 3, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(38, "AI Turret Jeep", "A jeep with a self operating AI turret, Enduring", "Vehicle", 2, 2, 2, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(39, "AI Artillery Stun Gun", "Disables enemy vehicles and equipment, Charged", "Artillery", 3, 3, 3, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(40, "AI Artillery Laser Gun", "Cuts through the enemy plans, Deals 5 damage to enemy vehicle units", "Artillery", 5, 4, 3, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(41, "AI Artillery Nanite Gun", "Beware the power of nanites, Enduring, Charged", "Vehicle", 5, 4, 4, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(42, "Commander General", "He is the boss, Immobile, 50% dodge - Summon 3 trooper cards on play", "Soldier", 6, 2, 4, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(43, "Portable Drone", "Flies at your command, ", "Vehicle", 5, 3, 1, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, false));
        cardList.Add(new CardVersion2(44, "Medic Van", "Help is on the way, Gives 2 health to all units", "Spell", 4, 0, 0, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, true));
        cardList.Add(new CardVersion2(45, "Resource Delivery Truck", "Supports your army with resources, Gain 3 Gold and 2 Mana", "Spell", 2, 0, 0, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, true));
        cardList.Add(new CardVersion2(46, "Nanotechnology Duplicator", "Quickly assembles new units with nanites", "Spell", 6, 0, 0, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, true));
        cardList.Add(new CardVersion2(47, "Self Driving AI Police Car", "Picks you up where ever you are, summons 2 under cover cops", "Spell", 3, 0, 0, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, true));
        cardList.Add(new CardVersion2(48, "Cybernetic Visual Aid", "Cybernetically enhances the vision of your troops, Give trooper unit(s) +1/+3", "Spell", 5, 0, 0, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, true));
        cardList.Add(new CardVersion2(49, "Dematerialization Cannon", "Obliterates your chosen target from existance, sacrifice one unit. damage all for 10", "Spell", 8, 0, 0, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, true));
        cardList.Add(new CardVersion2(50, "Holographic Forcefield", "Defense is the new offense, upgrade all unit -2 damage 3 health", "Spell", 5, 0, 0, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0, false, 0, true));

        #endregion


        #region Dylan

        #endregion

    
        #region Robert

        #endregion

       
        #region Ivan

        #endregion

        
        #region Milan

        #endregion
    }
}
