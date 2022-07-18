using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueTest : NetworkBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Players;

    [SyncVar]
    public float value1 = 0;

    
    private void Update()
    {
        value1 += Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.Q))
        {
            ResetValue();
            //// Players = GameObject.FindGameObjectsWithTag("Player");
            //foreach (GameObject player in Players)
            //{
                
            //}
            
        }
        if (value1 > 10)
            ResetValue();
    }

    [Command(requiresAuthority =false)]
    public void ResetValue()
    {
       
        value1 = 0;
        
    }

    

}
