using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System;
using UnityEngine.UI;

public class PlayFabControl : MonoBehaviour
{
    public static PlayFabControl PFControl;
  

    private void OnEnable()
    {
        if(PlayFabControl.PFControl == null)
        {
            PlayFabControl.PFControl = this;
        }
        else
        {
            if (PlayFabControl.PFControl != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private string userEmail;
    private string userPassword;
    private string userName;

    public GameObject errorTextObject;
    public TextMeshProUGUI errorText;

   

    private void Start()
    {
        errorTextObject = GameObject.Find("ErrorText");
        errorText = errorTextObject.GetComponent<TextMeshProUGUI>();
        errorTextObject.SetActive(false);
    }

    public void OnTest1()
    {
        userEmail = "testing@email.com";
        userPassword = "123456";
        OnClickLogin();
    }

    public void OnTest2()
    {
        userEmail = "testing2@email.com";
        userPassword = "123456";
        OnClickLogin();
    }

    public void OnClickLogin()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = userEmail,
            Password = userPassword
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);

        var buttons = FindObjectsOfType<Button>(); 
        foreach(var button in buttons)
        {
            button.interactable = false;
        }

        
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login Success!");

        errorText.text = "Login Success!";
        errorTextObject.SetActive(true);

        GetVirtualCurrencies();
        GetAccountInfo(result.PlayFabId);

        //var buttons = FindObjectsOfType<Button>();
        //foreach (var button in buttons)
        //{
        //    button.interactable = true;
        //}
        SceneLoad.SceneLoader.loadMenu();
    }

  

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("Login Failure!");
        errorTextObject.SetActive(true);
        errorText.text = error.ErrorMessage;

        var buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
    }

    private void GetAccountInfo(string ID)
    {
        var request = new GetAccountInfoRequest
        {
            PlayFabId = ID
        };

        PlayFabClientAPI.GetAccountInfo(request, OnGetInfoSuccess, OnGetInfoFailure);
    }

    private void OnGetInfoSuccess(GetAccountInfoResult result)
    {
        Debug.Log("Get info success!");
       
        PlayerInfo.playerInfo.accountInfo = result;

        getEquipped();
        
    }

    private void OnGetInfoFailure(PlayFabError error)
    {
        Debug.Log("Get Info failed!");
    }

    public void GetVirtualCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnGetUserInventoryFailure);
    }

    public void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        PlayerInfo.playerInfo.apples = result.VirtualCurrency["GA"];

    }

    public void OnGetUserInventoryFailure(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage);
    }

    public void OnClickRegister()
    {
        var registerRequest = new RegisterPlayFabUserRequest
        {
            Email = userEmail,
            Password = userPassword,
            Username = userName
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);

        var buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.interactable = false;
        }

    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Register Success!");

        errorText.text = "Register Success!";
        errorTextObject.SetActive(true);

        var buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.interactable = true;
        }

    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.Log("Register Failure!");
        errorText.text = error.ErrorMessage;
        errorTextObject.SetActive(true);

        var buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
    }

    public void OnClickReset()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = userEmail,
            TitleId = "E9C91"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnResetError);

        var buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
    }

    private void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        Debug.Log("Reset success!");

        errorText.text = "Reset email sent!";
        errorTextObject.SetActive(true);

        var buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
    }

    private void OnResetError(PlayFabError error)
    {
        Debug.Log("Reset fail!");

        errorText.text = error.ErrorMessage;
        errorTextObject.SetActive(true);

        var buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
    }

    public void GetUserEmail(string emailIn)
    {
        userEmail = emailIn;
    }    
    public void GetUserName(string nameIn)
    {
        userName = nameIn;
    }    
    public void GetUserPassword(string passwordIn)
    {
        userPassword = passwordIn;
    }

    public void saveBuy(int num)
    {
        string currentHat = "Hat" + num.ToString();
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {

                { currentHat, true.ToString() }
            }

        };
        PlayFabClientAPI.UpdateUserData( request, OnDataSend,OnError );
    }

    public void saveEquip(int num)
    {
        PlayerInfo.playerInfo.currentHat = num;
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {

                { "CurrentHat", num.ToString() }
            }

        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    public void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Server saved!");
    }

    public int getKey = 0;
    public string returnKey = "False";
    public string getBuy(int num)
    {
        getKey = num;

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnHatDataRecieved, OnError);

        return returnKey;
    }

    public void OnHatDataRecieved(GetUserDataResult result)
    {
        string hat = "Hat" + getKey;

        
        if (result.Data != null && result.Data.ContainsKey(hat))
        {
            Debug.Log("Data recieved!" + hat + ": " + result.Data[hat].Value);
            returnKey = result.Data[hat].Value;
        }
        else
        {
            Debug.Log("Data not recieved!" + hat + "no data or false!" );
            returnKey = "False";
        }
    }

    public void getEquipped()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnEquippedDataRecieved, OnError);
    }

    public void OnEquippedDataRecieved(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("CurrentHat"))
        {
            Debug.Log("Data recieved! "  + result.Data["CurrentHat"].Value);
            
            PlayerInfo.playerInfo.currentHat = int.Parse(result.Data["CurrentHat"].Value);
        }
        else
        {
            saveEquip(0);
        }
    }


    public void OnError(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage);
    }

    public void Logout()
    {
        PlayFabClientAPI.ForgetAllCredentials();
    }

}
