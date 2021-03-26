using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCameraController : MonoBehaviour
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

            transform.localRotation = Quaternion.Euler(10, 90f, 0f);

            PlayerBody.Rotate(Vector3.up * mouseX);
        }

        
        float z = -Input.GetAxis("Horizontal");
        float x = Input.GetAxis("Vertical");

        Vector3 move = PlayerBody.right * x + PlayerBody.forward * z + Vector3.down;

        controller.Move(move * speed * Time.deltaTime);

        float terrainHeight = Terrain.activeTerrain.SampleHeight(PlayerBody.position);
        if(terrainHeight <= WaterHeight){
            PlayerBody.position = pos;
        }

    }
}
