using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private PItemInfo pItemInfo;
    private GameManager gameManager;
    public GameObject objectSlot;
    private Inventory inventory;

    public int slotID;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        pItemInfo = FindObjectOfType(typeof(PItemInfo)) as PItemInfo;
        inventory = FindObjectOfType(typeof(Inventory)) as Inventory;
        objectSlot = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void useItem()
    {
        objectSlot.SendMessage("useItem", SendMessageOptions.DontRequireReceiver);
        pItemInfo.objectSlot = objectSlot;
        pItemInfo.slotID = slotID;
        //pItemInfo.itemImg = inventory.allImages[slotID];
        //pItemInfo.itemName = inventory.inventoryItem[slotID].name;

        gameManager.openItemInfoPannel();
    }

    public void RemoveItem()
    {

    }
}
