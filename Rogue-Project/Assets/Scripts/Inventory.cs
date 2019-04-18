﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public const int AMOUNT_OF_EQUIPPED_ARMOURS = 0;

    GameManager gameManager;

    public Button[] slot;
    public Image[] iconItem;

    public List<GameObject> inventoryItem;
    public List<Item> loadedItems;
    public Sprite[] allImages;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void loadInventory()
    {
        clearLoadedItems();
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        foreach (Button btn in slot)
        {
            btn.interactable = false;
        }

        foreach (Image i in iconItem)
        {
            i.sprite = null;
        }

        int slotId = AMOUNT_OF_EQUIPPED_ARMOURS;
        foreach(GameObject item in inventoryItem)
        {
            GameObject temp = Instantiate(item);
            Item itemInfo = temp.GetComponent<Item>();
            loadedItems.Add(itemInfo);
            iconItem[slotId].sprite = allImages[itemInfo.iconId];
            slot[slotId].GetComponent<InventorySlot>().objectSlot = temp;
            slot[slotId].interactable = true;
            iconItem[slotId].gameObject.SetActive(true);
            slotId++;
        }
    }

    public void clearLoadedItems()
    {
        foreach (Item li in loadedItems)
        {
            Destroy(li);
        }
        loadedItems.Clear();
    }
}
