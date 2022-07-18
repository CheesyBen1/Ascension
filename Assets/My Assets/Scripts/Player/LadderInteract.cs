using Mirror;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderInteract : NetworkBehaviour
{

    public Rigidbody rb;
    public PlayerMove pm;
    public FirstPersonController FPSController;

    
    public bool onLadder = false;
    
    public float ladderClimbSpeed = 1.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (gameObject.GetComponent<FirstPersonController>() == null)
        {
            pm = GetComponent<PlayerMove>();
        }
        else if(gameObject.GetComponent<PlayerMove>() == null)
        {
            FPSController = GetComponent<FirstPersonController>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(onLadder == true)
        {
            
            if (Input.GetKey("w")){
                transform.position += new Vector3(0, ladderClimbSpeed, 0);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.tag == "Ladder")
        {
            onLadder = true;

            if(pm == null)
            {
                FPSController.hasGravity = false;
            }
            else if(FPSController == null)
            {
                pm.hasGravity = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Ladder")
        {
            onLadder = true;

            if (pm == null)
            {
                FPSController.hasGravity = false;
            }
            else if (FPSController == null)
            {
                pm.hasGravity = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Ladder")
        {
            onLadder = false;

            if (pm == null)
            {
                FPSController.hasGravity = true;
            }
            else if (FPSController == null)
            {
                pm.hasGravity = true;
            }
        }
    }

    
}
