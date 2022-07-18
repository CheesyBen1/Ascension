using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Mirror;
using LightReflectiveMirror;

public class IpManager : NetworkBehaviour
{
    [SyncVar]
    public string roomCode;

    public GameObject networkManager;
    private void Start()
    {
        networkManager = GameObject.FindGameObjectWithTag("NetworkManager");
    }


    //public GameObject getRoomCode;
    private void Update()
    {
        //networkManager = GameObject.FindGameObjectWithTag("NetworkManager");
        GetLocalIPAddress();
        GetComponent<TextMeshProUGUI>().text = roomCode;
        //GetComponent<TextMeshProUGUI>().text = getRoomCode.GetComponent<GetRoomCode>().roomCode;
    }

    [Command(requiresAuthority = false)]
    public void GetLocalIPAddress()
    {
        
        roomCode = networkManager.GetComponent<LightReflectiveMirrorTransport>().serverId.ToString();
    }
}
