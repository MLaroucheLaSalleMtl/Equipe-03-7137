using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text inventoryText = GetComponent<Text>();
        inventoryText.text = gameObject.transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
