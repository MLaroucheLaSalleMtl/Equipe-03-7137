using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    public DummyBaseClass DBC;
    
    //Attacks Descriptions.//
    public string AttacksName;
    public string AttacksDescription;

    //Damage;
    public float Damage; //Extra damaged added due to Strength Stat => Damage + (Strength Stats * Multiplier)
}
