using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : Interactable
{
    
    [SerializeField] Item item;
    private Player_Inventory inventory;

    public override void Start() {
        base.Start();

        inventory = GameObject.Find("GUI").GetComponent<Player_Inventory>();
    }

    

    public override void Interact() {
        if (inventory.AddItem(item)) {
            Debug.Log("Picking up " + item.name);
            Destroy(this.gameObject);
        } else {
            Debug.Log("Failled to pick up" + item.name);
        }
    }
}
