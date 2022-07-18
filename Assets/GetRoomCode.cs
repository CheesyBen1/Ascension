using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using LightReflectiveMirror;

public class GetRoomCode : NetworkBehaviour
{
    [SyncVar]
    public string roomCode;

    
    // Update is called once per frame
    void Update()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            setCode();
        }
    }

    [Command]
    public void setCode()
    {
        roomCode = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<LightReflectiveMirrorTransport>().serverId.ToString();
    }
}
