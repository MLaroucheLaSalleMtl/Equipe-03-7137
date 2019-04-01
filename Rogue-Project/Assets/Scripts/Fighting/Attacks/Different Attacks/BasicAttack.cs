using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : BaseAttack
{
    //private const float STRENGTH_MULTI = 1.5f;

    //PlayerBaseClass PBC = GameManager.gameManager.MainCharacter.GetComponent<PlayerBaseClass>();

    public BasicAttack()
    {
        //Attack's Description.//
        AttacksName = "UltraDimensional Cutting Sword Edge of Destruction";
        AttacksDescription = "Attacks the enemy with all his might!";

        //Attack's Damage.//
        Damage = 100.0f;
    }
}
