using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RollingScore : NetworkBehaviour
{
    [SyncVar]
    public bool isGoal = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Goal")
        {
            isGoal = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Goal")
        {
            isGoal = false;
            
        }
    }

    
}
