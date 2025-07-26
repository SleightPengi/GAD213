using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOpening : MonoBehaviour
{
    [SerializeField] private GameObject GameplayUI;
    [SerializeField] private GameObject Inventory;
    [SerializeField] private Inventory _ItemHolder;


    private bool inventoryOpen = false;
    [SerializeField] public int itemsReady = 0; 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && inventoryOpen == false)
        {
            inventoryOpen = true;
            Inventory.SetActive(true);
            GameplayUI.SetActive(false);
            Time.timeScale = 0;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && inventoryOpen == true)
        {
            inventoryOpen = false;
            GameplayUI.SetActive(true);
            Inventory.SetActive(false);
            Time.timeScale = 1;
        }

        if (inventoryOpen == true)
        {
            transferItems();
        }
        
    }


    void transferItems()
    {
        
        
        for (int i = 0; i < itemsReady; i++)
        {
            _ItemHolder.SpawnInventoryItem();
            itemsReady--;
        }
    }



}
