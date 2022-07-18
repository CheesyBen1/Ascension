using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Customization : MonoBehaviour
{
    public int color = 1;

    //public Material white;
    //public Material black;
    //public Material red;
    //public Material green;
    //public Material blue;
    //public Material yellow;

    public static Customization customization;
    public GameObject customizationCanvas;

    public bool inMenu = false;
    //public bool inColor = false;

    private void Start()
    {
        customizationCanvas = GameObject.FindGameObjectWithTag("CustomizationCanvas");
        customizationCanvas.GetComponent<Canvas>().enabled = true;
        customizationCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        if (Customization.customization == null)
        {
            Customization.customization = this;
        }
        else
        {
            if (Customization.customization != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (inMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            if(customizationCanvas != null)
            customizationCanvas.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if(customizationCanvas != null)
            customizationCanvas.SetActive(false);
        }

        
    }

    public void setColorInt(int num)
    {
        color = num;
    }

    public void closeMenu()
    {
        inMenu = false;
        customizationCanvas.SetActive(false);
      //GameObject.FindGameObjectWithTag("PauseManager").GetComponent<Pause>().isPaused = false;
    }
}
