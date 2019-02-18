using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public abstract class DummyBaseClass
    {
    //Manages user's name.//
    public string name;

    //Manages the starting HP and current HP.//
    public int baseHP;
    public int currentHP;

    //Manages the starting MP and current MP.//
    public int baseMP;
    public int currentMP;

    //User's attributes.//
    public int Strength;     //=> Influences attacks.//
    public int Constitution; //=> Influences maximum HP the MC can have.//
    public int Defense;      //=> Used to reduce incoming attacks - Could be armor based.//
    public int Intelligence; //=> could be used for spells and such.//
    public int Luck;         //=> Influences the RUN command during a match and evasive attacks.//



    //Types of actions
    public abstract void Attack();
    public abstract void Defend();
    public abstract void UseItem();
}

