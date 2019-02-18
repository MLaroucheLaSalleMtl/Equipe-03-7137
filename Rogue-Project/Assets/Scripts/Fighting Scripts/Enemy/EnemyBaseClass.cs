using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyGrade
{
    COMMON,
    RARE,
    LEGENDARY
}

[System.Serializable]
public class EnemyBaseClass : DummyBaseClass
{

    //To make different types of enemy based on their grade.//
    public EnemyGrade enemyGrade;

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Defend()
    {
        throw new System.NotImplementedException();
    }

    public override void UseItem()
    {
        throw new System.NotImplementedException();
    }
}
