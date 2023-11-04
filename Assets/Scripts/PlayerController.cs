using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine; 
using UnityEngine.InputSystem; 
public class PlayerController : MonoBehaviour 
{
//rotation
    public float horizontalSpeed = 2.0F;
     public float verticalSpeed = 2.0F;
     //.rotation
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    //checking if player is on ground to add drag
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask isGround;
    bool grounded;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;
/// 
private Vector3 MousePositionViewport = Vector3.zero;
private Quaternion DesiredRotation = new Quaternion();
private float RotationSpeed = 15;
/// 
    float mouseX;
    float mouseY = 100;

    float yRotation;
    float xRotation;

    public GameObject player;
///
    Vector3 moveDirection;

    Rigidbody rb;

    public float hungerCount;

    public float speedMultiplier;
    public float currentSpeed;

    public float jumpForce;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update(){
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.7f+0.2f, isGround);
        MyInput();
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
         float v = verticalSpeed * Input.GetAxis("Mouse Y");
         transform.Rotate(v, h, 0);

        //RotationPlayer();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = groundDrag; //should be 0
    }

    private void FixedUpdate(){
        MovePlayer();
    }

    // trying to rotate player with camera/mouse
    private void RotationPlayer(){

    if (Input.GetKey("a")) {
         DesiredRotation = Quaternion.Euler (0, -180, 0);
    } else if(Input.GetKey("d")){
        DesiredRotation = Quaternion.Euler (0, 0, 0);
    } else if(Input.GetKey("s")){
        DesiredRotation = Quaternion.Euler (0, 90, 0);
    }else {
        DesiredRotation = Quaternion.Euler (0, -90, 0);
    }
    transform.rotation = Quaternion.Lerp (transform.rotation, DesiredRotation, Time.deltaTime*RotationSpeed);
    }

    private void MyInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //trying to move with mouse direction
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
    }


    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "PickUp") {
            other.gameObject.SetActive(false);
            BasicNeeds.hunger_remaining += hungerCount;
        }
    }

    private void MovePlayer(){
        //calc move direction
        if(Input.GetKey(KeyCode.LeftShift)){
            currentSpeed = moveSpeed * speedMultiplier;
        } else {
            currentSpeed = moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce * rb.mass, ForceMode.Impulse);
            Invoke("Down",0.4f);
        }
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;// * mouseX; //mouseY;
        rb.AddForce(moveDirection.normalized * currentSpeed * 10F, ForceMode.Force);
    }

    private void Down(){
        rb.AddForce(Vector3.down * jumpForce * rb.mass, ForceMode.Impulse);
    }

}