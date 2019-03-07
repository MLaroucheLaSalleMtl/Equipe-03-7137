using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum BattleState
{
    ORDERING,
    WAITINGFORINPUT,
    TAKEACTION,
    PERFORMACTION,

    ADDTOLIST,
    BUFFER,
    ATTACK,
    DEFEND,
    CHOOSEACTION,
    DEAD,
    CHOOSETARGET,
    END_TURN,
    END_BATTLE,
    ENEMYMOVE,
    STARTBATTLE,
}
public class BattleStateMachine : MonoBehaviour
{
    #region Variable, Classes and etc.

    //Lists.//
    public List<TurnHandler> TurnList = new List<TurnHandler>();


    //public List<EnemyStateMachine> EnemiesList = new List<EnemyStateMachine>();
    //public List<EnemyStateMachine> PlayerList = new List<EnemyStateMachine>();

    public List<EnemyStateMachine> Enemies = new List<EnemyStateMachine>();
    public List<PlayerStateMachine> Players = new List<PlayerStateMachine>();
    private List<PlayerStateMachine> playersAlive = new List<PlayerStateMachine>();
    private List<EnemyStateMachine> enemiesAlive = new List<EnemyStateMachine>();
    private List<EnemyBaseClass> potentialEnemies = new List<EnemyBaseClass>() {
        EnemyBaseClass.Goblin() , EnemyBaseClass.Orc(),EnemyBaseClass.Elf()
    };
    //Script Access.//
    private TurnHandler playerChoice;
    //private GameManager GM;

    //Game Objects.//
    public GameObject targetPanel;
    public GameObject commandsPanel;
    // public GameObject selectPlayerSword;
    public GameObject BattleCanvas;
    public PlayerBehaviour OverWorldPlayer;

    //Buttons.//
    public Button attackButton;

    public GameObject[] buttonsTargets;
    //Transforms.//
    private Transform Spacer;


    public BattleState Current_Battle_State;
    public Text[] enemynames;
    public PlayerState currentPlayerState;
    private int selectedTarget = -1;
    #endregion

    #region Awake, Start, Update
    void OnEnable()
    {
        potentialEnemies.Clear();
        potentialEnemies = new List<EnemyBaseClass>() {
        EnemyBaseClass.Goblin() , EnemyBaseClass.Orc(),EnemyBaseClass.Elf()
    };
        foreach (EnemyStateMachine e in Enemies)
        {
            e.EBS = null;
        }
        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            Enemies[i].EBS = potentialEnemies[i];

        }
        //  BattleCanvas.SetActive(true);
        MakeListOfOrders();
        Current_Battle_State = BattleState.STARTBATTLE;
    }
    void Awake()
    {
        Current_Battle_State = BattleState.STARTBATTLE;
        commandsPanel.SetActive(true);
        targetPanel.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        potentialEnemies.Clear();
        potentialEnemies = new List<EnemyBaseClass>() {
        EnemyBaseClass.Goblin() , EnemyBaseClass.Orc(),EnemyBaseClass.Elf()
    };
        foreach (EnemyStateMachine e in Enemies)
        {
            e.EBS = null;
        }
        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            Enemies[i].EBS = potentialEnemies[i];

        }


        // selectPlayerSword.SetActive(false);
        Current_Battle_State = BattleState.STARTBATTLE;
        Players[0].PBS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBaseClass>(); //Uses the MC stats in the battle.//
    }
    float time = 1f;
    int enemyTurn = 0;
    public static bool enemyTurnDone = false;
    bool onlyOnce = false;
    float timer = 0;
    // Update is called once per frame
    bool Exit = false;
    void Update()
    {
        bool battleEnded = true;
        foreach (EnemyStateMachine e in enemiesAlive)
        {
            
            if (e.EBS != null && e.EBS.currentHP > 0)
            {
                battleEnded = false;
                break;
            }

        }
        // print(Current_Battle_State);
        if (BattleCanvas.activeSelf)
        {
            switch (Current_Battle_State)
            {
                case BattleState.STARTBATTLE:
                    potentialEnemies.Clear();
                    potentialEnemies = new List<EnemyBaseClass>() {
        EnemyBaseClass.Goblin() , EnemyBaseClass.Orc(),EnemyBaseClass.Elf()
    };
                    foreach (EnemyStateMachine e in Enemies)
                    {
                        e.EBS = null;
                    }
                    for (int i = 0; i < Random.Range(1, 3); i++)
                    {
                        Enemies[i].EBS = potentialEnemies[i];
                        print("Added: " +potentialEnemies[i].enemyName);

                    }
                    MakeListOfOrders();

                    Current_Battle_State = BattleState.ORDERING;
                    break;
                case BattleState.ORDERING:
                    Input.ResetInputAxes();
                    selectedTarget = -1;
                    EventSystem.current.SetSelectedGameObject(null);
                    commandsPanel.SetActive(true);
                    Current_Battle_State = BattleState.WAITINGFORINPUT;
                    break;
                case BattleState.WAITINGFORINPUT:
                    // print(playersAlive[0].input);
                    //  selectPlayerSword.SetActive(true);
                    if (playersAlive[0].input != PlayerInput.NULL)
                    {
                        Current_Battle_State = BattleState.PERFORMACTION;

                        // selectPlayerSword.SetActive(false);
                    }
                    else
                    {

                    }


                    break;
                case BattleState.PERFORMACTION:
                    switch (playersAlive[0].input)
                    {
                        case PlayerInput.ATTACK: Current_Battle_State = BattleState.CHOOSETARGET; break;
                        case PlayerInput.DEFEND:
                            playersAlive[0].Defend();
                            Current_Battle_State = BattleState.END_TURN;
                            break;
                        case PlayerInput.INSPECT: Current_Battle_State = BattleState.CHOOSETARGET; break;
                        case PlayerInput.USE_ITEM: break;
                        case PlayerInput.USE_SKILLS: break;
                        case PlayerInput.RUN: Current_Battle_State = BattleState.END_BATTLE; battleEnded = true; Exit = true; break;

                    }
                    break;
                case BattleState.CHOOSETARGET:
                    if (selectedTarget > -1)
                    {
                        switch (playersAlive[0].input)
                        {
                            case PlayerInput.ATTACK:
                                EnemyStateMachine targetEnemy = enemiesAlive[selectedTarget];
                                // playersAlive[0].StartCoroutine(Players[0].actionTimer());
                                Image enem = targetEnemy.gameObject.GetComponent<Image>();
                                Color temp = new Color(255, 255, 255, 255);
                                Color red = new Color(1, 0, 0, 1);
                                time = time - Time.deltaTime;
                                if (time > 0)
                                {


                                    enem.color = red;
                                }
                                else
                                {
                                    playersAlive[0].Attack(targetEnemy);
                                    time = 1;
                                    enem.color = temp;
                                    Current_Battle_State = BattleState.END_TURN;
                                    print("ping");
                                }
                                break;
                            case PlayerInput.INSPECT: break;
                        }
                    }
                    break;

                case BattleState.END_TURN:
                    targetPanel.SetActive(false);

                    foreach (PlayerStateMachine p in playersAlive)
                    {
                        p.EndTurn();

                    }
                    Current_Battle_State = BattleState.ENEMYMOVE;
                    break;

                case BattleState.ENEMYMOVE:
                    Animator anim = enemiesAlive[enemyTurn].GetComponent<Animator>();
                    if (!onlyOnce)
                    {
                        if (enemiesAlive[enemyTurn].EBS != null && enemiesAlive[enemyTurn].EBS.currentHP > 0)
                        {
                            enemiesAlive[enemyTurn].Attack(playersAlive[0]);

                            anim.SetTrigger("Attack");
                            onlyOnce = true;
                        }
                        else {
                            enemyTurn++;
                            timer = 0;
                        }
                    }
                    timer += Time.deltaTime;
                    if (timer > 2f)
                    {
                        onlyOnce = false;
                        enemyTurn++;
                        timer = 0;
                    }
                    if (enemyTurn == enemiesAlive.Count)
                    {
                        enemyTurn = 0;
                        Current_Battle_State = BattleState.END_BATTLE;
                        //enemyTurnDone = false;
                    }
                    break;
                case BattleState.END_BATTLE:
                    print(battleEnded);

                    if (battleEnded||Exit)
                    {
                            Current_Battle_State = BattleState.STARTBATTLE;

                            EndBattle();

                        
                        OverWorldPlayer.enabled = true;
                        GameManager.inAFight = false;
                        Exit = false;
                    }
                    else {
                        Current_Battle_State = BattleState.ORDERING;
                    }
                    break;

            }
            
           
            for (int i = 0; i < enemiesAlive.Count; i++)
            {
                if (enemiesAlive[i].EBS != null && enemiesAlive[i].EBS.currentHP>0)
                {
                    Enemies[i].gameObject.SetActive(true);
                    buttonsTargets[i].SetActive(true);


                }
                else
                {
                    Enemies[i].gameObject.SetActive(false);
                    buttonsTargets[i].SetActive(false);
                    print("Removed " + enemiesAlive[i].EBS);

                   
                }
            }
            //print(playersAlive[0].input);


            //switch (Current_Battle_State)
            //{
            //    case (BattleState.WAITINGFORINPUT):
            //        WaitingBoS();
            //        break;

            //    case (BattleState.TAKEACTION):
            //        TakeActionBoS();
            //        break;

            //    case (BattleState.PERFORMACTION):
            //        //Buffer Battle State, for the animation and all.//
            //        break;
            //}

            //switch (currentPlayerState)
            //{
            //    case (PlayerState.CHOOSEACTIONS):
            //        Debug.Log("choose to fuck off");
            //        PlayerIsChoosing();
            //        break;

            //    case (PlayerState.WAITING):

            //        //Buffer state;
            //        break;

            //    case (PlayerState.TURNDONE):
            //        Debug.Log("done to fuck off");
            //        PlayerIsDone();
            //        break;
            //}
        }
    }
    #endregion

    #region Battle State Methods

    void MakeListOfOrders()
    {
        commandsPanel.SetActive(true);
        playersAlive.Clear();
        foreach (PlayerStateMachine p in Players)
        {
            p.StartUp();
            playersAlive.Add(p);
            //print(p);

        }
        enemiesAlive.Clear();
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].EBS != null)
            {
                enemynames[i].text = Enemies[i].EBS.enemyName;
                print(enemynames[i].text);
            }
            else
            {
                Enemies[i].gameObject.SetActive(false);
            }
            enemiesAlive.Add(Enemies[i]);
            enemiesAlive[i].StartBattle();
        }


    }




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


            for (int j = 0; j < Players.Count; j++)
            {
                if (TurnList[0].Target == null) //Already has a target ? => Change the State of ESM.//
                {
                    ESM.targetPlayer = Players[0];

                    ESM.Current_Battle_State = BattleState.ATTACK;
                }

                else //Does not have a target ? => Find one and then change the State of ESM.//
                {
                    TurnList[0].Target = Players[0].PBS.playerName;
                    ESM.targetPlayer = Players[0];

                    ESM.Current_Battle_State = BattleState.ATTACK;
                    break; //Prevent the loop to repeat itself one more time, its job has been done.//
                }
            }

        }
        //If it is the Player's turn to play.//
        if (TurnList[0].Type == "Player")
        {
            Debug.Log("Player's turn");
            PlayerStateMachine PSM = performer.GetComponent<PlayerStateMachine>();
            PSM.targetEnemy = Enemies.Find(x => x.EBS.enemyName == TurnList[0].Target);
            PSM.State_Of_Battle = PlayerState.ATTACK;
        }

        Current_Battle_State = BattleState.PERFORMACTION;
    }
    #endregion

    #region Player State Methods
    private void Commands()
    {
        commandsPanel.SetActive(true);
    }

    public void SwitchToTargeting()
    {
        commandsPanel.SetActive(false);
        targetPanel.SetActive(true);
    }

    public void SwitchToCommands()
    {
        commandsPanel.SetActive(true);
        targetPanel.SetActive(false);
    }


    public void SelectTarget(int value)
    {
        selectedTarget = value;
        print("Enemy " + value + " Selected");
    }

    public void UserInput(int input)
    {
        
            playersAlive[0].input = (PlayerInput)input;
            print("Ping: " + input);
        
    }




    public void EndBattle()
    {
        BattleCanvas.SetActive(false);

    }
    public void EscapeBattle()
    {
        BattleCanvas.SetActive(false);

    }
    public void StartBattle()
    {
        OverWorldPlayer.enabled = false;

        foreach (EnemyStateMachine e in Enemies)
        {
            e.EBS = null;
        }
        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            Enemies[i].EBS = potentialEnemies[i];

        }
        BattleCanvas.SetActive(true);

    }
    #endregion

    #region Other
    public void List_Of_Turns(TurnHandler turns)
    {
        TurnList.Add(turns);
    }
    #endregion

}

