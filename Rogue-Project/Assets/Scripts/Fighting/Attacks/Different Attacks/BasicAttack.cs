using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : BaseAttack
{
    private const float STRENGTH_MULTI = 1.5f;

    DummyBaseClass DBC;

    public BasicAttack()
    {
        //Attack's Description.//
        AttacksName = "UltraDimensional Cutting Sword Edge of Destruction";
        AttacksDescription = "Attacks the enemy with all his might!";

        //Attack's Damage.//
        Damage = 10.0f + (DBC.baseStats.Strength * STRENGTH_MULTI); // => Extra damages added depending on MC's strength's value.//
    }
}
