using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    public float sensX;
    public float sensY;
    public Transform orientation;
    float yRotation;
    float xRotation;
    public Transform cameraPosition;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        offset = transform.position ;
    }

    private void Update(){
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        //stop from rotating too much (took out x limit bc its better like this tbh)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        // get x movement from camera to move around player
        //var theta = (float) 1.5 * Mathf.Sin(xRotation/2);
        //float xmove = theta;
        //Vector3 moveCam = new Vector3(xmove, 0.0f, 0.0f);
        transform.position = cameraPosition.position; //+ moveCam;


    }

    //cam behind player
    void LateUpdate(){
        transform.position = player.transform.position + offset;
    }

}

