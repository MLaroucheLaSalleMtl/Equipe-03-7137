using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public int level;

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

    //Sprites
    public string spritePath;

    //Script access.//
    public GoblinAI GobAi;
    public OrcAI OrcAi;
    public ElfAI ElfAi;

    public static EnemyBaseClass Goblin()
    {
        
        return new EnemyBaseClass()
        {
            GobAi = new GoblinAI(),

            enemyName = "Goblin",
            level = 3,
            baseHP = 10,
            currentHP = 10,
            baseAttack = 10,
            currentAttack = 10,
            baseDefense = 10,
            currentDefense = 10,
            baseMP = 10,
            currentMP = 10,
            expGiven = 10,
            spritePath = "Sprites and TileMaps/Enemies and Characters/Enemies Sprites/Goblin"

        };

        
    }

    public static EnemyBaseClass Orc() {
        return new EnemyBaseClass()
        {
            OrcAi = new OrcAI(),

            enemyName = "Orc",
            level = 4,
            baseHP = 10,
            currentHP = 10,
            baseAttack = 20,
            currentAttack = 20,
            baseDefense = 5,
            currentDefense = 5,
            baseMP = 10,
            currentMP = 10,
            expGiven = 20,
            spritePath = "Sprites and TileMaps/Enemies and Characters/Enemies Sprites/Orc"

        };
    }public static EnemyBaseClass Elf() {
        return new EnemyBaseClass()
        {
            ElfAi = new ElfAI(),

            enemyName = "Elf",
            level = 5,
            baseHP = 10,
            currentHP = 10,
            baseAttack = 20,
            currentAttack = 20,
            baseDefense = 20,
            currentDefense = 20,
            baseMP = 10,
            currentMP = 10,
            expGiven = 25,
            spritePath = "Sprites and TileMaps/Enemies and Characters/Enemies Sprites/Elf"


        };
    }
}