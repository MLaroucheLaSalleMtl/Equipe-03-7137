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
    public DemonKingAI DKAi;

    public static EnemyBaseClass Goblin()
    {
        
        return new EnemyBaseClass()
        {
            GobAi = new GoblinAI(),

            enemyName = "Goblin",
            level = 3,
            baseHP = 75,
            currentHP =75,
            baseAttack = 8,
            currentAttack = 8,
            baseDefense = 3,
            currentDefense = 3,
            baseMP = 2,
            currentMP = 2,
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
            baseHP = 100,
            currentHP = 100,
            baseAttack = 10,
            currentAttack = 10,
            baseDefense = 3,
            currentDefense = 3,
            baseMP = 10,
            currentMP = 10,
            expGiven = 20,
            spritePath = "Sprites and TileMaps/Enemies and Characters/Enemies Sprites/Orc"

        };
    }
    public static EnemyBaseClass Elf()
    {
        return new EnemyBaseClass()
        {
            ElfAi = new ElfAI(),

            enemyName = "Elf",
            level = 5,
            baseHP = 50,
            currentHP = 50,
            baseAttack = 5,
            currentAttack = 5,
            baseDefense = 3,
            currentDefense = 3,
            baseMP = 10,
            currentMP = 10,
            expGiven = 25,
            spritePath = "Sprites and TileMaps/Enemies and Characters/Enemies Sprites/Elf"
        };
    }

    public static EnemyBaseClass Boss()
    {
        return new EnemyBaseClass()
        {
            DKAi = new DemonKingAI(),

            enemyName = "Demon King",
            level = 50,
            baseHP = 500,
            currentHP = 500,
            baseAttack = 25,
            currentAttack = 25,
            baseDefense = 4,
            currentDefense = 4,
            baseMP = 0,
            currentMP = 0,
            expGiven = 500,
            spritePath = "Sprites and TileMaps/Enemies and Characters/Enemies Sprites/DemonKing1"
        };
    }
}