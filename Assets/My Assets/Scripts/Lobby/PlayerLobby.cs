//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Mirror;

//public class PlayerLobby : NetworkBehaviour
//{

//    public static PlayerLobby localPlayer;

//    private void Start()
//    {
//        if (isLocalPlayer)
//        {
//            localPlayer = this;
//        }
//    }

//    public void HostGame()
//    {
//        string matchID = MatchMaker.GetRandomMatchID();
//        CmdHostGame(matchID);
//    }

//    [Command]

//    void CmdHostGame(string _matchID)
//    {
//        if (MatchMaker.instance.HostGame(_matchID, gameObject))
//        {
//            Debug.Log($"Game hosted successfully!");
//        }
//        else
//        {
//            Debug.Log($"Game hosted failed!");
//        }
//    }
   
//}
