using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;
using UnityEngine.SceneManagement;

public class ReturnToRoom : NetworkRoomPlayer
{
    //public override void OnStartLocalPlayer()
    //{
    //    base.OnStartLocalPlayer();
    //}

    public GameObject player;
    public override void OnClientEnterRoom()
    {
        base.OnClientEnterRoom();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //public override void OnClientEnterRoom()
    //{
    //    base.OnClientEnterRoom();
    //    NetworkClient.RegisterPrefab(player);
    //    Instantiate(player, GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkRoomManager>().GetStartPosition().position, GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkRoomManager>().GetStartPosition().rotation);
    //    player.GetComponent<OnlinePlayerStart>().OnStartLocalPlayer();
    //    player.GetComponent<OnlinePlayerStart>().OnStartAuthority();
    //}

    //public override void OnClientEnterRoom()
    //{
    //    if (isLocalPlayer)
    //    {
    //        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
    //    foreach (MonoBehaviour c in comps)
    //    {
    //        if (c.GetType() != typeof(ReturnToRoom))
    //            c.enabled = true;

    //    }

    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        transform.GetChild(i).gameObject.SetActive(true);
    //    }


    //        if (SceneManager.GetActiveScene().name == "Room")
    //        {
    //            GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().Follow = transform.GetChild(1).transform;


    //            GetComponent<SyncPlayerPush1>().playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    //            GetComponent<Ready>().playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    //            GetComponent<TestChangeScenes>().playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

    //            transform.GetChild(2).GetComponent<Billboard>().mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

    //            transform.position = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkRoomManager>().GetStartPosition().position;
    //            transform.rotation = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkRoomManager>().GetStartPosition().rotation;
    //        }
    //    }
    //}

    ////public override void OnStartLocalPlayer()
    ////{
    ////    base.OnStartLocalPlayer();




    ////    GetComponent<SyncPlayerPush1>().playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    ////    GetComponent<Ready>().playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    ////    GetComponent<TestChangeScenes>().playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

    ////    transform.GetChild(2).GetComponent<Billboard>().mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

    ////}



    ////private void Update()
    ////{
    ////    if (isLocalPlayer)
    ////    {
    ////        GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().Follow = transform.GetChild(1).transform;



    ////        GetComponent<SyncPlayerPush1>().playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    ////        GetComponent<Ready>().playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    ////        GetComponent<TestChangeScenes>().playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

    ////        transform.GetChild(2).GetComponent<Billboard>().mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;


    ////    }


    ////}

    public string roomScene;

    public override void DrawPlayerReadyState()
    {
        GUILayout.BeginArea(new Rect(20f + (index * 100), 200f, 90f, 130f));

        GUILayout.Label(GetComponent<NameTag>().playerName);


        if (readyToBegin)
            GUILayout.Label("Ready");
        else
            GUILayout.Label("Not Ready");

        if (((isServer && index > 0) || isServerOnly) && GUILayout.Button("REMOVE"))
        {
            // This button only shows on the Host for all players other than the Host
            // Host and Players can't remove themselves (stop the client instead)
            // Host can kick a Player this way.
            GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
        }

        GUILayout.EndArea();
    }
    public override void OnClientExitRoom()
    {
        if (SceneManager.GetActiveScene().name != roomScene)
        {
            MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour c in comps)
            {
                if (c.GetType() != typeof(ReturnToRoom))
                    c.enabled = false;

            }

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            transform.position = new Vector3(0, 1.375f, 0);
        }
    }
}
