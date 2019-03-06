using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    #region Variables and etc
    //Handles the timer between each Actions .//
    [Header("Everything concerning the time it takes between attacks.")]
    private float MAX_COOLDOWN = 10.0f;
    private float current_Timer = 0.0f;

    //Animation speed before the attack.//
    [Header("Everything concerning the animation of the player.")]
    public float ANIMATION_SPEED = 10.0f;
    public float ANIMATION_DISTANCE = 5.0F;

    //Script Access.//
    public EnemyBaseClass EBS;
    private BattleStateMachine BSM;

    //Bool.//
    private bool isDefending = false;
    private bool isAlive = true;
    private bool hasActionStarted = false;

    //Game Objects.//
    public GameObject targetPlayer;
    public GameObject enemySelector;

    //Positions.//
    private Vector3 startPosition;

    public enum BattleState
    {
        WAITING,
        CHOOSEACTION,
        BUFFER,
        ACTION,
        DEAD
    }
    public BattleState Current_Battle_State;
    #endregion

    #region Awake, Start, Update
    void Start()
    {
        Current_Battle_State = BattleState.WAITING;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        startPosition = transform.position;
        enemySelector.SetActive(false);
    }

    void Update()
    {
        switch (Current_Battle_State)
        {
            case (BattleState.WAITING):
                Attack_Timer();
                break;

            case (BattleState.CHOOSEACTION):
                if (BSM.MainCharacter.Count == 0)
                {
                    break;
                }
                else
                {
                    chooseAction();
                    Current_Battle_State = BattleState.BUFFER;
                }
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
        if (hasActionStarted) { yield break; }
        hasActionStarted = true;

        //Brings the MainCharacter towards the selected enemy.//
        Vector3 targetPos = new Vector3
            (
                targetPlayer.transform.position.x + ANIMATION_DISTANCE,
                targetPlayer.transform.position.y,
                targetPlayer.transform.position.z
            );


        while (MoveToEnemy(targetPos)) { yield return null; }
        yield return new WaitForSeconds(0.5f);

        //Do the attack to the enemy, depending on chosen attack - Here.//

        //animate back to start position
        Vector3 originPOS = startPosition;
        while (MoveToOrigin(originPOS)) { yield return null; }
        //remove from bsm list
        BSM.TurnList.RemoveAt(0);

        BoS_Reset();

        hasActionStarted = false;
    }

    private void BoS_Reset() //Resets the Battle State at the end of a turn.//
    {
            BSM.Current_Battle_State = BattleStateMachine.BattleState.WAITING;
            current_Timer = 0f;
            Current_Battle_State = BattleState.WAITING;   
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

    #region Enemy Damage
    //void doDamage()
    //{
    //    float damageDone = EBS.currentAttack + BSM.TurnList[0].chosenAttack.attackDmg;
    //    targetPlayer.GetComponent<PlayerStateMachine>().takeDamage(damageDone);
    //}

    public void takeDamage(float damageAmount)
    {
        //reduce hp by damage amount
        EBS.currentHP -= damageAmount;
        //check if dead
        if (EBS.currentHP <= 0)
        {
            EBS.currentHP = 0;
            Current_Battle_State = BattleState.DEAD;
        }

    }

    #endregion

    void chooseAction()
    {
        TurnHandler thisAttack = new TurnHandler();
        thisAttack.Attacker = EBS.enemyName;
        thisAttack.Type = "Enemy";
        thisAttack.AttackersGameObject = this.gameObject;
        thisAttack.Target = BSM.MainCharacter[Random.Range(0, BSM.MainCharacter.Count)];

        //Select an attack - Here.//

        BSM.List_Of_Turns(thisAttack);
    }

    void Attack_Timer() //Prevent AI's failure to crash/stop the flow of the game, if the enemy or the player doesn't attack or do anything within a certain time => Switch character.//
    {
        current_Timer = current_Timer + Time.deltaTime;
        float calcualtion = current_Timer / MAX_COOLDOWN;

        if (current_Timer >= MAX_COOLDOWN)
        {
            Current_Battle_State = BattleState.CHOOSEACTION;
        }
    }
}