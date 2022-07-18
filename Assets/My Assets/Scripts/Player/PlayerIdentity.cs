using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerIdentity : NetworkBehaviour
{
    [SyncVar]
    public string playerName = " ";

    public override void OnStartLocalPlayer()
    {
        
       SetName(PlayerInfo.playerInfo.accountInfo.AccountInfo.Username);
        
    }


    [Command]
    public void SetName(string name)
    {
        playerName = name;
    }
}
