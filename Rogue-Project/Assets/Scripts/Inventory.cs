using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    GameManager gameManager;

    public Button[] slot;
    public Image[] iconItem;

    public List<GameObject> inventoryItem;
    public List<Item> loadedItems;
    public Image[] allImages;


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

        int slotId = 0;
        foreach(GameObject item in inventoryItem)
        {
            GameObject temp = Instantiate(item);
            Item itemInfo = temp.GetComponent<Item>();
            Item[] items = ItemXML.LoadArmors().ToArray();
            foreach (Item i in items)
            {
                loadedItems.Add(i);
            }
            slot[slotId].GetComponent<InventorySlot>().objectSlot = temp;
            slot[slotId].interactable = true;

            //iconItem[slotId].sprite = GameManager.inventoryImage[itemInfo.itemId];  // prend l'image de la liste des items pour charger dans le bouton
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
