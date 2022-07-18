using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo playerInfo;

    public int apples = 0;

    private void OnEnable()
    {
        if (PlayerInfo.playerInfo == null)
        {
            PlayerInfo.playerInfo = this;
        }
        else
        {
            if (PlayerInfo.playerInfo != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public string playerID;
    public string playerName;

    public GetAccountInfoResult accountInfo;

    public int currentHat = 0;
    private void Update()
    {
        //PlayFabControl.PFControl.GetVirtualCurrencies();
    }


}
