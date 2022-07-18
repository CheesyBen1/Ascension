using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class CustomizationPlayerHat : NetworkBehaviour
{
    public CustomizationHat customizationHat;

    //public Material white;
    //public Material black;
    //public Material red;
    //public Material green;
    //public Material blue;
    //public Material yellow;

    public GameObject hat0;
    public GameObject hat1;
    public GameObject hat2;
    public GameObject hat3;
    public GameObject hat4;
    public GameObject hat5;
    public GameObject hat6;
    public GameObject hat7;
    public GameObject hat8;
    public GameObject hat9;
    public GameObject hat10;
    public GameObject hat11;

    List<GameObject> hats = new List<GameObject>();


    [SyncVar]
    public int hat = 0;

    public GameObject playerCamera;
    public int rayCastDist = 2;

    public GameObject customizationCanvas;

    public bool inMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        customizationHat = GameObject.FindGameObjectWithTag("CustomizationHat").GetComponent<CustomizationHat>();

        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

        // hats = { hat1, hat2, hat3, hat4, hat5, hat6, hat7, hat8, hat9, hat10, hat11 };

        hats.Add(hat0);
        hats.Add(hat1);
        hats.Add(hat2);
        hats.Add(hat3);
        hats.Add(hat4);
        hats.Add(hat5);
        hats.Add(hat6);
        hats.Add(hat7);
        hats.Add(hat8);
        hats.Add(hat9);
        hats.Add(hat10);
        hats.Add(hat11);

    }

    // Update is called once per frame
    void Update()
    {
        //customizationCanvas = GameObject.FindGameObjectWithTag("CustomizationCanvas");

        if (isLocalPlayer)
        {
            setHat(customizationHat.hat);
        }

        //wing1.GetComponent<Renderer>().material = colorMat;
        //wing2.GetComponent<Renderer>().material = colorMat;

        switchHat(hat);

        //RaycastHit hit;

        //if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, rayCastDist))
        //{
        //    if(hit.transform.tag == "CustomizationButton")
        //    {
        //        if (Input.GetButtonDown("Interact"))
        //        {
                    
        //            customization.inMenu = true;
        //            //GameObject.FindGameObjectWithTag("PauseManager").GetComponent<Pause>().isPaused = true;
        //        }
        //    }
        //}

        //inMenu = customization.inMenu;
        //checkInMenu();

    }

    //public void checkInMenu()
    //{
    //    if (isLocalPlayer)
    //        GetComponent<PlayerInput>().enabled = !customization.inMenu;
    //}

    public void switchHat(int num)
    {
       //foreach(Game hat in hats)
       // {
       //     if (hats[num] = hat)
       //     {
       //         hat.SetActive(true);
       //     }
       //     else
       //     {
       //         hat.SetActive(false);
       //     }
       // }

       for(int i=0; i<hats.Count; i++)
        {
            if(i == num)
            {
                hats[i].SetActive(true);
            }
            else
            {
                hats[i].SetActive(false);
            }
        }
    }

    [Command]
    public void setHat(int num)
    {
       hat = num;
    }
}
