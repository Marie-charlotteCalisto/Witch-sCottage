using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCameraController : MonoBehaviour
{
    private float mouseSensitivity = 100f;

    public Transform PlayerBody;
    public CharacterController controller;


    private float xRotation = 0f;
    private float speed = 12f;

    // Update is called once per frame
    void Update()
    {
        // Rotation
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(10, 90f, 0f);

            PlayerBody.Rotate(Vector3.up * mouseX);
        }

        
        float z = -Input.GetAxis("Horizontal");
        float x = Input.GetAxis("Vertical");

        Vector3 move = PlayerBody.right * x + PlayerBody.forward * z;

        controller.Move(move * speed * Time.deltaTime);

    }
}
