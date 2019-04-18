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
    //Chance of each attacks.//
    private const int NORMAL_PERCENTAGE_MAX = 0;
    private const int OTHER_PERCENTAGE_MAX = 1;

    //Smite related.//
    private const int SMITE_NECESSARY_HP = 50;

    //Obliterate related.//
    private const int OBLITERATE_WAIT = 1000;
    private const int OBLITERATE_SUCCESSIVE = 3;
    private const float OBLITERATE_MULTI_INCREMENT = 0.75F;
   
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

        if (PBS.currentHP <= SMITE_NECESSARY_HP)
        {
            Debug.Log("Smite.");
            SmiteHit(EBC, PBS);
            return;
        }

        if (RandomValue <= NORMAL_PERCENTAGE_MAX)
        {
            Debug.Log("Normal Attack.");
            NormalAttack(EBC, PBS);
            return;
        }

        if (RandomValue >= OTHER_PERCENTAGE_MAX)
        {
            Debug.Log("Obliterate attack.");
            ObliterateAttack(EBC, PBS);
            return;
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
        float multiplier = 1;

        for(int i = 0; i < OBLITERATE_SUCCESSIVE;i++ )
        { 
            PBS.currentHP -= EBC.currentAttack * multiplier;
            multiplier += OBLITERATE_MULTI_INCREMENT;
            System.Threading.Thread.Sleep(OBLITERATE_WAIT);
        }
    }
    #endregion
}
