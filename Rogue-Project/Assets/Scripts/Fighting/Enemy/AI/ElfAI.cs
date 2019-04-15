using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ElfAI 
{
    #region Variables
    //Script access.//
    readonly ColorHue ColorHues;

    //Consts.//
    //Attack Decision Maker.//
    private const int NORMAL_PERCENTAGE_MAX = 50;
    private const int MIN_PERCENTAGE_CRIT = 51;
    private const int MAX_PERCENTAGE_CRIT = 90;

    //Shadow Decision Maker.//
    private const int HIGHER_CRIT_MAX_CHANCE = 90;

    //Crits.//
    private const float REGULAR_CRITICAL = 1.25F;
    private const float HIGHER_CRITICAL = 1.75F;
    private const float HIGHEST_CRITICAL = 2.25F;

    //Bool.//
    private bool isInvisible = false;

    //GameObjects.//
    public GameObject ElfEnemy;
    public Image ElfImage;

    #endregion

    public void Attack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        attackDecisionMaker(EBC, PBS);
    }

    #region Generate Random Shite
    private int attackRandomGen()
    {
        int value = Random.Range(0, 100);

        return value;
    }
    private int ShadowRandomGen()
    {
        int value2 = Random.Range(0, 10);

        return value2;
    }

    private void attackDecisionMaker(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        int value = attackRandomGen();

        if(value <= NORMAL_PERCENTAGE_MAX)
        {
            Debug.Log("Normal Attack.");
            NormalAttack(EBC, PBS);
            return;
        }

        if((value >= MIN_PERCENTAGE_CRIT) && (value <= MAX_PERCENTAGE_CRIT))
        {
            Debug.Log("Crit Attack.");
            CriticalHit(EBC, PBS);
            return;
        }

        else
        {   
            ShadowDecisionMaker(EBC, PBS);
        }
    }

    private void ShadowDecisionMaker(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        isInvisible = true;
        ElfImage.color = ColorHue.ColourValue(ColorHue.ColorHues.Black); // => Invisible ?

        int value = ShadowRandomGen();

        if(value <= HIGHER_CRIT_MAX_CHANCE) 
        {
            Debug.Log("Shadow Attack.");
            AttackFromShadows(EBC, PBS);
        }
        else
        {
            Debug.Log("Shadowiest Attack.");
            DeadlyAttack(EBC, PBS);
        }
    }
    #endregion
    #region Types of Attacks
    private void NormalAttack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        PBS.currentHP -= EBC.currentAttack;
        ResetEnemy();
    }
    private void CriticalHit(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        PBS.currentHP -= EBC.currentAttack * REGULAR_CRITICAL;
        ResetEnemy();
    }
    private void AttackFromShadows(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        PBS.currentHP -= EBC.currentAttack * HIGHER_CRITICAL;
        ResetEnemy();
    }
    private void DeadlyAttack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        PBS.currentHP -= EBC.currentAttack * HIGHEST_CRITICAL;
        ResetEnemy();
    }

    #endregion

    private void ResetEnemy()
    {
        //ElfImage.color = ColorHue.ColourValue(ColorHue.ColorHues.BaseColor); //To be implemented.//
    }
}
