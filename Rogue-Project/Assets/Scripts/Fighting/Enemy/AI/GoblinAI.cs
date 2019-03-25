using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoblinAI : MonoBehaviour
{
    #region Variables
    //Script access.//
    readonly ColorHue ColorHues;
    EnemyBaseClass EBC;
    readonly PlayerBaseClass PBC;
    readonly BattleStateMachine BDSM;
    PlayerStateMachine PSM;

    //Consts.//
    private const int MAX_FRENZY = 1;
    //Attack Decision Maker.//

    //Bool.//
    private bool hasReceivedDamage = false;


    private int frenzyCount;

    private float damageReceived;



    //GameObjects.//
    public GameObject GoblinEnemy;
    public Image GoblinImage;

    public enum EnemyState
    {
        PICK_ATTACK,
        NORMAL_ATTACK,
        DEFENDING,
        FRENZY,
    }

    public EnemyState currentState;
    #endregion

    private void Start()
    {
        currentState = EnemyState.PICK_ATTACK;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.PICK_ATTACK:
                pickAttack();
                break;

            case EnemyState.NORMAL_ATTACK:
                NormalAttack();
                break;

            case EnemyState.FRENZY:
                FrenzyAttack();
                break;
        }
    }

    #region Type of Attacks
    private void NormalAttack()
    {
        PSM.PBS.currentHP -= EBC.currentAttack;
        currentState = EnemyState.PICK_ATTACK;
    }


    private void FrenzyAttack()
    {
        PSM.PBS.currentHP -= EBC.currentAttack;
        EBC.currentHP -= EBC.currentAttack;

        currentState = EnemyState.PICK_ATTACK;
    }
    #endregion

    private void CheckDamage()
    {
        if (EBC.currentHP < EBC.baseHP)
        {
            hasReceivedDamage = true;
        }
    }

    private void pickAttack()
    {
        CheckDamage();

        if ((hasReceivedDamage) && (frenzyCount < MAX_FRENZY))
        {
            frenzyCount++;
            currentState = EnemyState.FRENZY;
        }
        else
        {
            NormalAttack();
        }
    }

}
