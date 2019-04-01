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
    public string enemyName;

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

    //EXP
    public float expGiven;

    public static EnemyBaseClass Goblin() {
        return new EnemyBaseClass()
        {
            enemyName = "Goblin",
            baseHP = 10,
            currentHP = 10,
            baseAttack = 10,
            currentAttack = 10,
            baseDefense = 10,
            currentDefense = 10,
            baseMP = 10,
            currentMP = 10,
            expGiven = 10

        };
    }public static EnemyBaseClass Orc() {
        return new EnemyBaseClass()
        {
            enemyName = "Orc",
            baseHP = 20,
            currentHP = 20,
            baseAttack = 20,
            currentAttack = 20,
            baseDefense = 5,
            currentDefense = 5,
            baseMP = 10,
            currentMP = 10,
            expGiven = 20

        };
    }public static EnemyBaseClass Elf() {
        return new EnemyBaseClass()
        {
            enemyName = "Elf",
            baseHP = 10,
            currentHP = 10,
            baseAttack = 20,
            currentAttack = 20,
            baseDefense = 20,
            currentDefense = 20,
            baseMP = 10,
            currentMP = 10,
            expGiven = 25

        };
    }
}