using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private PItemInfo pItemInfo;
    private GameManager gameManager;
    public GameObject objectSlot;

    public int slotID;

    // Start is called before the first frame update
    void Awake()
    {
        objectSlot = gameObject;
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        pItemInfo = FindObjectOfType(typeof(PItemInfo)) as PItemInfo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void useItem()
    {
        //objectSlot.SendMessage("useItem",SendMessageOptions.DontRequireReceiver);
        pItemInfo.objectSlot = objectSlot;
        pItemInfo.slotID = slotID;
        gameManager.openItemInfoPannel();
    }
}
