using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{
    //Stores current enemies, player and turnhandler.//
    public List<TurnHandler> TurnList = new List<TurnHandler>();
    public List<GameObject> MainCharacter = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();

    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }
    public PerformAction battleState;

    void Start()
    {
        //Starts the fight as wait, no action yet.//
        battleState = PerformAction.WAIT;

        //Add the MC and the enemy(ies) into the list when starting the fight.//
        Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        MainCharacter.AddRange(GameObject.FindGameObjectsWithTag("MainCharacter"));
    }

    void Update()
    {
        switch (battleState)
        {
            case (PerformAction.WAIT):
                if (TurnList.Count > 0)
                {
                    battleState = PerformAction.TAKEACTION;
                }
                break;

            case (PerformAction.TAKEACTION):


                GameObject performer = GameObject.Find(TurnList[0].attackersName);

                if (TurnList[0].Type == "Enemy")
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    ESM.HeroAttack = TurnList [0].AttackersTarget;
                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                }

                if (TurnList[0].Type == "MainCharacter")

                {

                }

                battleState = PerformAction.PERFORMACTION;
                break;

            case (PerformAction.PERFORMACTION):
                break;
        }       
    }

    public void gatherActions(TurnHandler input)
    {
        TurnList.Add(input);
    }
}
