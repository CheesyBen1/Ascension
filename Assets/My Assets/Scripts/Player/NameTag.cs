using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameTag : NetworkBehaviour
{
    public TextMeshProUGUI nameTagText;

    [SyncVar (hook ="LocalSetName")]
    public string playerName = " ";

   

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        nameTagText.enabled = false;
        if (isLocalPlayer)
        {            
            CmdSetName(PlayerInfo.playerInfo.accountInfo.AccountInfo.Username);
        }                
    }

    [Command]
    public void CmdSetName(string name)
    {
        playerName = name;
    }


    public void LocalSetName(string oldName, string newName)
    {
        nameTagText.text = newName;
    }
}
