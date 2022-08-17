using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Elements : MonoBehaviour
{

    [SerializeField] Slider sanitySlider;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject inventory;

    

    //Pullup Inventory
    public void InventoryEnable() {
        hud.SetActive(false);
        inventory.SetActive(true);
    }

    public void InventoryDisable() {
        hud.SetActive(true);
        inventory.SetActive(false);
    }


    //Hud Functions;
    public void SetMaxSanity(float sanity) {
        sanitySlider.maxValue = sanity;
    }

    public void SetSanity(float sanity) {
        sanitySlider.value = sanity;
    }
}
