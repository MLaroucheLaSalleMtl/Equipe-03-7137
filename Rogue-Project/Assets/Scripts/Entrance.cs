using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entrance : MonoBehaviour
{
    static int levelNum = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("colliding with _______");
        if (collision.gameObject.tag == "Player" && gameObject.tag == "CavernEntry")
        {
            print("colliding with cavern");
            Entrance.CavernEntry();
        }
        else if (collision.gameObject.tag == "Player" && gameObject.tag == "CabinEntry")
        {
            print("colliding with cabin");
            Entrance.CabinEntry(ref collision);
        }
    }

    public static void CabinEntry(ref Collider2D collision)
    {
        GameObject level = GameManager.gameManager.levels[levelNum];//trouver le gameobject du level présent
        level.SetActive(false);
        //activer le safe house level
        GameManager.gameManager.levels[0] = Instantiate(GameManager.gameManager.levels[0], collision.transform.position, Quaternion.identity);
        GameManager.gameManager.levels[0].SetActive(true);
    }

    public static void CavernEntry()
    {
        GameObject level = GameManager.gameManager.levels[levelNum];//trouver le gameobject du level présent
        level.SetActive(false);
        //activer le safe house level
        GameManager.gameManager.levels[++levelNum].SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
