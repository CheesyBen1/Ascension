using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
public class EndRoundUpdate : NetworkBehaviour
{
    public string level1 = "1Rolling Inferno";
    public string level2 = "2King of The Hill";
    public string level3 = "3Sword Fight";
    public string level4 = "4Collapsing Platforms";

    public GameObject tracker;

    public bool creditted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer) {
            if (SceneManager.GetActiveScene().name == level1)
            {
                tracker = GameObject.FindGameObjectWithTag("Tracker");
                if (tracker.GetComponent<RollingTracker>().gameEnd)
                {
                    if(GetComponent<PlayerStats>().isDead ==false && creditted == false)
                    {
                        PlayerScoreTracker.track.roundCount++;
                        creditted = true;
                    }
                    else
                    {
                        creditted = true;
                    }
                }
            }
            else if (SceneManager.GetActiveScene().name == level2)
            {
                tracker = GameObject.FindGameObjectWithTag("Tracker");
                if (tracker.GetComponent<KOTHTracker>().gameEnd)
                {
                    if (GetComponent<PlayerStats>().isDead == false && creditted == false)
                    {
                        PlayerScoreTracker.track.roundCount++;
                        creditted = true;
                    }
                    else
                    {
                        creditted = true;
                    }
                }
            }
            else if (SceneManager.GetActiveScene().name == level3)
            {
                tracker = GameObject.FindGameObjectWithTag("Tracker");
                if (tracker.GetComponent<SwordTracker>().gameEnd)
                {
                    if (GetComponent<PlayerStats>().isDead == false && creditted == false)
                    {
                        PlayerScoreTracker.track.roundCount++;
                        creditted = true;
                    }
                    else
                    {
                        creditted = true;
                    }
                }
            }
            else if (SceneManager.GetActiveScene().name == level4)
            {
                tracker = GameObject.FindGameObjectWithTag("Tracker");
                if (tracker.GetComponent<PlatformTracker>().gameEnd)
                {
                    if (GetComponent<PlayerStats>().isDead == false && creditted == false)
                    {
                        PlayerScoreTracker.track.roundCount++;
                        creditted = true;
                    }
                    else
                    {
                        creditted = true;
                    }
                }
            } 
        }
    }
}
