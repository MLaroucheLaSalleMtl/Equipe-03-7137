using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct Statistics
{
    public int Defense;
    public int Attack;
    public int Luck;
    public int Support;
    public int Strength;
    public int Agility;
    public int Endurance;
    public int Wisdom;
    public int Intelligence;
}

public abstract class DummyBaseClass
{
    //Manages user's name.//
    public string name;

    //Manages the starting HP and current HP.//
    public float baseHP;
    public float currentHP;

    //Manages the starting MP and current MP.//
    public float baseMP;
    public float currentMP;

    //User's attributes.//
    /*//public int Strength;     //=> Influences attacks.//
    //public int Constitution; //=> Influences maximum HP the MC can have.//
    //public int Defense;      //=> Used to reduce incoming attacks - Could be armor based.//
    //public int Intelligence; //=> could be used for spells and such.//
    //public int Luck;         //=> Influences the RUN command during a match and evasive attacks.//*/
    public Statistics baseStats = new Statistics();

    //Image for items
    public string image;

    public DummyBaseClass(string name, Statistics baseStats, string image)
    {
        this.name = name;
        this.baseStats = baseStats;
        this.image = image;
    }

    public DummyBaseClass(string name, float baseHP, float currentHP, float baseMP, float currentMP, Statistics baseStats)
    {
        this.name = name;
        this.baseHP = baseHP;
        this.currentHP = currentHP;
        this.baseMP = baseMP;
        this.currentMP = currentMP;
        this.baseStats = baseStats;
    }

    //Types of actions
    public virtual void Attack()
    {

    }
    public virtual void Defend()
    {

    }
    public virtual void UseItem()
    {

    }
}

