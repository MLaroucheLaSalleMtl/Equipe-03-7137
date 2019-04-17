using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2 : MonoBehaviour
{
    private GameManager gameManager;
    private ObjectFactory objectFactory;

    public int ItemId;
    public List<Item> ListTiems = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void useItem()
    {
        // les commandes quand l'item est utilisé
        print("çet item" + ItemId + "a été utilisé!"); // pour tester si fonctionne.
        //gameManager.useItemWeapon(itemId); // pour appeler la methode d'echange d'arme
    }

    
}
