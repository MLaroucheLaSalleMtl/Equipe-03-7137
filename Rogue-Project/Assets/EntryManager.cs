using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryManager : MonoBehaviour
{
    public static void CabinEntry(ref Collider2D collision)
    {

        //activer le safe house level
        GameManager.gameManager.levels[GameManager.currentLevel].SetActive(false);
        GameManager.gameManager.levels[0] = Instantiate(GameManager.gameManager.levels[0], collision.transform.position, Quaternion.identity);
        GameManager.gameManager.levels[0].SetActive(true);
        GameManager.currentLevel = 0;

    }

    public static void CavernEntry(int levelNum)
    {

        //activer le safe house level
        GameManager.gameManager.levels[GameManager.currentLevel].SetActive(false);
        GameManager.gameManager.levels[levelNum].SetActive(true);
        GameManager.currentLevel = levelNum;

    }

}
