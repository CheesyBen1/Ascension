using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class DeathCamera : NetworkBehaviour
{
    public bool deadCamera = false;

    public int followIndex = 0;

    
    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            deadCamera = GetComponent<PlayerStats>().deadCamera;

            spectate();

        }
    }

    

    public void spectate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> playersCheck = new List<GameObject>();

        foreach(GameObject player in players)
        {
            if(player.GetComponent<PlayerStats>().isDead != true)
            {
                playersCheck.Add(player);
            }
        }

        if (deadCamera)
        {
            if(playersCheck.Count > 0)
            {
                GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().Follow = playersCheck[followIndex].transform.Find("DeathCameraRoot");

                if (Input.GetButtonDown("Fire1"))
                {
                    followIndex++;
                }
                else if (Input.GetButtonDown("Fire2"))
                {
                    followIndex--;
                }

                followIndex = Mathf.Abs(followIndex);
                followIndex = followIndex % playersCheck.Count;
            }

            
        }
    }
}
