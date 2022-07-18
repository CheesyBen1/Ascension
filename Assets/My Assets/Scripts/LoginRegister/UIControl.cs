using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIControl : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject registerPanel;

    public GameObject errorTextObject;

    public void OnRegisterPage()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
        errorTextObject.SetActive(false);

    }

    public void OnLoginPage()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
        errorTextObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("App quit!");
        Application.Quit();
    }
}
