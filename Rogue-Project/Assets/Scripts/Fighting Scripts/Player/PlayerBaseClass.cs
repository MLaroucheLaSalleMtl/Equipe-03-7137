using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerBaseClass : DummyBaseClass
{
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
    public void Run()
    {

    }
}
