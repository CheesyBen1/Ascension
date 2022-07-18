using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    NetworkManager manager;

    public GameObject joinPanel;

    public string NetworkAddress = "";

    // Start is called before the first frame update

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        manager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>();
    }

    public void OnHost()
    {
        manager.StartHost();
    }

    public void OnJoinPanel()
    {
        joinPanel.SetActive(true);
    }

    public void OnCloseJoinPanel()
    {
        networkAddress.color = Color.black;
        joinPanel.SetActive(false);

        
    }

    public GameObject hatPanel;

    public void OnHatPanel()
    {
        hatPanel.SetActive(true);
        hatPanel.GetComponent<CustomizationHatPreview>().hatPreview = GameObject.FindGameObjectWithTag("CustomizationHat").GetComponent<CustomizationHat>().hat;
        hatPanel.GetComponent<CustomizationHatPreview>().onClickHat(GameObject.FindGameObjectWithTag("CustomizationHat").GetComponent<CustomizationHat>().hat);
    }
    
    public void OnCloseHatPanel()
    {
        hatPanel.SetActive(false);
    }

    public void OnJoin()
    {
        manager.networkAddress = NetworkAddress;
        manager.StartClient();

        StartCoroutine(wiggle());
    }

    public void SetNetworkAddress(string address)
    {
        NetworkAddress = address.ToUpper();
    }


    public TextMeshProUGUI networkAddress;

    IEnumerator wiggle()
    {

        

        yield return new WaitForSeconds(0.5f);

        networkAddress.color = Color.red;

        
    }

    public GameObject settingsPanel;
    public GameObject settingsCloseButton;

    public void OnSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void OnSettingsClose()
    {
        settingsPanel.SetActive(false);
    }

    public string loginScreen = "!1Login Register";
    public void Logout()
    {
        //PlayFabControl.PFControl.Logout();
        //SceneManager.LoadScene(loginScreen);

        Debug.Log("App quit!");
        Application.Quit();
        
    }

    public GameObject instructionsPanel;
    
    public void OnInstructionsPanel()
    {
        instructionsPanel.SetActive(true);

    }

    public void OnInstructionsClose()
    {
        instructionsPanel.SetActive(false);
    }

    
}
