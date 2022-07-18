using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Pause : MonoBehaviour
{
    public GameObject pauseCanvas;

    public bool isPaused = false;

    public NetworkManager networkManager;

    public Customization customization;

    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
        pauseCanvas.SetActive(false);
        pauseCanvas.GetComponent<Canvas>().enabled = true;

        networkManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>();

        customization = GameObject.FindGameObjectWithTag("Customization").GetComponent<Customization>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            pauseCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (!isPaused && customization.inMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        //if (isPaused && !customization.inMenu) 
        //{
        //    pauseCanvas.SetActive(true);
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}
        //if (isPaused && customization.inMenu)
        //{
        //    pauseCanvas.SetActive(true);
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}
        else
        {
            pauseCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void resume()
    {
        isPaused = false;
        if (customization.inMenu)
        {
            customization.inMenu = false;
        }
    }

    public void quit()
    {
        isPaused = false;
        customization.inMenu = false;
        // stop host if host mode
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            
                networkManager.StopHost();
            
            
        }
        // stop client if client-only
        else if (NetworkClient.isConnected)
        {
            
                networkManager.StopClient();
            
        }
    }
}
