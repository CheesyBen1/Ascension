using Mirror;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Results : MonoBehaviour
{

    public static Results track;
    private void OnEnable()
    {
        if (Results.track == null)
        {
            Results.track = this;
        }
        else
        {
            if (Results.track != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public GameObject roundText;
    public GameObject roundText2;

    public GameObject basicText;

    public GameObject totalText;

    public int roundCount;
    public int roundCount2;

    public int perRound = 20;

    public int basic = 50;

    public int total = 0;

    public bool awarded = false;

    public string menu = "!2Menu";

    public string results = "5End Screen";


    // Start is called before the first frame update
    void Start()
    {
        roundCount = PlayerScoreTracker.track.roundCount;

        NetworkRoomManager roomManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkRoomManager>();

        roomManager.StopHost();
        SceneManager.LoadScene(results);
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        roundCount2 = roundCount * perRound;
        total = roundCount2 + basic;

        roundText = GameObject.Find("RoundCount");
        roundText2 = GameObject.Find("RoundCount2");
        basicText = GameObject.Find("Basic");
        totalText = GameObject.Find("Total");

        if (roundText != null && roundText2 != null && basicText != null && totalText != null)
        {
            roundText.GetComponent<TextMeshProUGUI>().text = roundCount.ToString();
            
            roundText2.GetComponent<TextMeshProUGUI>().text = roundCount2.ToString();
            basicText.GetComponent<TextMeshProUGUI>().text = basic.ToString();
            totalText.GetComponent<TextMeshProUGUI>().text = total.ToString();

        }

        

        if (awarded == false)
        {
            AddApples(total);
            awarded = true;
            
        }

        
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);

    }

    public void AddApples(int total)
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "GA",
            Amount = total
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, onAddSuccess, onError);
    }

    public void onAddSuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Added total: "+ total);
      
    }

    public void onError(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage);
    }

    public void Continue()
    {
        SceneManager.LoadScene(menu);
        
    }
}
