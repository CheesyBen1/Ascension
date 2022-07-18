using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System.Linq;

public class KOTHTracker : NetworkBehaviour
{
    public GameObject player1Name;
    public GameObject player2Name; 
    public GameObject player3Name;
    public GameObject player4Name;
    public GameObject player5Name;
    public GameObject player6Name;

    public GameObject player1Score;
    public GameObject player2Score;
    public GameObject player3Score;
    public GameObject player4Score;
    public GameObject player5Score;
    public GameObject player6Score;

    [SyncVar]
    public bool gameStart = false;

    [SyncVar]
    public bool start = false;

    [SyncVar]
    public bool gameEnd = false;


    [SyncVar]
    public float countDown = 5;

    public GameObject gameplayCanvas;
    public GameObject instructionsCanvas;

    public GameObject instructionsCountdown;
    public GameObject gameplayCountdown;

    public GameObject mainCamera;
    public GameObject cutsceneCamera;

    [SyncVar]
    public float gameEndTimeOut = 15;

    public GameObject gameEndTimeOutText;

    public NetworkRoomManager roomManager;
    public string nextScene;

    [SyncVar]
    public float levelTimer = 120;

   

    public GameObject levelTimeText;
    public GameObject maxLevelTimeText;

    // Start is called before the first frame update
    void Start()
    {
        player1Name.GetComponent<TextMeshProUGUI>().text = "";
        player2Name.GetComponent<TextMeshProUGUI>().text = "";
        player3Name.GetComponent<TextMeshProUGUI>().text = "";
        player4Name.GetComponent<TextMeshProUGUI>().text = "";
        player5Name.GetComponent<TextMeshProUGUI>().text = "";
        player6Name.GetComponent<TextMeshProUGUI>().text = "";

        player1Score.GetComponent<TextMeshProUGUI>().text = "";
        player2Score.GetComponent<TextMeshProUGUI>().text = "";
        player3Score.GetComponent<TextMeshProUGUI>().text = "";
        player4Score.GetComponent<TextMeshProUGUI>().text = "";
        player5Score.GetComponent<TextMeshProUGUI>().text = "";
        player6Score.GetComponent<TextMeshProUGUI>().text = "";

        maxLevelTimeText.GetComponent<TextMeshProUGUI>().text = levelTimer.ToString("F0");
        maxLevelTimeText.GetComponent<TextMeshProUGUI>().text = levelTimer.ToString("F0");

        roomManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkRoomManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> checkPlayer = new List<GameObject>();

        foreach(GameObject player in players)
        {
            if(!player.GetComponent<PlayerStats>().isDead)
            {
                checkPlayer.Add(player);
            }
        }

        updateLeaderBoard();

        if (countDown <= 0)
        {
            if (levelTimer <= 0 || checkPlayer.Count <= 1)
            {
                gameEnd = true;
            }
            else
            {
                levelTimer -= Time.deltaTime;
            }

            if (start == false)
            {
                gameStart = true;
                setPlayerWalls(false);
            }

            start = true;

            
            
        }
        else
        {
            countDown -= Time.deltaTime;
            setPlayerWalls(true);
            instructions();
        }

        levelTimeText.GetComponent<TextMeshProUGUI>().text = levelTimer.ToString("F1");

        if (gameEnd == true)
        {

            gameEndTimeOutText.SetActive(true);
            gameEndTimeOutText.GetComponent<TextMeshProUGUI>().text = gameEndTimeOut.ToString("F0");

           
            if (gameEndTimeOut <= 0)
            {
                roomManager.ServerChangeScene(nextScene);
            }
            else
            {
                gameEndTimeOut -= Time.deltaTime;
            }

            EndGame();
        }
    }

    
    public void updateLeaderBoard()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        GameObject[] placements = players.OrderByDescending(p => p.GetComponent<KOTHScore>().score).ToArray();



        //for(int i = 0; i < players.Length; i++)
        //{
        //    if (placements[0] == null)
        //    {
        //        placements[0] = players[i];
        //    }
        //    else
        //    {
        //        if (players[i].GetComponent<KOTHScore>().score > placements[0].GetComponent<KOTHScore>().score)
        //        {
        //            placements[1] = placements[0];
        //            placements[0] = players[i];
        //        }
        //        else
        //        {
        //            placements[1] = players[i];
        //        }
        //    }
        //}

        

        GameObject[] Name = { player1Name , player2Name, player3Name, player4Name, player5Name, player6Name};
        GameObject[] Score = { player1Score, player2Score, player3Score, player4Score, player5Score, player6Score };

        for (int i = 0; i < placements.Length; i++)
        {
            Name[i].GetComponent<TextMeshProUGUI>().text = placements[i].GetComponent<PlayerIdentity>().playerName;
            Score[i].GetComponent<TextMeshProUGUI>().text = placements[i].GetComponent<KOTHScore>().score.ToString("F1");

        }

        for (int i = 0; i < placements.Length / 2; i++)
        {
            
            Name[i].GetComponent<TextMeshProUGUI>().color = Color.yellow;
            Score[i].GetComponent<TextMeshProUGUI>().color = Color.yellow;
        }


        SetSound();

    }

    public void setPlayerWalls(bool set)
    {
        GameObject[] barriers = GameObject.FindGameObjectsWithTag("Barrier");

        foreach (GameObject barrier in barriers)
        {
            barrier.SetActive(set);
        }

    }

    public void instructions()
    {
        if (countDown > 5)
        {
            gameplayCanvas.GetComponent<Canvas>().enabled = false;
            instructionsCanvas.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            gameplayCanvas.GetComponent<Canvas>().enabled = true;
            instructionsCanvas.GetComponent<Canvas>().enabled = false;
            mainCamera.SetActive(true);
            cutsceneCamera.SetActive(false);
        }

        instructionsCountdown.GetComponent<TextMeshProUGUI>().text = countDown.ToString("F0");
        gameplayCountdown.GetComponent<TextMeshProUGUI>().text = countDown.ToString("F0");


        if (countDown <= 0)
        {
            gameplayCountdown.SetActive(false);
        }




    }

    [Command (requiresAuthority = false)]
    public void EndGame()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        GameObject[] placements = players.OrderBy(p => p.GetComponent<KOTHScore>().score).ToArray();

        

        for (int i = 0; i < placements.Length / 2; i++)
        {
            placements[i].GetComponent<PlayerStats>().health = 0;
            
        }
    }

    public AudioSource bgmAudio;
    public AudioSource sfxAudio;

    public AudioClip gameEndClip;
    public AudioClip readyClip;

    public bool bgmPlaying = false;

    public bool readyHorn = false;
    public bool endHorn = false;


    public void SetSound()
    {
        if (gameStart && !bgmPlaying)
        {
            bgmAudio.Play();
            bgmPlaying = true;
        }
        else if (gameEnd)
        {
            bgmAudio.Stop();

        }

        if (countDown <= 3 && !readyHorn)
        {
            sfxAudio.PlayOneShot(readyClip);
            readyHorn = true;
        }

        if (gameEnd && !endHorn)
        {
            sfxAudio.PlayOneShot(gameEndClip);
            endHorn = true;
        }
    }

}
