using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public const int AMOUNT_OF_EQUIPPED_ARMOURS = 0;

    GameManager gameManager;
    public static Inventory inventory = new Inventory();
    public Button[] slot;
    public Image[] iconItem;

    public List<GameObject> inventoryItem;
    public List<Item> loadedItems;
    
    public Sprite[] allImages;

    public List<Armor> armor = new List<Armor>();
    public List<MeleeWeapon> melee = new List<MeleeWeapon>();
    public List<DistanceWeapon> distance = new List<DistanceWeapon>();
    public List<Potion> potion = new List<Potion>();

    void Start()
    {
        //ItemXML.LoadAllItems(ref armor, ref melee, ref distance, ref potion);
    }
    //an idea : instantiate all items in the loadallitems, and then delete them when out of the inventory attention TODO put the action taskbar in every slot
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
        inventory = FindObjectOfType(typeof(Inventory)) as Inventory;
        //ItemXML.LoadAllItems(ref armor, ref melee, ref distance, ref potion);
        //armor = ItemXML.LoadArmors();

        int slotId = AMOUNT_OF_EQUIPPED_ARMOURS;
        foreach(GameObject item in inventoryItem)
        {
            GameObject temp = Instantiate(item);
            Item itemInfo = temp.GetComponent<Item>();
            loadedItems.Add(itemInfo);
            //iconItem[slotId].sprite = allImages[itemInfo.iconId];
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
