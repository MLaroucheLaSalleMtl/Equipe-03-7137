using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variable List.//
    public bool isWalking = false; 
    public bool CanFight = false; //Prevent from entering a fight twice => enemyVicinity(); //
    public bool isFighting = false;

    public int enemyCount = 0;

    //Lists.//
    public List<GameObject> NumberOfEnemies = new List<GameObject>(); //Manages the numbers of enemies inside a Fight.//


    public static GameManager gameManager;
    public GameObject MainCharacter;

    //Thingy to make battle screen appear, Brad's way thing.

    public enum StateOfGame
    {
        Gameplay,
        Fighting
    }
    public StateOfGame Game_State;

    void Awake()
    {
        CheckGM();

        if(!GameObject.Find("MainCharacter"))
        {
            GameObject MC = Instantiate(MainCharacter, Vector3.zero, Quaternion.identity) as GameObject; //Set the MC vector to 0 and same for rotation (Quaternion).//
            MC.name = "Main Character";
        }
    }

    void Update()
    {
        switch (Game_State)
        {
            case (StateOfGame.Gameplay):

                if(isWalking)
                {
                    vicinityEnemy();
                }
                if(isFighting)
                {
                    Game_State = StateOfGame.Fighting;
                }

                break;

            case (StateOfGame.Fighting):
                //Make shit pop-up.//

                break;
        }
    }

    private void vicinityEnemy()
    {
        if(isWalking && CanFight) //Prevent from entering a fight twice.//
        {
            //Enemy is within range.//
            isFighting = true;
        }
    }

    private void StartFight()
    {
        //Count how many enemies are in the vicinity.//
        for(int i = 0; i < enemyCount; i++)
        {
            //NumberOfEnemies.Add(  //Add enemies within range? // )

        }

        //Popup scene Brad's way.//

        //Player Reset.//
        isWalking = false;
        isFighting = false;
        CanFight = false;
    }

    private void CheckGM() //Check for gameManager object duplication.//
    {
        if (gameManager = this)//If the gameManager exists, if not set it to the GameObject.//
        {
            gameManager = this;
        }
        else if (gameManager != this) //If it already exists, destroy it to prevent duplicates.//
        {
            Destroy(gameObject);
        }
    }

}
