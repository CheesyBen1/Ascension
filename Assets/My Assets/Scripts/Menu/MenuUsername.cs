using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuUsername : MonoBehaviour
{
    private TextMeshProUGUI username;

    // Start is called before the first frame update
    void Start()
    {
        username = GetComponent<TextMeshProUGUI>();
        username.text = PlayerInfo.playerInfo.accountInfo.AccountInfo.Username;
    }

    
}
