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

    void Update(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        switch (currentState)
        {
            case EnemyState.PICK_ATTACK:
                pickAttack(EBC, PBS);
                break;

            case EnemyState.NORMAL_ATTACK:
                NormalAttack(EBC, PBS);
                break;

            case EnemyState.FRENZY:
                FrenzyAttack(EBC, PBS);
                break;
        }
    }

    public void Attack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        Update(EBC, PBS);
    }

    #region Type of Attacks
    private void NormalAttack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        PBS.currentHP -= EBC.currentAttack;
        Debug.Log("NormalAttack");
        currentState = EnemyState.PICK_ATTACK;
    }


    private void FrenzyAttack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        Debug.Log("Frenzy Attack");
        PBS.currentHP -= EBC.currentAttack * DAMAGE_MULTI;
        EBC.currentHP -= EBC.currentAttack;

        currentState = EnemyState.PICK_ATTACK;
    }
    #endregion

    private void CheckDamage(EnemyBaseClass EBC)
    {
        Debug.Log("Check Damage");
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
