using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChangeScenes : MonoBehaviour
{

    public GameObject playerCamera;
    public int rayCastDist = 2;

    public NetworkRoomManager manager;
    public string nextRoom;
    
    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        manager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkRoomManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, rayCastDist))
        {
            if (hit.transform.tag == "TestButton")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    manager.ServerChangeScene(hit.transform.name);
                }
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

            }
        }
    }
}
