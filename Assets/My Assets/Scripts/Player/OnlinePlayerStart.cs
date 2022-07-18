using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using Cinemachine;

public class OnlinePlayerStart : NetworkBehaviour
{
    public GameObject playerModel;
    //public GameObject playerWing;
    

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        //playerModel.SetActive(false);
       // playerWing.SetActive(false);
        GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().Follow = transform.GetChild(1).transform;
        
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        PlayerInput playerInput = GetComponent<PlayerInput>();
        playerInput.enabled = true;
    }

   

}
