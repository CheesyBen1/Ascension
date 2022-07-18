using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class KOTHScore : NetworkBehaviour
{
    [SyncVar]
    public float score = 0f;

    KOTHTracker tracker;

    public AudioSource sfx;
    public AudioClip goalSfx;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "2King of The Hill")
        tracker = GameObject.Find("KOTHTracker").GetComponent<KOTHTracker>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (tracker != null)
        {
            if (tracker.gameStart == true && tracker.gameEnd == false)
            {
                if (other.transform.tag == "Hill")
                {
                    score += Time.deltaTime;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isLocalPlayer)
        {
            if (other.transform.tag == "Hill")
            {
                sfx.clip = goalSfx;
                sfx.loop = true;
                sfx.Play();
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isLocalPlayer)
        {
            if (other.transform.tag == "Hill")
            {
                sfx.loop = false;
                sfx.Stop();
            }
            
        }
    }
}
