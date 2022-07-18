using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using TMPro;

public class Ready : NetworkBehaviour
{
  
    public GameObject playerCamera;
    public int rayCastDist = 2;
    NetworkRoomPlayer networkRoomPlayer;

    public GameObject interactPrompt;
    
    public string roomScene;
   

    private void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        networkRoomPlayer = GetComponent<NetworkRoomPlayer>();

        interactPrompt = GameObject.Find("InteractPrompt");
        interactPrompt.GetComponent<TextMeshProUGUI>().enabled = false;
        
    }

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, rayCastDist))
        {
            if (hit.transform.tag == "ReadyButton")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    setReady();
                }
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

                interactPrompt.GetComponent<TextMeshProUGUI>().enabled = true;
            }
            else if (hit.transform.tag == "CustomizationButton")
            {
                interactPrompt.GetComponent<TextMeshProUGUI>().enabled = true;
            }
            else
            {
                interactPrompt.GetComponent<TextMeshProUGUI>().enabled = false;
            }
        }
        else
        {
            interactPrompt.GetComponent<TextMeshProUGUI>().enabled = false;
        }

        
    }

   

    //public void disableOnPlayers()
    //{
    //    //GameObject[] gamePlayers = GameObject.FindGameObjectsWithTag("RoomPlayer");

    //    //foreach (GameObject gamePlayer in gamePlayers)
    //    //{
    //    //    MonoBehaviour[] comps = gamePlayer.GetComponents<MonoBehaviour>();
    //    //    foreach (MonoBehaviour c in comps)
    //    //    {
    //    //        if (c.GetType() != typeof(NetworkRoomPlayer))
    //    //            c.enabled = false;

    //    //    }




    //    //    for (int i = 0; i < gamePlayer.transform.childCount; i++)
    //    //    {
    //    //        gamePlayer.transform.GetChild(i).gameObject.SetActive(false);
    //    //    }

    //    //}
    //    MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
    //    foreach (MonoBehaviour c in comps)
    //    {
    //        if (c.GetType() != typeof(NetworkRoomPlayer))
    //            c.enabled = false;

    //    }

    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        transform.GetChild(i).gameObject.SetActive(false);
    //    }
    //}

    private void setReady()
    {

        if (isLocalPlayer)
        {

            if (networkRoomPlayer.readyToBegin)
            {

                networkRoomPlayer.CmdChangeReadyState(false);
            }
            else
            {

                networkRoomPlayer.CmdChangeReadyState(true);
            }
        }
    }






  

    
  
}
