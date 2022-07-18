using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Apples : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI appleText;

   

    void Update()
    {
        appleText.text = PlayerInfo.playerInfo.apples.ToString("F0");
    }
}
