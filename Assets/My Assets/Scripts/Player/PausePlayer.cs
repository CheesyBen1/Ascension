using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PausePlayer : NetworkBehaviour
{
    public Pause pauseManager;

    // Start is called before the first frame update
    void Start()
    {
        pauseManager = GameObject.FindGameObjectWithTag("PauseManager").GetComponent<Pause>();    
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isLocalPlayer)
            GetComponent<PlayerInput>().enabled = !pauseManager.isPaused;
        
    }
}
