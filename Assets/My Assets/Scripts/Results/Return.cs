using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    public string menu = "!2Menu";
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(menu);
        PlayerScoreTracker.track.DestroyThis();
        Results.track.DestroyThis();
        PlayFabControl.PFControl.GetVirtualCurrencies();
    }
}
