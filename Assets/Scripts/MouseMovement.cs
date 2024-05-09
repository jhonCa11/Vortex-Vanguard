using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSens = 100f;

    float x = 0f;
    float y = 0f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;
    // Start is called before the first frame update
    void Start()
    {
        //Locking the cursor to the middle of the screen an making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Getting the mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        // Rotation around the x axis (Look up and down)
        x -= mouseY;
        //Clamp Rotation
        x = Mathf.Clamp(x, topClamp, bottomClamp);
        //Rotation around the y axis (Look left and right)
        y += mouseX;
        //Apply rotations to our transform
        transform.localRotation = Quaternion.Euler(x,y,0f);
    }
}
