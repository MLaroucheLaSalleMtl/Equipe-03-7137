using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entrance : MonoBehaviour
{
    [SerializeField] private int levelNum = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("colliding with _______");
        if (collision.gameObject.tag == "Player" && gameObject.tag == "CavernEntry")
        {
            print("colliding with cavern");
            EntryManager.CavernEntry(levelNum);
        }
        else if (collision.gameObject.tag == "Player" && gameObject.tag == "CabinEntry")
        {
            print("colliding with cabin");
            EntryManager.CabinEntry(ref collision);
        }
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
