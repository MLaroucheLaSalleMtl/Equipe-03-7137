using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    public PlayerBaseClass playerBaseClass;
    public PlayerBehaviour playerBehaviour;
    public GameObject BedPannel;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag == "BedCollider")
        {
            print("colliding with Bed");
            BedPannel.SetActive(true);
            bool pauseState = BedPannel.activeSelf;
            playerBehaviour.enabled = false;
        }

        if (collision.gameObject.tag == "Player" && gameObject.tag == "DoorCollider")
        {
            EntryManager.CabinExit(ref collision);
        }
    }

    public void yesButton()
    {
        //il faut mettre le code pour remplir le HP
        playerBaseClass.currentHP = playerBaseClass.baseHP;
        playerBehaviour.enabled = true;
        BedPannel.SetActive(false);
    }
    public void noButton()
    {
        playerBehaviour.enabled = true;
        BedPannel.SetActive(false);
    }
}