using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {

    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 2.0f;
    public float upDownRange = 60.0f;

    private float rotUpDown = 0;
    private float baseMovementSpeed;
    private bool sprintActive;

    // Use this for initialization
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        sprintActive = false;
        baseMovementSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update() {
        //Rotation
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        rotUpDown -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotUpDown = Mathf.Clamp(rotUpDown, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(rotUpDown, 0, 0);

        //Movement
        float translation = Input.GetAxis("Vertical") * movementSpeed;
        float strafe = Input.GetAxis("Horizontal") * movementSpeed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);

        //if (Input.GetKeyDown("escape")) {
        // Cursor.lockState = CursorLockMode.None;
        //}

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            sprintActive = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            sprintActive = false;
        }

        if (sprintActive) {
            movementSpeed = baseMovementSpeed + 6;
        }

        else {
            movementSpeed = baseMovementSpeed;
        }
    }
}