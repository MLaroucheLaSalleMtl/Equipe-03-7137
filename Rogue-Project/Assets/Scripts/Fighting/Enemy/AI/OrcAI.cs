using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrcAI
{
    #region Variables
    //Script access.//
    readonly ColorHue ColorHues;
   
   
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
        HEALTH_CHECK
    }
    public EnemyState currentState;
    #endregion

    private void Start()
    {
        currentState = EnemyState.HEALTH_CHECK;
    }

    void Update(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        Debug.Log(currentState);
        switch (currentState)
        {
            
            case EnemyState.HEALTH_CHECK:
                Debug.Log("Check Health - ORC");
                CheckHealth(EBC, PBS);
                break;
        }
    }

    public void Attack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        Debug.Log("Cyka");
        Update(EBC, PBS);
        
    }

    #region Actions
    private void SecondWind(EnemyBaseClass EBC)
    {
        Debug.Log("Healed - ORC");
        EBC.currentHP += HEAL;
    }

    private void Berserker(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        Debug.Log("Berserker - ORC");
        PBS.currentHP -= EBC.currentAttack * BERSERK_MULTI;
    }

    private void normalAttack(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        Debug.Log("Normal Attack- ORC");
        PBS.currentHP -= EBC.currentAttack;
    }

    private void CheckHealth(EnemyBaseClass EBC, PlayerBaseClass PBS)
    {
        Debug.Log("DEBUGGG");
        //Check if the current health is smaller than the Base Health divided by the Berserk Modifier.//
        if ((EBC.currentHP <= (EBC.baseHP / BERSERK_TRIGGER)) && (!isBerserkUsed))  
        {
            isLifeLow = true;
            isBerserk = true;
            isBerserkUsed = true;

            //OrcImage.color = ColorHue.ColourValue(ColorHue.ColorHues.Red);
            Berserker(EBC, PBS);
            BerserkTurns++;
        }
        if ((isLifeLow) && (SecondWindTurns < MAX_SECOND_WINDER) && (!isBerserk))
        {
            isSecondWindUsed = true;
            //OrcImage.color = ColorHue.ColourValue(ColorHue.ColorHues.Golden);
            SecondWind(EBC);
            SecondWindTurns++;
        }
        else
        {
            normalAttack(EBC, PBS);
        }
    }

    #endregion
}
