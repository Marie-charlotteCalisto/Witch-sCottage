﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCameraControler : MonoBehaviour
{
    private float mouseSensitivity = 100f;

    public Transform PlayerBody;
    public CharacterController controller;
    public GameObject Water;


    private float xRotation = 0f;
    private float WaterHeight;
    private float speed = 12f;
    private Vector3 pos;

    void Start()
    {
        WaterHeight = Water.transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {

        pos = PlayerBody.position;
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

        float z = -Input.GetAxis("Horizontal");
        float x = Input.GetAxis("Vertical");

        Vector3 move = PlayerBody.right * x + PlayerBody.forward * z + Vector3.down;

        controller.Move(move * speed * Time.deltaTime);

        float terrainHeight = Terrain.activeTerrain.SampleHeight(transform.position);
        if(terrainHeight <= WaterHeight){
            PlayerBody.position = pos;
        }

    }

}
