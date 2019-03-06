using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{
    #region Variable, Classes and etc.

    //Lists.//
    public List<TurnHandler> TurnList = new List<TurnHandler>();


    //public List<EnemyStateMachine> EnemiesList = new List<EnemyStateMachine>();
    //public List<EnemyStateMachine> PlayerList = new List<EnemyStateMachine>();

    public List<EnemyStateMachine> Enemies = new List<EnemyStateMachine>();
    public List<PlayerStateMachine> MainCharacter = new List<PlayerStateMachine>();

    //Script Access.//
    private TurnHandler playerChoice;
    //private GameManager GM;

    //Game Objects.//
    public GameObject targetPanel;
    public GameObject commandsPanel;

    

    //Buttons.//
    public Button attackButton;

    //Transforms.//
    private Transform Spacer;

    public enum BattleState
    {
        WAITING,
        TAKEACTION,
        PERFORMACTION,
    }
    public BattleState Current_Battle_State;

    public enum PlayerState
    {
        WAITING,
        CHOOSEACTIONS,
        TARGETENEMY,
        TURNDONE
    }
    public PlayerState currentPlayerState;

    #endregion

    #region Awake, Start, Update
    void Awake()
    {
        commandsPanel.SetActive(true);
        targetPanel.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        Current_Battle_State = BattleState.WAITING;
        MainCharacter[0].PBS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBaseClass>(); //Uses the MC stats in the battle.//
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

        switch(currentPlayerState)
        {
            case (PlayerState.CHOOSEACTIONS):
                Debug.Log("choose to fuck off");
                PlayerIsChoosing();
                break;

            case (PlayerState.WAITING):
                
                //Buffer state;
                break;

            case (PlayerState.TURNDONE):
                Debug.Log("done to fuck off");
                PlayerIsDone();
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
            Debug.Log("Enemy turn.");
            EnemyStateMachine ESM = Enemies.Find(x => x.EBS.enemyName == TurnList[0].Attacker); ;


            for (int j = 0; j < MainCharacter.Count; j++)
            {
                if (TurnList[0].Target == null) //Already has a target ? => Change the State of ESM.//
                {
                    ESM.targetPlayer = MainCharacter[0];

                    ESM.Current_Battle_State = EnemyStateMachine.BattleState.ACTION;
                }

                else //Does not have a target ? => Find one and then change the State of ESM.//
                {
                    TurnList[0].Target = MainCharacter[0].PBS.playerName;
                    ESM.targetPlayer = MainCharacter[0];

                    ESM.Current_Battle_State = EnemyStateMachine.BattleState.ACTION;
                    break; //Prevent the loop to repeat itself one more time, its job has been done.//
                }
            }
            
        }
        //If it is the Player's turn to play.//
        if (TurnList[0].Type == "Player")
        {
            Debug.Log("Player's turn");
            PlayerStateMachine PSM = performer.GetComponent<PlayerStateMachine>();
            PSM.targetEnemy = Enemies.Find(x=> x.EBS.enemyName == TurnList[0].Target);
            PSM.State_Of_Battle = PlayerStateMachine.BattleState.ACTION;
        }

        Current_Battle_State = BattleState.PERFORMACTION;
    }
    #endregion

    #region Player State Methods
    private void Commands()
    {
        commandsPanel.SetActive(true);
    }

    public void input1()
    {
        Debug.Log("input 1 clicked.");
    
        commandsPanel.SetActive(false);
        targetPanel.SetActive(true);
    }
    public void input2(string enemyIsTarget)
    {
        Debug.Log(enemyIsTarget);
        currentPlayerState = PlayerState.TURNDONE;
    }

    public void EnemyA_Button()
    {
        Debug.Log("Enemy A Selected");
        input2(Enemies[0].EBS.enemyName);
    }
    public void EnemyB_Button()
    {
        Debug.Log("Enemy B Selected");
        input2(Enemies[1].EBS.enemyName);
    }

    public void EnemyC_Button()
    {
        Debug.Log("Enemy C Selected");
        input2(Enemies[2].EBS.enemyName);
    }

    private void PlayerIsChoosing()
    {
        if (MainCharacter.Count > 0)
        {
            playerChoice = new TurnHandler();
            currentPlayerState = PlayerState.WAITING;
        }
    }

    private void PlayerIsDone()
    {
        currentPlayerState = PlayerState.CHOOSEACTIONS;
    }



    #endregion

    #region Other
    public void List_Of_Turns(TurnHandler turns)
    {
        TurnList.Add(turns);
    }
    #endregion

}

