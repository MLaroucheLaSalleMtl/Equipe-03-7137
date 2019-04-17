using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PItemInfo : MonoBehaviour
{
    GameManager gameManager;

    public int slotID;
    public GameObject objectSlot;
    private MeleeWeapon weapon;

    [Header("Item Info")]
    public Image itemImg;
    public Text itemName;
    public Text damage;

    //public Image[] statsImage;

    [Header("Buttons")]
    public Button use;
    public Button discard;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void loadItemInfo()
    //{
    //    Item itemInfo = objectSlot.GetComponent<Item>();
    //    int itemId = itemInfo.id;

    //    itemName.text = gameManager.name[itemId];
    //    itemImg.sprite = weapon.image[itemId];
    //    itemType.text = WeaponType[itemId];
    //}
}
