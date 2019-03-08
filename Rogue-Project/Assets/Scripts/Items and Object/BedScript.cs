using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{

    public GameObject BedPannel;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag == "BedCollider")
        {
            print("colliding with Bed");
            bool pauseState = BedPannel.activeSelf;
        }

        if (collision.gameObject.tag == "Player" && gameObject.tag == "DoorCollider")
        {
            EntryManager.CabinExit(ref collision);
        }
    }

    public void yesButton()
    {
        //il faut mettre le code pour remplir le HP
        BedPannel.SetActive(false);
    }
    public void noButton()
    {
        //il faut mettre le code pour remplir le HP
        BedPannel.SetActive(false);
    }
}