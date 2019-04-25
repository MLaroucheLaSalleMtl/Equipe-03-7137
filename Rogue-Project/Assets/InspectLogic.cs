using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectLogic : MonoBehaviour
{
    public Text text;
    public EnemyStateMachine esm;
    public Button returno;

    // Update is called once per frame
    void Update()
    {
        if (esm != null)
        {
            text.text = $"{esm.EBS.enemyName}\nLvl: {(int)esm.EBS.level}\nHP: {(int)esm.EBS.currentHP}\nAtk: {(int)esm.EBS.currentAttack}\nDef: {(int)esm.EBS.currentDefense}";
        }
    }
}
