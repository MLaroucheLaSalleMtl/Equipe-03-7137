using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    #region Variables and etc
    [Header("Defending multiplier - Lower the less damage the player takes.")]
    public const float DEFEND_MULTI = 25;

    //Handles the timer between each Actions .//
    [Header("Everything concerning the time it takes between attacks.")]
    private const float MAX_COOLDOWN = 10.0f;
    private float current_Timer = 0.0f;

    //Animation speed before the attack.//
    [Header("Everything concerning the animation of the player.")]
    public const float ANIMATION_SPEED = 10.0f;
    public const float ANIMATION_DISTANCE = 5.0F;

    //Script Access.//
    public PlayerBaseClass PBS;
    private BattleStateMachine BSM;
    private BaseAttack BasicAttack;

    //Bool.//
    private bool isDefending = false;
    private bool isAlive = true;
    private bool hasActionStarted = false;


    //Game Objects.//
    public GameObject targetEnemy;

    //Positions.//
    private Vector3 startPosition;

    public enum BattleState
    {
        WAITING,
        ADDTOLIST,
        BUFFER,
        ACTION,
    }
    public BattleState State_Of_Battle;
    #endregion

    #region Awake, Start, Update
    void Start()
    {
        State_Of_Battle = BattleState.WAITING;

        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();

        //startPosition = transform.position; => To be seen if we use one or not.//
    }

    void Update()
    {
        switch (State_Of_Battle)
        {
            case (BattleState.WAITING):
                Attack_Timer();
                break;

            case (BattleState.ADDTOLIST):
                BSM.PlayerManagement.Add(this.gameObject);
                State_Of_Battle = BattleState.BUFFER;
                break;

            case (BattleState.BUFFER):
                //Buffer Battle State, for the animation and all.//
                break;

            case (BattleState.ACTION):
                StartCoroutine(actionTimer());
                break;
        }

    }
    #endregion

    #region Animation
    private IEnumerator actionTimer()
    {
        if (hasActionStarted){yield break;}
        hasActionStarted = true;

        //Brings the MainCharacter towards the selected enemy.//
        Vector3 targetPos = new Vector3
            (
                targetEnemy.transform.position.x + ANIMATION_DISTANCE, 
                targetEnemy.transform.position.y, 
                targetEnemy.transform.position.z
            );


        while (MoveToEnemy(targetPos)){yield return null;}
        yield return new WaitForSeconds(0.5f);

        doDamage(); //Attack the selected target.//

        //animate back to start position
        Vector3 originPOS = startPosition;
        while (MoveToOrigin(originPOS)){yield return null;}
        //remove from bsm list
        BSM.TurnList.RemoveAt(0);

        BoS_Reset();

        hasActionStarted = false;
    }

    private void BoS_Reset() //Resets the Battle State at the end of a turn.//
    {
        if (PBS.currentHP > 0)
        {
            BSM.Current_Battle_State = BattleStateMachine.BattleState.WAITING;
            current_Timer = 0f;
            State_Of_Battle = BattleState.WAITING;
        }
        else
        {
            State_Of_Battle = BattleState.BUFFER;
        }
    }

    private bool MoveToEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, ANIMATION_SPEED * Time.deltaTime));
    }

    private bool MoveToOrigin(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, ANIMATION_SPEED * Time.deltaTime));
    }

    #endregion

    #region Player Damage
    public void takeDamage(float damageAmount)
    {
        if(isDefending)
        {
            //Reduces HP on attack received + Defense bonus.//
            PBS.currentHP -= damageAmount * (DEFEND_MULTI / 100);
        }
        else
        {
            //Reduces HP on attack received.//
            PBS.currentHP -= damageAmount;
        }


        if (PBS.currentHP <= 0)
        {
            PBS.currentHP = 0; 
            //Dead battle state.///
        }
    }


    void doDamage()
    {
        float damageDone = BasicAttack.Damage;
        targetEnemy.GetComponent<EnemyStateMachine>().takeDamage(damageDone);
    }

    #endregion

    void Attack_Timer() //Prevent AI's failure to crash/stop the flow of the game, if the enemy or the player doesn't attack or do anything within a certain time => Switch character.//
    {
        current_Timer = current_Timer + Time.deltaTime;
        float calcualtion = current_Timer / MAX_COOLDOWN;

        if (current_Timer >= MAX_COOLDOWN)
        {
            State_Of_Battle = BattleState.ADDTOLIST;
        }
    }

}

