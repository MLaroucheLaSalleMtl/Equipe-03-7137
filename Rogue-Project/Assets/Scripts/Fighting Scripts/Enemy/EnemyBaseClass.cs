using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyBaseClass
{

    //To make different types of enemy based on their grade.//
    public enum EnemyGrade
    {
        COMMON,
        RARE,
        LEGENDARY
    }

    public EnemyGrade enemyGrade;

    //Manages enemy's name.//
    public string playerName;

    //Manages the starting HP and current HP.//
    public float baseHP;
    public float currentHP;

    //Manages the starting MP and current MP.//
    public float baseMP;
    public float currentMP;

    //Manages the Attacks and Defense of the enemy.//
    public float baseAttack;
    public float currentAttack;

    public float baseDefense;
    public float currentDefense;
}
