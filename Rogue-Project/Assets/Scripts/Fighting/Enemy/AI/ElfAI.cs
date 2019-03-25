using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ElfAI : MonoBehaviour
{
    #region Variables
    //Script access.//
    readonly ColorHue ColorHues;
    EnemyBaseClass EBC;
    readonly PlayerBaseClass PBC;
    readonly BattleStateMachine BDSM;
    PlayerStateMachine PSM;

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

    public enum EnemyState
    {
        RANDOM_STATE,
        SHADOW_ATTACK,
        NORMAL_ATTACK,
        CRIT_ATTACK,
        DEADLY_ATTACK
    }

    public EnemyState currentState;
    #endregion

    private void Start()
    {
        currentState = EnemyState.RANDOM_STATE;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.RANDOM_STATE:
                attackDecisionMaker();
                break;

            case EnemyState.NORMAL_ATTACK:
                NormalAttack();
                break;

            case EnemyState.CRIT_ATTACK:
                CriticalHit();
                break;

            case EnemyState.SHADOW_ATTACK:
                AttackFromShadows();
                break;

            case EnemyState.DEADLY_ATTACK:
                DeadlyAttack();
                break;
        }
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

    private void attackDecisionMaker()
    {
        int value = attackRandomGen();

        if(value <= NORMAL_PERCENTAGE_MAX)
        {
            currentState = EnemyState.NORMAL_ATTACK;
            return;
        }

        if((value >= MIN_PERCENTAGE_CRIT) && (value <= MAX_PERCENTAGE_CRIT))
        {
            currentState = EnemyState.CRIT_ATTACK;
            return;
        }

        else
        {   
            ShadowDecisionMaker();
        }
    }

    private void ShadowDecisionMaker()
    {
        isInvisible = true;
        ElfImage.color = ColorHue.ColourValue(ColorHue.ColorHues.Black); // => Invisible ?

        int value = ShadowRandomGen();

        if(value <= HIGHER_CRIT_MAX_CHANCE) 
        {
            currentState = EnemyState.SHADOW_ATTACK;
        }
        else
        {
            currentState = EnemyState.DEADLY_ATTACK;
        }
    }
    #endregion
    #region Types of Attacks
    private void NormalAttack()
    {
        PSM.PBS.currentHP -= EBC.currentAttack;
        ResetEnemy();
    }
    private void CriticalHit()
    {
        PSM.PBS.currentHP -= EBC.currentAttack * REGULAR_CRITICAL;
        ResetEnemy();
    }
    private void AttackFromShadows()
    {
        PSM.PBS.currentHP -= EBC.currentAttack * HIGHER_CRITICAL;
        ResetEnemy();
    }
    private void DeadlyAttack()
    {
        PSM.PBS.currentHP -= EBC.currentAttack * HIGHEST_CRITICAL;
        ResetEnemy();
    }

    #endregion

    private void ResetEnemy()
    {
        currentState = EnemyState.RANDOM_STATE;
        ElfImage.color = ColorHue.ColourValue(ColorHue.ColorHues.BaseColor);
    }
}
