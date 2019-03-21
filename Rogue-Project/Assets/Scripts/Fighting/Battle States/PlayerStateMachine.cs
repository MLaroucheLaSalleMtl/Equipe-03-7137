using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PlayerState {
    WAITINGFORINPUT,
    ATTACK,
    DEFEND,
    STARTUP,
    RUN,
    USE_SKILL,
    USE_ITEM,
    DEAD,
    BUFFER

}
[System.Serializable]
public enum PlayerInput {

    ATTACK,DEFEND,INSPECT,USE_ITEM,USE_SKILLS,RUN,NULL


}

public class PlayerStateMachine : MonoBehaviour, StateMachine
{
  
    [Header("Defending multiplier - Lower the less damage the player takes.")]
    public float DEFEND_MULTI = 25;

    //Handles the timer between each Actions .//
    [Header("Everything concerning the time it takes between attacks.")]
    private float MAX_COOLDOWN = 10.0f;
    private float current_Timer = 0.0f;

    //Animation speed before the attack.//
    [Header("Everything concerning the animation of the player.")]
    public float ANIMATION_SPEED = 1000000000.0f;
    public float ANIMATION_DISTANCE = 5.0F;

    //Script Access.//
    public PlayerBaseClass PBS;
    private BattleStateMachine BSM;
    private BaseAttack BasicAttack;

    //Bool.//
    private bool isDefending = false;
    public GameObject miniShield;
    private bool isAlive = true;
    private bool hasActionStarted = false;


    //Game Objects.//
    //public EnemyStateMachine targetEnemy; not useful
    public GameObject playerSelector;

    //Positions.//
    private Vector3 startPosition;


    public PlayerState State_Of_Battle;
    public PlayerInput input = PlayerInput.NULL;

  

   
    void Awake() {
        startPosition = transform.position;
    }
    void Start()
    {
        
      //  miniShield.SetActive(false);
        current_Timer = 0.0f;
        State_Of_Battle = PlayerState.WAITINGFORINPUT;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        playerSelector.SetActive(false);



        //startPosition = transform.position; => To be seen if we use one or not.//
    }
   public  void StartUp() {
       // miniShield.SetActive(false);
        State_Of_Battle = PlayerState.WAITINGFORINPUT;
        input = PlayerInput.NULL;
    }

    #region Animation
    public IEnumerator actionTimer()
    {
        //startPosition = gameObject.transform.position;
        if (hasActionStarted) { yield break; }
        hasActionStarted = true;

        //Brings the MainCharacter towards the selected enemy.//
        Vector3 targetPos = new Vector3
            (
                targetEnemy.gameObject.transform.position.x + ANIMATION_DISTANCE,
                targetEnemy.transform.position.y,
                targetEnemy.transform.position.z
            );


        while (MoveToEnemy(targetPos)) { yield return null; }
        yield return new WaitForSeconds(0.5f);

        doDamage(); //Attack the selected target.//

        //animate back to start position
        Vector3 originPOS = startPosition;
        print(originPOS);

        while (MoveToOrigin(originPOS)) { yield return null; }
        //remove from bsm list
       // BSM.TurnList.RemoveAt(0);

       // BoS_Reset();

        hasActionStarted = false;
    }

    void MovingShieldAnim() {
=======
>>>>>>> SPRINTenCOURS


 

>>>>>>> Stashed changes
    private void BoS_Reset() //Resets the Battle State at the end of a turn.//
    {
        if (PBS.currentHP > 0)
        {
            BSM.Current_Battle_State = BattleState.WAITINGFORINPUT;
            current_Timer = 0f;
            State_Of_Battle = PlayerState.WAITINGFORINPUT;
            isDefending = false;
        }
        else
        {
            State_Of_Battle = PlayerState.BUFFER;
        }
    }

    public bool MoveToEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, ANIMATION_SPEED * Time.deltaTime));
    }

    private bool MoveToOrigin(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, ANIMATION_SPEED * Time.deltaTime));
    }

    void Attack_Timer() //Prevent AI's failure to crash/stop the flow of the game, if the enemy or the player doesn't attack or do anything within a certain time => Switch character.//
    {
        current_Timer = current_Timer + Time.deltaTime;
        float calcualtion = current_Timer / MAX_COOLDOWN;

        if (current_Timer >= MAX_COOLDOWN)
        {
           // State_Of_Battle = PlayerState.ADDTOLIST;
        }
    }
   

    public void Defend() {
        isDefending = true;
        State_Of_Battle = PlayerState.DEFEND;
    }

    public void EndTurn() {
        input = PlayerInput.NULL;


    }

    public void Attack(EnemyStateMachine targetEnemy)
    {
        BasicAttack = new BasicAttack();
        targetEnemy.EBS.currentHP -= BasicAttack.Damage - targetEnemy.EBS.currentDefense;
        State_Of_Battle = PlayerState.ATTACK;
    }
}
