using PlayFab;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using PlayFab.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientStartup : MonoBehaviour
{

    public NetworkConfig NetworkConfig;
    public bool testServer = false;

    // Start is called before the first frame update
    void Start()
    {
        if (testServer)
        {
            LoginWithCustomIDRequest request = new LoginWithCustomIDRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                CreateAccount = true,
                CustomId = SystemInfo.deviceUniqueIdentifier
            };

            PlayFabClientAPI.LoginWithCustomID(request, OnPlayFabLoginSuccess, OnLoginError);
        }
    }

    

    void OnPlayFabLoginSuccess (LoginResult loginResult)
    {
        Debug.Log("Login Sucesss!");

       
        RequestMultiplayerServer();
        
    }

    private void RequestMultiplayerServer()
    {
        Debug.Log("[ClientStartup].RequestMultiplayer");
        RequestMultiplayerServerRequest requestData = new RequestMultiplayerServerRequest
        {
            BuildId = NetworkConfig.buildId,
            SessionId = "031a66e2-86e9-45dd-a717-c600621fb042",
            PreferredRegions = new List<string> { NetworkConfig.region }
        };

        PlayFabMultiplayerAPI.RequestMultiplayerServer(requestData, OnRequestMultiplayerServer, OnRequestMultiplayerServerError);
    }

    void OnRequestMultiplayerServer(RequestMultiplayerServerResponse response)
    {
        if (response == null) return;

        Debug.Log("**** THESE ARE YOUR DETAILS **** -- IP: " + response.IPV4Address + " Port: " + (ushort)response.Ports[0].Num);

        UnityNetworkServer.Instance.networkAddress = response.IPV4Address;
        UnityNetworkServer.Instance.GetComponent<kcp2k.KcpTransport>().Port = (ushort)response.Ports[0].Num;

        UnityNetworkServer.Instance.StartClient();

    }

    void OnRequestMultiplayerServerError(PlayFabError playFabError)
    {
        Debug.Log("An error has occoured, cannot connect to server!");
    }

    void OnLoginError(PlayFabError playFabError)
    {
        Debug.Log("Login Failed!");
    }
  
}
