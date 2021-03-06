﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryManager : MonoBehaviour
{
    public static void CabinEntry(ref Collider2D collision)
    {

        //activer le safe house level
        GameManager.gameManager.levels[GameManager.currentLevel].SetActive(false);
        GameManager.gameManager.levels[0].transform.position = new Vector3(collision.transform.position.x + 0.26f, collision.transform.position.y + 1.13f, collision.transform.position.z);
        GameManager.gameManager.levels[0].SetActive(true);
        GameManager.previousLevel = GameManager.currentLevel;
        GameManager.currentLevel = 0;

    }
    public static void CabinExit(ref Collider2D collision)
    {

        //desactiver le safe house level
        GameManager.gameManager.levels[GameManager.previousLevel].SetActive(true);
        GameManager.gameManager.levels[0].SetActive(false);
        GameManager.currentLevel = GameManager.previousLevel;

    }

    public static void CavernEntry(ref Collider2D collision, int levelNum)
    {

        //aller vers le prochain level
        GameManager.gameManager.levels[GameManager.currentLevel].SetActive(false);
        GameManager.gameManager.levels[levelNum].SetActive(true);
        GameManager.currentLevel = levelNum;
        if (levelNum == 2)
        {
            AudioManager.CheckSound();
            GameManager.gameManager.camera.backgroundColor = new Color32(21, 18, 4, 255);
        }
        else if (levelNum == 3)
        {
            AudioManager.CheckSound();
            GameManager.gameManager.camera.backgroundColor = new Color32(119, 103, 44, 255);
        }

    }

    public static void CavernExit(ref Collider2D collision, int levelNum)
    {

        //aller vers le dernier level
        GameManager.gameManager.levels[GameManager.currentLevel].SetActive(false);
        if (levelNum <= 1)
        {
            levelNum = 2;
        }
        GameManager.gameManager.levels[--levelNum].SetActive(true);
        GameManager.currentLevel = levelNum;
        GameManager.gameManager.MainCharacter.transform.localPosition = new Vector3(GameManager.gameManager.MainCharacter.transform.localPosition.x + 0.7f, GameManager.gameManager.MainCharacter.transform.localPosition.y, GameManager.gameManager.MainCharacter.transform.localPosition.z);

        AudioManager.CheckSound();
    }

    public static void CabinExit(ref Collider2D collision, int levelNum)
    {

        //sortir de la cabane et aller au dernier level
        GameManager.gameManager.levels[GameManager.currentLevel].SetActive(false);
        if (levelNum >= 0)
        {
            levelNum = 1;
        }
        GameManager.gameManager.levels[--levelNum].SetActive(true);
        GameManager.currentLevel = levelNum;
        GameManager.gameManager.MainCharacter.transform.localPosition = new Vector3(GameManager.gameManager.MainCharacter.transform.localPosition.x + 0.7f, GameManager.gameManager.MainCharacter.transform.localPosition.y, GameManager.gameManager.MainCharacter.transform.localPosition.z);
        AudioManager.CheckSound();
    }

    //private static void CheckSound()
    //{
    //    if (GameManager.currentLevel == 1)
    //    {
    //        FindObjectOfType<AudioManager>().StopPlay("OnGrass");
    //    }
    //    if (GameManager.currentLevel == 2)
    //    {
    //        FindObjectOfType<AudioManager>().StopPlay("OnCavern");
    //    }
    //    if (GameManager.currentLevel == 3)
    //    {
    //        FindObjectOfType<AudioManager>().StopPlay("OnSand");
    //    }
    //}
}
