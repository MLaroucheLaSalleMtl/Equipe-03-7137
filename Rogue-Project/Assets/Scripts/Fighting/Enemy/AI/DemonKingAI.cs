using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DemonKingAI
{
    #region Variables
    //Script access.//
    readonly ColorHue ColorHues;

    //Consts.//
    //Attack Decision Maker.//
    private const int NORMAL_PERCENTAGE_MAX = 60;
    private const int OTHER_PERCENTAGE_MAX = 61;

    private const int MIN_PERCENTAGE_CRIT = 51;
    private const int MAX_PERCENTAGE_CRIT = 90;
    private const int SMITE_NECESSARY_HP = 50;

    private const float WAIT_TIME = 20.0F;
    private const float ATTACK_TIME = 5.0F;


    //Crits.//
    private const float REGULAR_CRITICAL = 1.25F;
    private const float HIGHER_CRITICAL = 1.75F;
    private const float HIGHEST_CRITICAL = 2.25F;

    //Bool.//
    private bool isInvisible = false;

    private int timer = 0;

    //GameObjects.//
    public GameObject DemonKing;
    public Image DemonKingImage;

    #endregion

    public void Attack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        attackDecisionMaker(EBC, PBS);
    }

    #region Generate Random Shite
    private int attackRandomGen()
    {
        int RandomValue = Random.Range(0, 100);

        return RandomValue;
    }


    private void attackDecisionMaker(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        int RandomValue = attackRandomGen();

        if (RandomValue <= NORMAL_PERCENTAGE_MAX)
        {
            Debug.Log("Normal Attack.");
            NormalAttack(EBC, PBS);
            return;
        }

        if (PBS.currentHP <= SMITE_NECESSARY_HP)
        {
            Debug.Log("Smite.");
            SmiteHit(EBC, PBS);
            return;
        }

        if(RandomValue <= OTHER_PERCENTAGE_MAX)
        {

        }
    }
    #endregion
    #region Types of Attacks
    private void NormalAttack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        PBS.currentHP -= EBC.currentAttack;
    }
    private void SmiteHit(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        PBS.currentHP = 0;
    }
    private void ObliterateAttack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {

        //Time.deltaTime
        //for(int i = 0; i < )
        //{

        //}
        PBS.currentHP -= EBC.currentAttack * HIGHER_CRITICAL;
    }
    private void DeadlyAttack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        PBS.currentHP -= EBC.currentAttack * HIGHEST_CRITICAL;
    }

    #endregion

}
