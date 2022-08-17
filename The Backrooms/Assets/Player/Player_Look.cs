using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Look : MonoBehaviour
{
    [SerializeField] Transform playerBody;
    [SerializeField] Camera camera;
    [SerializeField] Player_Movement movement;

    private float xRotation = 0f;
    [SerializeField] float mouseSensitivity = 350f;

    [SerializeField] float sprintingFieldOfView;
    [SerializeField] float standardFieldOfView;
    private float sprintZoomIncriment;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        camera.fieldOfView = standardFieldOfView;
        sprintZoomIncriment = (sprintingFieldOfView - standardFieldOfView) * 6; //Over a single second is too long, instantly is too short, we played around in the middle and 6 works
    }

    void Update() {
        MouseLook();
        SprintFOV();
    }




    private void MouseLook() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void SprintFOV() {
        if (movement.isSprinting == true && camera.fieldOfView < sprintingFieldOfView) { //It "doesn't feel right" if we don't change the field of view and do it over the course of a moment;
            camera.fieldOfView += sprintZoomIncriment * Time.deltaTime;
        } else if (movement.isSprinting == false && camera.fieldOfView > standardFieldOfView) {
            camera.fieldOfView -= sprintZoomIncriment * Time.deltaTime;
        }
    }
}
