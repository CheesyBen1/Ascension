using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;


public class PlayFabController : MonoBehaviour
{
    public static PlayFabController PFC;

    

    private void OnEnable()
    {
        if(PlayFabController.PFC == null)
        {
            PlayFabController.PFC = this;
        }
        else
        {
            if (PlayFabController.PFC != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private string userEmail;
    private string userPassword;
    private string userName;

    public GameObject loginPanel;

    public void Start()
    {
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "E9C91"; // Please change this value to your own titleId from PlayFab Game Manager
        }
        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

        PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("EMAIL"))
        {
            userEmail = PlayerPrefs.GetString("EMAIL");
            userPassword = PlayerPrefs.GetString("PASSWORD");
            var request = new LoginWithEmailAddressRequest
            {
                Email = userEmail,
                Password = userPassword
            };

            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }       
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login success!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        loginPanel.SetActive(false);
        GetStats();
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Registered successfully!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        loginPanel.SetActive(false);
        GetStats();
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    private void OnLoginFailure(PlayFabError error)
    {
        //Debug.LogWarning("Something went wrong with your first API call.  :(");
        //Debug.LogError("Here's some debug information:");
        //Debug.LogError(error.GenerateErrorReport());

        var registerRequest = new RegisterPlayFabUserRequest
        {
            Email = userEmail,
            Password = userPassword,
            Username = userName
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest,OnRegisterSuccess,OnRegisterFailure);
    }

    public void GetUserEmail(string emailIn)
    {
        userEmail = emailIn;
    }

    public void GetUserPassword(string passwordIn)
    {
        userPassword = passwordIn;
    }

    public void GetUserName(string nameIn)
    {
        userName = nameIn;
    }

    public void OnClickLogin()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = userEmail,
            Password = userPassword
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

 

    public Vector3 playerPos;

    public int playerPosX;
    public int playerPosY;
    public int playerPosZ;

    #region PlayerStats

    public void SetStats()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
        new StatisticUpdate { StatisticName = "playerPosX", Value = playerPosX},
        new StatisticUpdate { StatisticName = "playerPosY", Value = playerPosY},
        new StatisticUpdate { StatisticName = "playerPosZ", Value = playerPosZ},
            }
        },
        result => { Debug.Log("User statistics updated"); },
        error => { Debug.LogError(error.GenerateErrorReport()); });
    }

    void GetStats()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStats,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    void OnGetStats(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
            switch (eachStat.StatisticName)
            {
                case "playerPosX":
                    playerPosX = eachStat.Value;
                    break;
                case "playerPosY":
                    playerPosY = eachStat.Value;
                    break;
                case "playerPosZ":
                    playerPosZ = eachStat.Value;
                    break;

            }
        }
    }

    #endregion PlayerStats
}
