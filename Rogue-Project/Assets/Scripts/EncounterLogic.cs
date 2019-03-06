using System.Collections;
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
    private float chanceTime = 0f;
    private float maxChanceTime = 1f;
    private bool isIn = false;
    void Update() {
        if (isIn)
        {
            chanceTime += Time.deltaTime;
           
            if (chanceTime > maxChanceTime)
            {
                if (Random.Range(1, 100) <20)
                {
                    game.StartFight();
                }
                chanceTime = 0;
            }

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