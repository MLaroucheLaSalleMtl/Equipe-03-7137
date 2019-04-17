using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoblinAI 
{
    #region Variables
    //Script access.//
    readonly ColorHue ColorHues;

    //Consts.//
    private const int MAX_FRENZY = 1;
    private const float DAMAGE_MULTI= 1.5F;
    //Attack Decision Maker.//

    //Bool.//
    private bool hasReceivedDamage = false;

    private int frenzyCount;

    private float damageReceived;

    //GameObjects.//
    public GameObject GoblinEnemy;
    public Image GoblinImage;
    #endregion



    public void Attack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        pickAttack(EBC, PBS);
    }

    #region Type of Attacks
    private void NormalAttack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        PBS.currentHP -= EBC.currentAttack;
    }


    private void FrenzyAttack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        PBS.currentHP -= EBC.currentAttack * DAMAGE_MULTI;
        EBC.currentHP -= EBC.currentAttack;
    }
    #endregion

    private void CheckDamage(EnemyBaseClass EBC)
    {
        if (EBC.currentHP < EBC.baseHP)
        {
            hasReceivedDamage = true;
        }
    }

    private void pickAttack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        CheckDamage(EBC);

        if ((hasReceivedDamage) && (frenzyCount < MAX_FRENZY))
        {
            frenzyCount++;
            FrenzyAttack(EBC, PBS);
        }
        else
        {
            NormalAttack(EBC, PBS);
        }
    }

}
