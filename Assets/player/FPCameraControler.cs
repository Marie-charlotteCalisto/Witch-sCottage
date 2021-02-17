using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCameraControler : MonoBehaviour
{
    private float mouseSensitivity = 100f;

    public Transform PlayerBody;

    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -20f, 70f);

            transform.localRotation = Quaternion.Euler(xRotation, 90f, 0f);

             PlayerBody.Rotate(Vector3.up * mouseX);
        }

    }
}
