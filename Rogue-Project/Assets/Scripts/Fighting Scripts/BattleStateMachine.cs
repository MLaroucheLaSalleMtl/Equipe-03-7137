using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BattleStateMachine : MonoBehaviour
{
    //Constants - Variables - Lists - Etc.//
    #region
    //Constant List.//
    private const float ATTACK_TIME = 0.05f; 
    public const float DISTANCE_ATTACKER = 2.0f;    //Distance between the enemy and the player during the attack.//
    public const float ANIMATION_SPEED = 5.0F;      //Speed of the 'going to' theenemy.//
    private const float DEFENDING_DEMULTIPLIER = 0.25F;     //Multiplier if the player is defending.//

    //Variables List.//
        //Manages the time before the enemy attacks.//
    private float current_Cooldown = 0.0f;
    private float max_Cooldown = 5.0f;

    //Bool.//
    private bool ActionStarted = false;
    private bool isDefending = false;

    //Lists.//
    public List<TurnHandler> TurnList = new List<TurnHandler>();
    public List<GameObject> MainCharacter = new List<GameObject>();
    public List<GameObject> MCManagement = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();
    public List<Transform> SpawnPoints = new List<Transform>();

    //Vectors.//
    private Vector3 StartPosition;

    //Game Objects.//
    public GameObject Hero;
    public GameObject TargettedEnemy;
    
    //Script Access.//
    private BasicAttack bAttacks;
    private DummyBaseClass DBC;
    private GameManager GM;
    //Enums.//
    public enum StateOfBattle
    {
        WAITING,
        TAKEACTION,
        PERFORMACTION,
        CHECKALIVE,
        WIN,
        LOSE
    }

    public StateOfBattle Battle_State;
    #endregion

    void Awake()//Adds the enemies' names to a list.//
    {
        for (int i = 0; i < GameManager.gameManager.enemyCount; i++)
        {
            GameObject NewEnemy = Instantiate(GameManager.gameManager.NumberOfEnemies[i], , Quaternion.identity) as GameObject;
            NewEnemy.name = NewEnemy.GetComponent<EnemyStateMachine>().Enemies. + "_" + (i + 1);
            NewEnemy.GetComponent<EnemyStateMachine>().TargettedEnemy.name = NewEnemy.name;
            Enemies.Add(NewEnemy);
        }
    }

    void Start()
    {
        Battle_State = StateOfBattle.WAITING;
        MainCharacter.AddRange(GameObject.FindGameObjectsWithTag("Player"));
    }

    void Update()
    {
        B_State();
    }

    private void B_State() //Manages the different states of the ongoing battle.//
    {
        switch (Battle_State)
        {
            case (StateOfBattle.WAITING):
                CheckTurnList(); //=> Redirects to PLAYER_TURN if TurnList < 0;
                break;

            case (StateOfBattle.TAKEACTION):
                TakeAction();
                break;  

            case (StateOfBattle.WIN):
                //Display "You've won", press ok and come back to main map.//
                break;

            case (StateOfBattle.LOSE):
                //Display "You died", press ok and come back to main map.//

                break;
        }
    }

   public void CollectActions(TurnHandler Turn) //Add attackers' attack to a list.//
    {
        TurnList.Add(Turn);
    }

    void TakeAction()
    {
        GameObject performer = GameObject.Find(TurnList[0].attackersName);
        //handles enemies
        if (TurnList[0].Type == "Enemy")
        {
            EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
            for (int i = 0; i < MainCharacter.Count; i++)
            {
                if (TurnList[0].AttackersTarget == MainCharacter[i])
                {
                    ESM.TargettedEnemy = TurnList[0].AttackersTarget;
                    ESM.Battle_State = EnemyStateMachine.StateOfBattle.ACTION;
                    break;
                }

                else
                {
                    TurnList[0].AttackersTarget = MainCharacter[Random.Range(0, MainCharacter.Count)];
                    ESM.TargettedEnemy = TurnList[0].AttackersTarget;
                    ESM.Battle_State = EnemyStateMachine.StateOfBattle.ACTION;
                }
            }
        }
        //handles heroes
        if (TurnList[0].Type == "Player")
        {
            PlayerStateMachine PSM = performer.GetComponent<PlayerStateMachine>();
            PSM.TargettedEnemy = TurnList[0].AttackersTarget;
            PSM.Battle_State = PlayerStateMachine.StateOfBattle.ACTION;
        }

        Battle_State = StateOfBattle.PERFORMACTION;
    }

    //Check ups - Region.//
    #region
    private void CheckTurnList() //Initial test to see if the fight is a new one, if new => Change state to Player_Turn.//
    {
        if (TurnList.Count > 0)
        {
            Battle_State = StateOfBattle.WAITING;
        }
    }
    private void CheckIfAlive() //Check whoever has remaining people alive.//
    {
        if ((MainCharacter.Count > 0) && (Enemies.Count <= 0))
        {
            Battle_State = StateOfBattle.WIN;
        }

        if ((MainCharacter.Count <= 0) && (Enemies.Count > 0))
        {
            Battle_State = StateOfBattle.LOSE;
        }
        if ((MainCharacter.Count >= 0) && (Enemies.Count > 0))
        {
            //Change battle state to what it was.//
        }

    }
    #endregion
}
