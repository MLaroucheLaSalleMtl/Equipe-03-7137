using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerBaseClass 
{
    //Manages player's name.//
    public string playerName;

    //Manages the starting HP and current HP.//
    public float baseHP;
    public float currentHP;

    //Manages the starting MP and current MP.//
    public float baseMP;
    public float currentMP;

    //Main character's attributes.//
    public int Strength;     //=> Influences attacks.//
    public int Constitution; //=> Influences maximum HP the MC can have.//
    public int Defense;      //=> Used to reduce incoming attacks - Could be armor based.//
    public int Intelligence; //=> could be used for spells and such.//
    public int Luck;         //=> Influences the RUN command during a match and evasive attacks.//
}
