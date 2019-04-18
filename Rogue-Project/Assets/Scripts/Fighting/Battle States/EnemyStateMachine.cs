using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Move {
Attack,Defend,Skill,Item

}

public class EnemyStateMachine : MonoBehaviour,StateMachine
{

   
    //Handles the timer between each Actions .//
    [Header("Everything concerning the time it takes between attacks.")]
    private float MAX_COOLDOWN = 0.5f;
    private float current_Timer = 0.0f;

    //Animation speed before the attack.//
    [Header("Everything concerning the animation of the player.")]
    public float ANIMATION_SPEED = 10.0f;
    public float ANIMATION_DISTANCE = 5.0F;

    //Script Access.//
    public EnemyBaseClass EBS;
    private BattleStateMachine BSM;
    public ElfAI elfAI;
    public OrcAI OAI;
    private GoblinAI GAI;

    //Bool.//
    private bool isDefending = false;
    private bool isAlive = true;
    private bool hasActionStarted = false;

    //Game Objects.//
    public PlayerStateMachine targetPlayer;
    public GameObject enemySelector;

    //Positions.//
    private Vector3 startPosition;
    public Text UIname;
    
    public BattleState Current_Battle_State;
        
 
   public  void StartBattle()
    {
        if (EBS != null)
        {

            UIname.text = EBS.enemyName;
            
            print("UI: "+UIname.text);
        }
        


    }

    void Update()
    {
        //StartBattle();
    //    switch (Current_Battle_State)
    //    {
    //        case (BattleState.WAITINGFORINPUT):
    //            Attack_Timer();
    //            break;

    //        case (BattleState.CHOOSEACTION):
    //            if (BSM.Players.Count == 0)
    //            {
    //                break;
    //            }
    //            else
    //            {
    //                chooseAction();
    //                Current_Battle_State = BattleState.BUFFER;
    //            }
    //            break;

    //        case (BattleState.BUFFER):
    //            //Buffer Battle State, for the animation and all.//
    //            break;

    //        case (BattleState.ATTACK):
    //            StartCoroutine(actionTimer());
    //            break;
        
    }
     
    

  
    private bool MoveToEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, ANIMATION_SPEED * Time.deltaTime));
    }

    private bool MoveToOrigin(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, ANIMATION_SPEED * Time.deltaTime));
    }
   

    
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

    public void DoMove(PlayerBaseClass PBS) {

        if(EBS.enemyName == "Goblin")
        {
            Debug.Log("Goblin turn");
            EBS.GobAi.Attack(EBS, PBS);
        }

        if (EBS.enemyName == "Orc")
        {
            Debug.Log("Orc turn");
            EBS.OrcAi.Attack(EBS, PBS);
        }

        if (EBS.enemyName == "Elf")
        {
            Debug.Log("Elf turn");
            EBS.ElfAi.Attack(EBS, PBS);
        }

        if (EBS.enemyName == "Demon King")
        {
            EBS.DKAi.Attack(EBS, PBS);
        }
    }

    //void chooseAction()
    //{
    //    TurnHandler thisAttack = new TurnHandler();
    //    thisAttack.Attacker = EBS.enemyName;
    //    thisAttack.Type = "Enemy";

    //    //thisAttack.Target = BSM.MainCharacter[Random.Range(0, BSM.MainCharacter.Count)];

    //    //Select an attack - Here.//

    //    BSM.List_Of_Turns(thisAttack);
    //}

    public void Attack(PlayerStateMachine p)
    {
        float modifier = 1f;

        if (p.isDefending) {
            modifier = 0.5f;
        }

       p.PBS.currentHP -= EBS.currentAttack*modifier/10 ;

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

    public void DoAi() {

    }
}