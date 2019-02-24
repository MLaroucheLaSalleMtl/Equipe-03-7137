using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine battleStateMachine;

    public EnemyBaseClass enemyBaseClass;
<<<<<<< HEAD
    public PlayerBaseClass playerBaseClass;
=======
   
>>>>>>> Alonso

    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }
    public TurnState currentState;

    private float current_Cooldown = 0.0f;
    private float max_Cooldown = 5.0f;

    private Vector3 StartPosition;
    private bool ActionStarted = false;
    public Image ProgressBar;
    public GameObject HeroAttack;
    private float animSpeed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        currentState = TurnState.PROCESSING;
        battleStateMachine = GameObject.Find("Battle Manager").GetComponent<BattleStateMachine>();
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switchState();
    }

    void UpgradeProgressBar()
    {
        current_Cooldown = current_Cooldown + Time.deltaTime;

        if (current_Cooldown >= max_Cooldown)
        {
            currentState = TurnState.CHOOSEACTION;
        }
    }

    void switchState()
    {
        switch (currentState)
        { 
            case (TurnState.PROCESSING):
                UpgradeProgressBar();
                break;

            case (TurnState.WAITING):
                break;

            case (TurnState.CHOOSEACTION):
                TakeAction();
                currentState = TurnState.WAITING;
                break;

            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;
                
            case (TurnState.DEAD):
                break;
        }
    }

    void TakeAction()
    {
        TurnHandler attack = new TurnHandler();

<<<<<<< HEAD
        attack.attackersName = enemyBaseClass.playerName;
=======
        attack.attackersName = enemyBaseClass.name;
>>>>>>> Alonso
        attack.Type = "Enemy";
        attack.AttackerGameObject = this.gameObject;
        //Attack the player at random if there is an ally to the Main Character it will attack, if there is only one person it will always attack one person.//
        attack.AttackersTarget = battleStateMachine.MainCharacter[Random.Range(0,battleStateMachine.MainCharacter.Count)];

        battleStateMachine.gatherActions(attack);
    }

    private IEnumerator TimeForAction()
    {
        if(ActionStarted)
        {
            yield break;
        }

        ActionStarted = true;

        Vector3 MainCharactersPosition = new Vector3
            (
                HeroAttack.transform.position.x - 1.5f, 
                HeroAttack.transform.position.y, 
                HeroAttack.transform.position.z
            );


        while(MoveTowardsEnemy(MainCharactersPosition))
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        //Back to start animation.//
        Vector3 firstPosition = StartPosition;

        while(MoveTowardsStart(firstPosition))
        {
            yield return null;
        }

        //Reset BattleStateMachine
        battleStateMachine.TurnList.RemoveAt(0);
        battleStateMachine.battleState = BattleStateMachine.PerformAction.WAIT;
        ActionStarted = false;
        //Reset enemy's state.//
        current_Cooldown = 0.0f;
        currentState = TurnState.PROCESSING;
    }

    private bool MoveTowardsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }


    
}
