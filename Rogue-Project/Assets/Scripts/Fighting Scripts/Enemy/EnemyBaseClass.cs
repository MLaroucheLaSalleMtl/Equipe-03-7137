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
public abstract class EnemyBaseClass : DummyBaseClass
{

    //To make different types of enemy based on their grade.//
    public EnemyGrade enemyGrade;

    
}
