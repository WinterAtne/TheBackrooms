using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Inventory : MonoBehaviour
{
    private Transform player;

    private GameObject equippedItem;
    private int equippedItemIndex;

    [SerializeField] Transform hand;
    [SerializeField] Item[] items = new Item[2];
    [SerializeField] Image[] itemIcon = new Image[2];




    private void Start() {
        player = GameObject.Find("Player").GetComponent<Transform>();
        UpdateItems();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Destroy(equippedItem);
            equippedItemIndex = 0;
            EquipItem(equippedItemIndex);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Destroy(equippedItem);
            equippedItemIndex = 1;
            EquipItem(equippedItemIndex);
        }




        if (Input.GetKeyDown(KeyCode.Q)) {
            DropItem();
        }
    }






    public bool AddItem(Item item) {
        for (int i = 0; i < items.Length; i++) {
            if (items[i] == null) {
                items[i] = item;
                UpdateItems();
                return(true);
            }
        }

        return(false);
    }

    public void RemoveItem(int removeItem) {
        items[removeItem] = null;
        UpdateItems();
    }

    private void DropItem() {
        if (equippedItem != null) {
            Rigidbody rb = equippedItem.AddComponent(typeof(Rigidbody)) as Rigidbody;
            equippedItem.GetComponent<ItemScript>().enabled = true;

            equippedItem.transform.parent = null;
            equippedItem = null;
            RemoveItem(equippedItemIndex);
        }
    }

    private void EquipItem(int i) {
        if (items[i] != null) {
            equippedItem = Instantiate(items[i].itemPrefab, hand.position, hand.rotation);
            equippedItem.transform.parent = hand;
        }
    }



    private void UpdateItems()
    {
        for (int i = 0; i < itemIcon.Length - 1; i++)
        {

            if (items[i] != null)
            {
                itemIcon[i].sprite = items[i].icon;
            } else {
                itemIcon[i].sprite = null;
            }
        }
    }
}
