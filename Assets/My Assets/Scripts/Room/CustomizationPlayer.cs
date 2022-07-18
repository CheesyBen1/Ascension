using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class CustomizationPlayer : NetworkBehaviour
{
    public Customization customization;

    public Material white;
    public Material black;
    public Material red;
    public Material green;
    public Material blue;
    public Material yellow;

    public Material colorMat;

    public GameObject wing1;
    public GameObject wing2;

    [SyncVar]
    public int color = 1;

    public GameObject playerCamera;
    public int rayCastDist = 2;

    public GameObject customizationCanvas;

    public bool inMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        customization = GameObject.FindGameObjectWithTag("Customization").GetComponent<Customization>();

        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

        

    }

    // Update is called once per frame
    void Update()
    {
        //customizationCanvas = GameObject.FindGameObjectWithTag("CustomizationCanvas");

        if (isLocalPlayer)
        {
            setColor(customization.color);
        }

        wing1.GetComponent<Renderer>().material = colorMat;
        wing2.GetComponent<Renderer>().material = colorMat;

        switchColor();

        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, rayCastDist))
        {
            if(hit.transform.tag == "CustomizationButton")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    
                    customization.inMenu = true;
                    //customization.inColor = true;
                    //GameObject.FindGameObjectWithTag("PauseManager").GetComponent<Pause>().isPaused = true;
                }
            }
        }

        inMenu = customization.inMenu;
        checkInMenu();

    }

    public void checkInMenu()
    {
        if (isLocalPlayer)
            GetComponent<PlayerInput>().enabled = !customization.inMenu;
    }

    public void switchColor()
    {
        switch (color)
        {
            case 1:
                colorMat = white;
                return;
            case 2:
                colorMat = black;
                return;
            case 3:
                colorMat = red;
                return;
            case 4:
                colorMat = green;
                return;
            case 5:
                colorMat = blue;
                return;
            case 6:
                colorMat = yellow;
                return;
            default:

                colorMat = white;
                return;
        }
    }

    [Command]
    public void setColor(int num)
    {
        color = num;
    }
}
