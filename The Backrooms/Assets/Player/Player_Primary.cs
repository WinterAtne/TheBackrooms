using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class Player_Primary : MonoBehaviour
{
    //Sanity Variables
    [SerializeField] float maximumSanity = 1000;
    [SerializeField] float currentSanity;

    [SerializeField] float baseSanityDecay = 0.1f;
    private float sanityChange = 0f;
    private float sanityDecayMultiplier = 0f;

    //UI
    [SerializeField] UI_Elements _UIElements;
    [SerializeField] Scene_Manager scenemanager;

    private void Start() {
        currentSanity = maximumSanity;
        _UIElements.SetMaxSanity(maximumSanity);
        _UIElements.SetSanity(currentSanity);
    }


    private void Update() {
        CalculateSanity();
    }

    private void CalculateSanity() {
        currentSanity -= (baseSanityDecay + sanityDecayMultiplier) * Time.deltaTime + sanityChange;
        if (currentSanity <= 0) {
            currentSanity = 0;
        }
        sanityChange = 0f;
        _UIElements.SetSanity(currentSanity);
    }

    public void SanityChangeApply(float amount) {
        sanityChange += amount;
    }

    public void SanityMultiplierApply(float amount) {
        sanityDecayMultiplier += amount;
    }

    public float ReturnSanity() {
        return(currentSanity);
    }
}
