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

   
    ATTACK,
    DEFEND,
    CHOOSEACTION,
    DEAD,
    CHOOSETARGET,
    END_TURN,
    END_BATTLE,
    ENEMYMOVE,
    STARTBATTLE,
    RUN_BATTLE
    
}
public class BattleStateMachine : MonoBehaviour
{
    public const int TIME_NORMAL = 1;
    public const int PLAYER_REMAINING = 0;
    public const int HEALTH_REMAINING = 0;
    public const float LAST_TIMER = 100.0F;

    public EXPUi ui;
    public List<EnemyStateMachine> Enemies = new List<EnemyStateMachine>();
    public List<PlayerStateMachine> Players = new List<PlayerStateMachine>();
    private List<PlayerStateMachine> playersAlive = new List<PlayerStateMachine>();
    private List<EnemyStateMachine> enemiesAlive = new List<EnemyStateMachine>();
    private List<EnemyBaseClass> potentialEnemies = new List<EnemyBaseClass>()
    {
        EnemyBaseClass.Goblin(),
        EnemyBaseClass.Orc(),
        EnemyBaseClass.Elf(),
        
    };
    //Level Up System//
    public PlayerStateMachine psm;
    
    
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
    public GameObject battleSlots;
    public GameObject[] EnemySlots;
    private Image[] battleImage = new Image[3];
    private Text[] battleText = new Text[3];
    public Slider sliderHp;
    public Slider sliderMp;
    public InspectLogic inspLog;
    private RectTransform inspPos;
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

    string absolutePath;
    #endregion

    #region Awake, Start, Update
    void Awake()
    {
        ui.Disable();
        absolutePath = Application.dataPath;
        Current_Battle_State = BattleState.STARTBATTLE;
        commandsPanel.SetActive(true);
        targetPanel.SetActive(false);
        inspPos = inspLog.GetComponent<RectTransform>();
        
    }
    void Start()
    {
        EndScreen.SetActive(false);
        Time.timeScale = TIME_NORMAL;
        ui.Disable();

        battleImage = battleSlots.GetComponentsInChildren<Image>();
        battleText = battleSlots.GetComponentsInChildren<Text>();

        // selectPlayerSword.SetActive(false);
        Current_Battle_State = BattleState.STARTBATTLE;
        Players[0].PBS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBaseClass>(); //Uses the MC stats in the battle.//
    }
    bool onceExp = true;
    void Update()
    {
       
        // Debug.Log(enemiesAlive[0].EBS.currentHP);
        bool battleEnded = true;
        foreach (EnemyStateMachine e in enemiesAlive)
        {
            
            if (e.EBS != null && e.EBS.currentHP > HEALTH_REMAINING)
            {
                battleEnded = false;
                break;
            }

        }
        // print(Current_Battle_State);
        if (BattleCanvas.activeSelf)
        {
            if (playersAlive.Count > 0)
            {
                sliderHp.value = (int)(playersAlive[0].PBS.currentHP / playersAlive[0].PBS.baseHP * 100);
                sliderMp.value = (int)(playersAlive[0].PBS.currentMP / playersAlive[0].PBS.baseMP * 100);
                print($"current hp {playersAlive[0].PBS.currentHP} and {playersAlive[0].PBS.baseMP}");
            }

            switch (Current_Battle_State)
            {
                case BattleState.STARTBATTLE:
                    
                    ui.Disable();
                   
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
                    if (playersAlive[0].input != PlayerInput.NULL)
                    {
                        Current_Battle_State = BattleState.PERFORMACTION;

                        // selectPlayerSword.SetActive(false);
                    }
                    else
                    {
                        //TODO this is empty ?
                    }


                    break;
                case BattleState.PERFORMACTION:
                    print("Input is:  " + playersAlive[0].input);
                    switch (playersAlive[0].input)
                    {
                        case PlayerInput.ATTACK: Current_Battle_State = BattleState.CHOOSETARGET; break;
                        case PlayerInput.DEFEND:
                            playersAlive[0].Defend();
                            commandsPanel.SetActive(false);
                            Current_Battle_State = BattleState.END_TURN;
                            break;
                        case PlayerInput.INSPECT:
                            inspLog.returno.gameObject.SetActive(true);
                            
                           Current_Battle_State = BattleState.CHOOSETARGET; break;
                        case PlayerInput.USE_ITEM: break;
                        case PlayerInput.USE_SKILLS: break;
                        case PlayerInput.RUN:
                            commandsPanel.SetActive(false);
                            if (Random.Range(1, 100) > 60)
                            {
                                Current_Battle_State = BattleState.RUN_BATTLE; battleEnded = true; Exit = true;
                            }
                            else {
                                Current_Battle_State = BattleState.END_TURN;
                            }
                            break;

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
                            case PlayerInput.INSPECT:
                               
                                
                                inspLog.esm = enemiesAlive[selectedTarget];
                                inspLog.gameObject.SetActive(true);
                                
                                Vector3 enemyPos = EnemySlots[selectedTarget].transform.position;

                                inspPos.position = new Vector3(enemyPos.x+88,enemyPos.y,enemyPos.z);
                                break;
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
                        if (enemiesAlive[enemyTurn].EBS != null && enemiesAlive[enemyTurn].EBS.currentHP >= 1)
                        {
                            enemiesAlive[enemyTurn].DoMove(playersAlive[0].PBS);

                            anim.SetTrigger("Attack");
                            onlyOnce = true;
                        }
                        else {
                            enemyTurn++;
                            timer = 0;
                        }
                    }
                    timer += Time.deltaTime;
                    if (timer > 2.0f)
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
                            onceExp = true;
                        }
                        else {
                            if (onceExp) {
                                playersAlive[0].PBS.Exp += totalEXP;
                                onceExp = false;
                            }
                            ui.Enable(totalEXP);

                        }
                    }
                    else {
                        Current_Battle_State = BattleState.ORDERING;
                        playersAlive[0].isDefending = false;
                        playersAlive[0].input = PlayerInput.NULL;
                        selectedTarget = -1;
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
                case BattleState.RUN_BATTLE:
                  
                   
                        Current_Battle_State = BattleState.STARTBATTLE;

                        EscapeBattle();


                        OverWorldPlayer.enabled = true;
                        GameManager.inAFight = false;
                        Exit = false;
                        lastTimer = LAST_TIMER;
                    


                    break;
            }
            
           
            for (int i = 0; i < enemiesAlive.Count; i++)
            {
                if (enemiesAlive[i].EBS != null && enemiesAlive[i].EBS.currentHP>= 1)
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
            if (playersAlive[0].PBS.currentHP < 1) {
                Time.timeScale = 0;
                EndScreen.SetActive(true);

            }
          
        }
    }
    #endregion
  
    void MakeListOfOrders()
    {
        commandsPanel.SetActive(true);
        playersAlive.Clear();
        foreach (PlayerStateMachine p in Players)
        {
            p.StartUp();
            playersAlive.Add(p);
            psm = p;
            //print(p);
        }
        enemiesAlive.Clear();
        for (int i = 0; i < Enemies.Count; i++)
        {
            
            if (Enemies[i].EBS != null)
            {
                enemynames[i].text = Enemies[i].EBS.enemyName;
                battleText[i].text = Enemies[i].EBS.enemyName;
                battleImage[i].sprite = Resources.Load<Sprite>(Enemies[i].EBS.spritePath);
                print(enemynames[i].text);
                print("Loaded: " + Enemies[i].EBS.spritePath);
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
        targetPanel.SetActive(false);
       
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
        inspLog.returno.gameObject.SetActive(false);
        inspLog.gameObject.SetActive(false);
        selectedTarget = -1;
        Current_Battle_State = BattleState.PERFORMACTION;
        playersAlive[0].input = PlayerInput.NULL;
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
        AudioManager.CheckSound();

        EncounterLogic.ResetChance();
        Debug.Log("on va essayer de looter un objet");
        //ObjectControllerFactory.LootObjects();
        BattleCanvas.SetActive(false);
    }
    public void EscapeBattle()
    {
        AudioManager.CheckSound();

        EncounterLogic.ResetChance();
        BattleCanvas.SetActive(false);
    }
    public void StartBattle()
    {
        FindObjectOfType<AudioManager>().VariantPlay("OnBattle");

        OverWorldPlayer.enabled = false;
        totalEXP = 0;
        potentialEnemies.Clear();
        potentialEnemies = new List<EnemyBaseClass>() {
        EnemyBaseClass.Goblin() , EnemyBaseClass.Orc(),EnemyBaseClass.Elf(),
    }; 
        foreach (EnemyStateMachine e in Enemies)
        {
            e.EBS = null;
        }
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            Enemies[i].EBS = potentialEnemies[i];
            totalEXP += potentialEnemies[i].expGiven;
            print("Added: " + potentialEnemies[i].enemyName);

        }
        BattleCanvas.SetActive(true);
    }
    public void StartBossBattle(List<EnemyBaseClass> bosses)
    {
        FindObjectOfType<AudioManager>().VariantPlay("OnBattle");

        OverWorldPlayer.enabled = false;
        totalEXP = 0;
        potentialEnemies.Clear();
        potentialEnemies = bosses;
        foreach (EnemyStateMachine e in Enemies)
        {
            e.EBS = null;
        }
        for (int i = 0; i < Random.Range(1, 2); i++)
        {
            Enemies[i].EBS = potentialEnemies[i];
            totalEXP += potentialEnemies[i].expGiven;
            print("Added: " + potentialEnemies[i].enemyName);

        }
        BattleCanvas.SetActive(true);
    }
    public void GameOver()
    {
        SceneManager.LoadScene("gameplay");
    }



    #endregion
}
