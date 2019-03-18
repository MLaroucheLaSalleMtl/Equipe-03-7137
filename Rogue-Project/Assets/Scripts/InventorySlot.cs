using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{    

    public GameObject objectSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void useItem()
    {
        objectSlot.SendMessage("useItem",SendMessageOptions.DontRequireReceiver);
    }
}
