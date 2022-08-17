using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //Character Components (Defined in Start())
    private Rigidbody rb;
    private Transform tf;
    private Player_Primary primary;

    //Used in the GroundCheck() method;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    private float groundDistance = 0.1f;


    //Movement Variables
    [SerializeField] float speed = 2f;
    [SerializeField] float sprintSpeed = 5f;
    [SerializeField] float crouchSpeed = 1f;
    private float moveSanityDrain = 1.5f; //The Additional sanity that will be drained while running or crouching;

    [SerializeField] float jumpHeight = 1f;
    [SerializeField] float stepHeight = 0.1f;

    //Movement Dirrections;
    private float x; //These three are here so that they don't get collected by the gc;
    private float y;
    private float z;
    Vector3 jumpForce;
    Vector3 dirrection;
    Vector3 gravity;

    //Movement Manner (these are accessed in the Item_Controller & Player_Look Scripts);
    public  bool isWalking;
    public bool isCrouching;
    public bool isSprinting;
    public bool isGrounded;

    //Used in StairStep();
    [SerializeField] Transform lowstep;
    [SerializeField] Transform heighstep;
    private float rayCastLookAhead = 0.3f;
    

    //Other Movement Variables
    float standingHeight;
    public float crouchingHeight = 0.5f; //Accessed in Item_Controller;

    void Start() {
        standingHeight = this.transform.localScale.y;
        rb = this.GetComponent<Rigidbody>();
        tf = this.GetComponent<Transform>();
        primary = this.GetComponent<Player_Primary>();
    }



    void FixedUpdate() {
        isGrounded = GroundCheck();

        InputCall();
        InputInterprate();
        MovementApplyAndRegulate();
        StairStep();
    }



    bool GroundCheck() {
        return(Physics.CheckSphere(groundCheck.position, groundDistance, groundMask));
    }

    void InputCall() { // Figures out what exactly the character is inputting;

        //Input for MovementDirrection;
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        y = Input.GetAxis("Jump");

        //Input for Movement Type;
        if (Input.GetKey(KeyCode.LeftControl)) {
            isCrouching = true;
        } else {
            isCrouching = false;
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            isSprinting = true;
        } else {
            isSprinting = false;
        }
    }

    void InputInterprate() { // Figures out what the input actually means, aka figures out if we should be running or crouching; in addition to the exact forces we sould be applying;

        //A
        if ((isCrouching != true && isSprinting != true) || (primary.ReturnSanity() <= 0)) {
            isWalking = true;
            isCrouching = false;
            isSprinting = false;
        } else if (isSprinting == true) { //Its probably a good idea to just start sprinting regardless of wheather we are crouching, simply due to the fact we are not making a rage game;
            isWalking = false;
            isCrouching = false;
        } else if (isCrouching == true && y == 0) { //Also probably a good idea to just go ahead and jump;
            isWalking = false;
        }

        //B
        if (isGrounded == true) {
            jumpForce = transform.up * y;
        } else {
            jumpForce = Vector3.zero;
        }

        dirrection = transform.right * x + transform.forward * z;
    }

    void MovementApplyAndRegulate() { //Also applies the sanity changes involved so that we can half the number of required of if statements;

        gravity = transform.up * rb.velocity.y; // Ensures we don't kill our vertical velocity everytime we call FixedUpdate();

        if (isWalking == true) {
            rb.velocity = (((dirrection * speed) + (jumpForce * jumpHeight)) + gravity);
            UnCrouch();
        } else if (isSprinting == true) {
            rb.velocity = (((dirrection * sprintSpeed) + (jumpForce * jumpHeight)) + gravity);
            SanityUponMovement();
            UnCrouch();
        } else if (isCrouching == true) {
            rb.velocity = ((dirrection * crouchSpeed) + gravity);
            SanityUponMovement();
            Crouch();
        }
    }

    void StairStep() {
        if (Physics.Raycast(lowstep.position, transform.TransformDirection(Vector3.forward), rayCastLookAhead, groundMask)) {
            if (!Physics.Raycast(heighstep.position, transform.TransformDirection(Vector3.forward), rayCastLookAhead, groundMask) && (rb.velocity.x != 0 || rb.velocity.z != 0)) {
                transform.position += transform.up * stepHeight;
            }
        }
    }

    void Crouch() {
        if (tf.localScale.y > 0.5) {
            tf.localScale = new Vector3(tf.transform.localScale.x, crouchingHeight, tf.localScale.z);
        }
    }

    void UnCrouch() {
        if (tf.localScale.y < 1) {
            tf.localScale = new Vector3(tf.localScale.x, 1f, tf.localScale.z);
        }
    }

    void SanityUponMovement() {
        primary.SanityChangeApply(moveSanityDrain * Time.deltaTime);
    }
}
