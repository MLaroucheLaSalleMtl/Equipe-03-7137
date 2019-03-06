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
    public List<GameObject> TargetBtns = new List<GameObject>();

    //Script Access.//
    private TurnHandler playerChoice;
    //private GameManager GM;

    //Game Objects.//
    public GameObject targetButtons;
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
        for (int i = 0; i < GameManager.gameManager.enemyCount; i++)
        {
            GameObject NewEnemy = Instantiate
                (
                    GameManager.gameManager.NumberOfEnemies[i],
                    Vector2.zero,
                    Quaternion.identity

                ) as GameObject;

            NewEnemy.name = NewEnemy.GetComponent<EnemyStateMachine>().EBS.enemyName + "_" + (i + 1);
            NewEnemy.GetComponent<EnemyStateMachine>().EBS.enemyName = NewEnemy.name;

            //Add the enemy or enemies to the list.//
            Enemies.Add(NewEnemy);
        }
    }

    // Use this for initialization
    void Start()
    {
        Current_Battle_State = BattleState.WAITING;
        TargetButtons();
        MainCharacter.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        commandsPanel.SetActive(true);

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
                PlayerIsChoosing();
                break;

            case (PlayerState.WAITING):
                //Buffer state;
                break;

            case (PlayerState.TURNDONE):
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
            EnemyStateMachine ESM = GameObject.Find(TurnList[0].Attacker).GetComponent<EnemyStateMachine>();


            for (int j = 0; j < MainCharacter.Count; j++)
            {

                if (TurnList[0].Target == MainCharacter[j]) //Already has a target ? => Change the State of ESM.//
                {
                    ESM.targetPlayer = TurnList[0].Target;

                    ESM.Current_Battle_State = EnemyStateMachine.BattleState.ACTION;
                }

                else //Does not have a target ? => Find one and then change the State of ESM.//
                {
                    TurnList[0].Target = MainCharacter[Random.Range(0, MainCharacter.Count)];
                    ESM.targetPlayer = TurnList[0].Target;

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
            PSM.targetEnemy = TurnList[0].Target;
            PSM.State_Of_Battle = PlayerStateMachine.BattleState.ACTION;
        }

        Current_Battle_State = BattleState.PERFORMACTION;
    }
    #endregion

    #region Player State Methods


    private void Commands()
    {
        commandsPanel.SetActive(true);
        attackButton.onClick.AddListener(() => input1());
        
    }

    private void input1()
    {
        Debug.Log("input 1 clicked.");
        playerChoice.Attacker = PlayerManagement[0].name;
        playerChoice.AttackersGameObject = PlayerManagement[0];
        playerChoice.Type = "Player";

        commandsPanel.SetActive(false);
        targetPanel.SetActive(true);
    }
    public void input2(GameObject enemyIsTarget)
    {
        playerChoice.Target = enemyIsTarget;
        currentPlayerState = PlayerState.TURNDONE;
    }


    private void PlayerIsChoosing()
    {
        if (PlayerManagement.Count > 0)
        {
            PlayerManagement[0].transform.Find("selector").gameObject.SetActive(true);
            playerChoice = new TurnHandler();
            currentPlayerState = PlayerState.WAITING;
        }
    }

    private void PlayerIsDone()
    {
        TurnList.Add(playerChoice);
        PlayerManagement[0].transform.Find("Selector").gameObject.SetActive(false);
        PlayerManagement.RemoveAt(0);
        currentPlayerState = PlayerState.CHOOSEACTIONS;
    }



    #endregion

    #region Other
    public void List_Of_Turns(TurnHandler turns)
    {
        TurnList.Add(turns);
    }

    public void TargetButtons()
    {
        foreach(GameObject targetButton in TargetBtns)
        {
            Destroy(targetButton);
        }
        
        foreach(GameObject enemy in Enemies)
        {
            GameObject newButton = Instantiate(targetButtons) as GameObject;
            EnemyTarget ETButton = newButton.GetComponent<EnemyTarget>();

            EnemyStateMachine currentEnemy = enemy.GetComponent<EnemyStateMachine>();
            Text buttonText = newButton.transform.Find("EnemyName").gameObject.GetComponent<Text>();

            buttonText.text = currentEnemy.EBS.enemyName;
            ETButton.enemyGameObject = enemy;

            newButton.transform.SetParent(Spacer, false);   
            TargetBtns.Add(newButton);
        }
    }



    #endregion

}

