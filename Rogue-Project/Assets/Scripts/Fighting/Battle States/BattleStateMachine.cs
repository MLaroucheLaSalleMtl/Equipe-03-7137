using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#region Variables
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
    public EXPUi ui;
    public List<EnemyStateMachine> Enemies = new List<EnemyStateMachine>();
    public List<PlayerStateMachine> Players = new List<PlayerStateMachine>();
    private List<PlayerStateMachine> playersAlive = new List<PlayerStateMachine>();
    private List<EnemyStateMachine> enemiesAlive = new List<EnemyStateMachine>();
    private List<EnemyBaseClass> potentialEnemies = new List<EnemyBaseClass>()
    {
        EnemyBaseClass.Goblin() , EnemyBaseClass.Orc(),EnemyBaseClass.Elf()
    };

    //Script Access.//
    private TurnHandler playerChoice;
    //private GameManager GM;

    //Game Objects.//
    public GameObject targetPanel;
    public GameObject commandsPanel;
    //public GameObject selectPlayerSword;
    public GameObject BattleCanvas;
    public PlayerBehaviour OverWorldPlayer;
    public GameObject EndScreen;

    //Buttons.//
    public Button attackButton;

    public GameObject[] buttonsTargets;

    //Transforms.//
    private Transform Spacer;

    //Text.//
    public Text[] enemynames;

    //Variable.//
    public static bool enemyTurnDone = false;
    bool onlyOnce = false;
    bool Exit = false;

    int enemyTurn = 0;
    int totalEXP;
    private int selectedTarget = -1;

    float lastTimer = 100f;
    float time = 1f;
    float timer = 0;

    //Enum.//
    public BattleState Current_Battle_State;
    public PlayerState currentPlayerState;

    #endregion

    #region Awake, Start, Update
    void Awake()
    {
        ui.Disable();

        Current_Battle_State = BattleState.STARTBATTLE;
        commandsPanel.SetActive(true);
        targetPanel.SetActive(false);
    }
    void Start()
    {
        EndScreen.SetActive(false);

        ui.Disable();

        potentialEnemies.Clear();
        potentialEnemies = new List<EnemyBaseClass>() {
        EnemyBaseClass.Goblin() , EnemyBaseClass.Orc(),EnemyBaseClass.Elf()
    };
        totalEXP = 0;
        foreach (EnemyStateMachine e in Enemies)
        {
            e.EBS = null;
        }
        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            Enemies[i].EBS = potentialEnemies[i];
            totalEXP += 30;
        }


        // selectPlayerSword.SetActive(false);
        Current_Battle_State = BattleState.STARTBATTLE;
        Players[0].PBS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBaseClass>(); //Uses the MC stats in the battle.//
    }
    void Update()
    {
        Debug.Log(enemiesAlive[0].EBS.currentHP);
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
                    
                    ui.Disable();

                    potentialEnemies.Clear();
                    potentialEnemies = new List<EnemyBaseClass>() {
        EnemyBaseClass.Goblin() , EnemyBaseClass.Orc(),EnemyBaseClass.Elf()
    };totalEXP = 0;
                    foreach (EnemyStateMachine e in Enemies)
                    {
                        e.EBS = null;
                    }
                    for (int i = 0; i < Random.Range(1, 3); i++)
                    {
                        Enemies[i].EBS = potentialEnemies[i];
                        totalEXP += 30;
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
                                    //playersAlive[0].Attack(targetEnemy); // what is this wtf
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
                            enemiesAlive[enemyTurn].DoMove(playersAlive[0]);

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
                   

                    if (battleEnded)
                    {
                        lastTimer -= Time.timeScale;
                        if (lastTimer < 0)
                        {
                            Current_Battle_State = BattleState.STARTBATTLE;

                            EndBattle();


                            OverWorldPlayer.enabled = true;
                            GameManager.inAFight = false;
                            
                            lastTimer = 100f;
                        }
                        else {
                            ui.Enable(totalEXP);

                        }
                    }
                    else {
                        Current_Battle_State = BattleState.ORDERING;
                    }
                    if (Exit) {
                        Current_Battle_State = BattleState.STARTBATTLE;

                        EndBattle();


                        OverWorldPlayer.enabled = true;
                        GameManager.inAFight = false;
                        Exit = false;
                        lastTimer = 100f;
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
                    //print("Removed " + enemiesAlive[i].EBS);

                   
                }
            }
            if (playersAlive[0].PBS.currentHP <=0) {
                Time.timeScale = 0;
                EndScreen.SetActive(true);

            }
          
        }
    }
    #endregion
    void OnEnable()
    {
        ui.Disable();
        potentialEnemies.Clear();
        potentialEnemies = new List<EnemyBaseClass>() {
        EnemyBaseClass.Goblin() , EnemyBaseClass.Orc(),EnemyBaseClass.Elf()
    };

        totalEXP = 0;

        foreach (EnemyStateMachine e in Enemies)
        {
            e.EBS = null;
        }
        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            Enemies[i].EBS = potentialEnemies[i];
            totalEXP += 30;
        }
        //  BattleCanvas.SetActive(true);
        MakeListOfOrders();
        Current_Battle_State = BattleState.STARTBATTLE;
    }
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
    #region Switch and Commands
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
    #endregion
    #region User Input
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
    #endregion
    #region Game Over, End, Escape and Start Battle
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
        totalEXP = 0;
        foreach (EnemyStateMachine e in Enemies)
        {
            e.EBS = null;
        }
        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            Enemies[i].EBS = potentialEnemies[i];
            totalEXP = 30;
        }
        BattleCanvas.SetActive(true);
    }
    public void GameOver()
    {
        SceneManager.LoadScene("gameplay");
    }

    #endregion
}
