using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BattleStateMachine2 : MonoBehaviour
{
    //Constant List.//
    private const float ATTACK_TIME = 0.05f;
    public const float DISTANCE_ATTACKER = 2.0f;
    public const float ANIMATION_SPEED = 5.0F;

    //Variables List.//
    
    //Manages the time before the enemy attacks.//
    private float current_Cooldown = 0.0f;
    private float max_Cooldown = 5.0f;

    private bool ActionStarted = false;

    //Lists.//
    public List<TurnHandler> TurnList = new List<TurnHandler>();
    public List<GameObject> MainCharacter = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();
    public List<Transform> SpawnPoints = new List<Transform>();

    //Vectors.//
    private Vector3 StartPosition;

    //Enums.//
    public enum StateOfBattle
    {
        WAITING,
        PLAYER_TURN,
        ENEMY_PENDING,
        ENEMY_TURN,
        CHECK_ALIVE,
        WIN,
        LOSE
    }
    public StateOfBattle Battle_State;

    void Awake()//Adds the enemies' names to a list.//
    {
        for(int i = 0; i < GameManager.gameManager.enemyCount; i++)
        {
            GameObject newEnemy = Instantiate(GameManager.gameManager.NumberOfEnemies[i], SpawnPoints[i].position, Quaternion.identity) as GameObject;
            //spawn enemies and give them shit.//
            Enemies.Add(newEnemy); //Add the new enemies that were in the vicinity to the list for the fight.//
        }
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

            case (StateOfBattle.PLAYER_TURN):
                //Actions from player's.//
                break;

            case (StateOfBattle.ENEMY_PENDING):
                EnemyTakingAction(); //Wait for 'X' amount of seconds before switching state. => Increase/Decrease max_Cooldown.//
                break;

            case (StateOfBattle.ENEMY_TURN):
                //Actions from enemy's.//
                StartCoroutine(TimeForAction());
                break;

            case (StateOfBattle.CHECK_ALIVE):

                /**Needs to be checked at the end of either turn and change the battle state to what it once was. 
                   Basically an interruption to check if either party is still alive.**/

                CheckIfAlive();
                break;

            case (StateOfBattle.WIN):
                //Display "You've won", press ok and come back to main map.//
                break;

            case (StateOfBattle.LOSE):
                //Display "You died", press ok and come back to main map.//
                break;
        }
    }

    private void PlayerTurn()
    {

    }
    private void EnemyTurn()
    {

    }

    private void EnemyTakingAction() //Wait for 'X' amount of seconds before switching state. => Increase/Decrease max_Cooldown.//
    {
        current_Cooldown = current_Cooldown + Time.deltaTime;

        if (current_Cooldown >= max_Cooldown)
        {
            Battle_State = StateOfBattle.ENEMY_TURN;
        }
    }
    private void CheckTurnList() //Initial test to see if the fight is a new one, if new => Change state to Player_Turn.//
    {
        if (TurnList.Count > 0)
        {
            Battle_State = StateOfBattle.PLAYER_TURN;
        }
    }
    private void CheckIfAlive() //Check whoever has remaining people alive.//
    {
        if((MainCharacter.Count > 0) && (Enemies.Count <= 0))
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
    public void CollectActions(TurnHandler Turn) //Add attackers' attack to a list.//
    {
        TurnList.Add(Turn);
    }

    //Add small animation for the attacking opponent to move toward the enemy.//
    private IEnumerator TimeForAction()
    {
        if (ActionStarted){yield break;}

        ActionStarted = true;

        Vector3 MainCharactersPosition = new Vector3
            (
            //Move enemy towards the player.//
            //opponent.transform.position.x - DISTANCE_ATTACKER,
            //opponent.transform.position.y, 
            //opponent.transform.position.Z
            );

        while (MoveTowardsEnemy(MainCharactersPosition)){yield return null;}

        yield return new WaitForSeconds(ATTACK_TIME); //After moving towards the enemy, waits for 'X' seconds.//

        Vector3 firstPosition = StartPosition; //Moves the attacker back to its starting position.//
        while (MoveTowardsStart(firstPosition)){yield return null;}

        //Reset BattleStateMachine
        TurnList.RemoveAt(0);
        Battle_State = StateOfBattle.WAITING;
        ActionStarted = false;
        current_Cooldown = 0.0f;
    }
    private bool MoveTowardsEnemy(Vector3 target) //Move towards the opponent.//
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, ANIMATION_SPEED * Time.deltaTime));
    }
    private bool MoveTowardsStart(Vector3 target) //Move back to start.//
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, ANIMATION_SPEED * Time.deltaTime));
    }

}
