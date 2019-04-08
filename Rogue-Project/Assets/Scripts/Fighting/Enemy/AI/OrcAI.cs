using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrcAI : MonoBehaviour
{
    #region Variables
    //Script access.//
    readonly ColorHue ColorHues;
    EnemyBaseClass EBC;
    readonly PlayerBaseClass PBC;
    readonly BattleStateMachine BDSM;
    PlayerStateMachine PSM;

    //Consts.//
    private const int BERSERK_TRIGGER = 4; //At what treshold does the Orc goes berserk ? Max Health/Trigger
    private const int MAX_SECOND_WINDER = 1; //Maximum of times the Orc can second wind.//
    private const int MAX_BERSERK_TURN = 3; //Max numbers of times the Orc can stay in Berserk Mode.//
    private const float BERSERK_MULTI = 2.0F; //Muliplier of how much more damage the Orc does.//
    private const float HEAL = 10;

    //Bool.//
    private bool isBerserk = false;
    private bool isBerserkUsed = false;

    private bool isSecondWindUsed = false;

    private bool isLifeLow = false;


    //Variables.//
    private int BerserkTurns = 0;
    private int SecondWindTurns = 0;

    //GameObjects.//
    public GameObject OrcEnemy;
    public Image OrcImage;


    public enum EnemyState
    {
        BERSERKER,
        NORMAL_ATTACK,
        SECOND_WIND,
        HEALTH_CHECK
    }
    public EnemyState currentState;
    #endregion

    private void Start()
    {
        currentState = EnemyState.HEALTH_CHECK;
    }

    void Update()
    {

    }

    public void Attack()
    {
        switch (currentState)
        {
            case EnemyState.HEALTH_CHECK:
                CheckHealth();
                break;


            case EnemyState.BERSERKER: // => Maximum of three turns as Berserker.//
                BerserkTurns++; //Increments to limit the amount of turns.//
                Berserker();
                break;

            case EnemyState.NORMAL_ATTACK:
                normalAttack();
                break;

            case EnemyState.SECOND_WIND:
                SecondWind();
                SecondWindTurns++;
                break;
        }
    }

    #region Actions
    private void SecondWind()
    {
        EBC.currentHP += HEAL;
       
        currentState = EnemyState.HEALTH_CHECK;
    }

    private void Berserker()
    {
        PSM.PBS.currentHP -= EBC.currentAttack * BERSERK_MULTI;
        currentState = EnemyState.HEALTH_CHECK;
    }

    private void normalAttack()
    {
        
        PSM.PBS.currentHP -= EBC.currentAttack;
        currentState = EnemyState.HEALTH_CHECK;
    }

    private void CheckHealth()
    {
        //Check if the current health is smaller than the Base Health divided by the Berserk Modifier.//
        if ((EBC.currentHP <= EBC.baseHP / BERSERK_TRIGGER) && (!isBerserkUsed))  
        {
            isLifeLow = true;
            isBerserk = true;
            isBerserkUsed = true;

            OrcImage.color = ColorHue.ColourValue(ColorHue.ColorHues.Red);
            currentState = EnemyState.BERSERKER;

            return; 
        }
        if ((isLifeLow) && (SecondWindTurns < MAX_SECOND_WINDER) && (!isBerserk))
        {
            isSecondWindUsed = true;
            OrcImage.color = ColorHue.ColourValue(ColorHue.ColorHues.Golden);
            currentState = EnemyState.SECOND_WIND;

            return;
        }
        else
        {
            currentState = EnemyState.NORMAL_ATTACK;
            return;
        }
    }

    #endregion
}
