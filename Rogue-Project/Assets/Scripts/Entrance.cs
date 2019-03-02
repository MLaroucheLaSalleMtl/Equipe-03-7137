using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entrance : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "CavernEntry")
        {
            CavernEntry();
        }
        else if (gameObject.tag == "CabinEntry")
        {
            CabinEntry();
        }
    }

    private void CabinEntry()
    {
        throw new NotImplementedException();
    }

    private void CavernEntry()
    {
        throw new NotImplementedException();
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
