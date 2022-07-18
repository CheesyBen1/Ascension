using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILobby : MonoBehaviour
{

    [SerializeField] InputField joinMatchInput;
    [SerializeField] Button joinButton;
    [SerializeField] Button hostButton;


    public void Host()
    {
        joinMatchInput.interactable = false;
        joinButton.interactable = false;
        hostButton.interactable = false;

        //PlayerLobby.localPlayer.HostGame();
    }

    public void Join()
    {
        joinMatchInput.interactable = false;
        joinButton.interactable = false;
        hostButton.interactable = false;
    }
}
