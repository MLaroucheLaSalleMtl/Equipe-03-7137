﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterLogic : MonoBehaviour
{
    private GameManager game;
    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    private static float chanceTime = 0f;
    private float maxChanceTime = 2f;
    private bool isIn = false;

    static public void ResetChance() {
        chanceTime = 0f;
    }
    void Update() {
        
        if (isIn)
        {
            chanceTime += Time.deltaTime;
           
            if (chanceTime > maxChanceTime)
            {
                if (Random.Range(1, 100) <200)
                {
                    if (gameObject.transform.parent.name == "Level4")
                    {
                        game.BossFight();
                    }
                    else
                    {
                        //print(chanceTime);
                        game.StartFight();
                    }
                   
                }

                chanceTime = 0;
            }
            //print(chanceTime);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isIn = true;

        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isIn = false;
        }
    }
}