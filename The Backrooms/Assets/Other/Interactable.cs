using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Transform thisTransform;
    private Transform playerTransform;
    

    [SerializeField] float interactionRadius = 2f;
    [SerializeField] LayerMask playerMask;

    public virtual void Start() {
        thisTransform = this.transform;
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update() {
        if (CheckInteractionRadius() && Input.GetKeyDown(KeyCode.E)) {
            Interact();
        }
    }

    bool CheckInteractionRadius() {
        return(Physics.CheckSphere(thisTransform.position, interactionRadius, playerMask));
    }



    public virtual void Interact() {
        Debug.LogError("Null Interaction: Please attach an interaction to " + this.gameObject.name);


        //MUST BE OVERWRITTEN BY THE CHILDREN OF THIS SCRIPT
    }
}
