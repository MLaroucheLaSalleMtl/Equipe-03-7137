using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
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
    public List<GameObject> MainCharacter = new List<GameObject>();
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
    private BattleStateMachine BSM;

    //Enums.//
    public enum StateOfBattle
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD

    }

    public StateOfBattle Battle_State;
    #endregion

    void Start()
    {
        Battle_State = StateOfBattle.PROCESSING;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
    }

    void Update()
    {

        switch (Battle_State)
        {
            case (StateOfBattle.PROCESSING):
                TurnTimer();
                break;

            case (StateOfBattle.ADDTOLIST):
                BSM.MCManagement.Add(this.gameObject);
                Battle_State = StateOfBattle.WAITING;
                break;

            case (StateOfBattle.WAITING):
                //idle
                break;

            case (StateOfBattle.ACTION):
                TimeForAction();
                Battle_State = StateOfBattle.WAITING;
                break;

            case (StateOfBattle.DEAD):
                break;
        }

    }




    void TurnTimer()
    {
        current_Cooldown = current_Cooldown + Time.deltaTime;

        if (current_Cooldown >= max_Cooldown)
        {
            Battle_State = StateOfBattle.ACTION;
        }
    }

    //Animation - Region.///
    #region
    //Add small animation for the attacking opponent to move toward the enemy.//
    private IEnumerator TimeForAction()
    {
        if (ActionStarted) { yield break; }

        ActionStarted = true;

        Vector3 MainCharactersPosition = new Vector3
            (
            //Move enemy towards the player.//
            Hero.transform.position.x - DISTANCE_ATTACKER,
            Hero.transform.position.y,
            Hero.transform.position.z
            );

        while (MoveTowardsEnemy(MainCharactersPosition)) { yield return null; }
        
        //DoDamage here.//

        yield return new WaitForSeconds(ATTACK_TIME); //After moving towards the enemy, waits for 'X' seconds.//

        Vector3 firstPosition = StartPosition; //Moves the attacker back to its starting position.//
        while (MoveTowardsStart(firstPosition)) { yield return null; }

        BSM_Reset(); //Reset.//
    }
    private bool MoveTowardsEnemy(Vector3 target) //Move towards the opponent.//
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, ANIMATION_SPEED * Time.deltaTime));
    }
    private bool MoveTowardsStart(Vector3 target) //Move back to start.//
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, ANIMATION_SPEED * Time.deltaTime));
    }
    #endregion

    //Resets - Region./
    #region
    private void BSM_Reset()
    {
        //Reset BattleStateMachine
        Battle_State = StateOfBattle.WAITING;
        ActionStarted = false;
        current_Cooldown = 0.0f;
    }
    #endregion

}
