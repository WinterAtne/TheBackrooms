using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    //Stats
    public new string name = "New Item";
    public float weight;


    public Sprite icon;
    public GameObject itemPrefab;
}
