using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{
    #region Variable, Classes and etc.

    //Lists.//
    public List<TurnHandler> TurnList = new List<TurnHandler>();
    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> MainCharacter = new List<GameObject>();
    public List<GameObject> PlayerManagement = new List<GameObject>();

    //Script Access.//
    private TurnHandler turnHandler;
    private GameManager GM;



    public enum BattleState
    {
        WAITING,
        TAKEACTION,
        PERFORMACTION,
    }
    public BattleState Current_Battle_State;

    #endregion

    #region Awake, Start, Update
    void Awake()
    {
        for (int i = 0; i < GM.enemyCount; i++)
        {
            //GameObject NewEnemy = Instantiate
            //    (
            //        GameManager.gameManager.NumberOfEnemies[i], 
            //        //Deal with enemy/enemies' position on awake.//
            //        Quaternion.identity

            //    ) as GameObject;

            //NewEnemy.name = NewEnemy.GetComponent<EnemyStateMachine>().enemy.theName + "_" + (i + 1);
            //NewEnemy.GetComponent<EnemyStateMachine>().enemy.theName = NewEnemy.name;

            ////Add the enemy or enemies to the list.//
            //EnemyCharacters.Add(NewEnemy);
        }
    }

    // Use this for initialization
    void Start()
    {
        Current_Battle_State = BattleState.WAITING;

        //MainCharacter.AddRange(GameObject.FindGameObjectsWithTag("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        switch (Current_Battle_State)
        {
            case (BattleState.WAITING):
                WaitingBoS();
                break;

            case (BattleState.TAKEACTION):
                TakeActionBoS();
                break;

            case (BattleState.PERFORMACTION):
                //Buffer Battle State, for the animation and all.//
                break;
        }
    }
    #endregion

    #region Battle State Methods
    private void WaitingBoS()
    {
        if (TurnList.Count > 0)
        {
            Current_Battle_State = BattleState.TAKEACTION;
        }
    }

    private void TakeActionBoS()
    {
        GameObject performer = GameObject.Find(TurnList[0].Attacker);

        //If it is the enemy's turn to play, check for enemy to fight and targets one if it doesn't have a target in hand.//
        if (TurnList[0].Type == "Enemy")
        {
            EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();

            //for (int i = 0; i < MainCharacter.Count; i++)
            //{

            //    if (TurnList[0].Target == MainCharacter[i]) //Already has a target ? => Change the State of ESM.//
            //    {
            //        ESM.targetPlayer = TurnList[0].Target;

            //        ESM.currentState = EnemyStateMachine.TurnState.ACTION;
            //        break;
            //    }

            //    else //Does not have a target ? => Find one and then change the State of ESM.//
            //    {
            //        TurnList[0].Target = MainCharacter[Random.Range(0, MainCharacter.Count)];
            //        ESM.targetPlayer = TurnList[0].Target;

            //        ESM.currentState = EnemyStateMachine.TurnState.ACTION;
            //        break;
            //    }
            //}
        }
        //If it is the Player's turn to play.//
        if (TurnList[0].Type == "Player")
        {

            PlayerStateMachine PSM = performer.GetComponent<PlayerStateMachine>();
            PSM.targetEnemy = TurnList[0].Target;
            PSM.State_Of_Battle = PlayerStateMachine.BattleState.ACTION;
        }

        Current_Battle_State = BattleState.PERFORMACTION;
    }
    #endregion

    public void List_Of_Turns(TurnHandler turns)
    {
        TurnList.Add(turns);
    }
}

