using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.InputSystem;
using StarterAssets;

public class RollingTracker : NetworkBehaviour
{
    

    [SyncVar]
    public bool start = false;

    [SyncVar]
    public bool gameStart = false;

    [SyncVar]
    public bool gameEnd = false;

    [SyncVar]
    public float gameEndTimeOut = 15;

    public GameObject gameEndTimeOutText;

    public bool sceneSwitch = false;

    public string testSwitch = "";

    [SyncVar]
    public float levelTimer = 0f;

    [SyncVar]
    public float maxTime = 180;

    public Transform lava;

    public float lavaRiseTime = 60;
    public float lavaRiseSpeed = 5;
    public float maxLavaRise = -28.1f;

    [SyncVar]
    public int playerCount = 0;

    [SyncVar]
    public int playerInGoal = 0;

    public GameObject goal;

    public NetworkRoomManager roomManager;

    [SyncVar]
    public float countDown = 15;

    public GameObject gameplayCanvas;
    public GameObject instructionsCanvas;

    public GameObject instructionsCountdown;
    public GameObject gameplayCountdown;

    public GameObject mainCamera;
    public GameObject cutsceneCamera;

    private void Start()
    {
        
        //lava = GameObject.FindGameObjectWithTag("Lava").transform;
        goal = GameObject.FindGameObjectWithTag("Goal");
       roomManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkRoomManager>();
    }

    private void Update()
    {

        
        if (countDown <= 0)
        {
            
            if(start == false)
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

        gameEndSet();

        checkGoal();

        startGame();

        if(gameEnd == true)
        {

            gameEndTimeOutText.SetActive(true);
            gameEndTimeOutText.GetComponent<TextMeshProUGUI>().text = gameEndTimeOut.ToString("F0");

           // gameEndTimeOut -= Time.deltaTime;

            if(gameEndTimeOut <= 0)
            {
                switchScene(testSwitch);
            }
            else
            {
                gameEndTimeOut -= Time.deltaTime;
            }
        }

        SetSound();
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

    public void setPlayerWalls(bool set)
    {
        GameObject[] barriers = GameObject.FindGameObjectsWithTag("Barrier");

        foreach(GameObject barrier in barriers)
        {
            barrier.SetActive(set);
        }
        
    }

    
    private void switchScene(string sceneName)
    {
        roomManager.ServerChangeScene(sceneName);

        //roomManager.ServerChangeScene(roomManager.RoomScene);
    }



    [ServerCallback]
    public void startGame()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> checkPlayer = new List<GameObject>();

        foreach (GameObject player in players)
        {
            if (!player.GetComponent<PlayerStats>().isDead)
            {
                checkPlayer.Add(player);
            }
        }

        if (gameStart)
        {
            levelTimer += Time.deltaTime;
            if (levelTimer > lavaRiseTime && lava.position.y < maxLavaRise)
            {
                lava.position += new Vector3(0, lavaRiseSpeed, 0);
            }

        }

        if (gameStart)
        {
            GameObject[] playersInitial = GameObject.FindGameObjectsWithTag("Player");

            int maxCounter =0 ;
            foreach(GameObject player in playersInitial)
            {
               if( player.GetComponent<PlayerStats>().isDead == false)
                {
                    maxCounter++;
                }
            }

            playerCount = maxCounter;
            
        }

        if (gameStart)
        {
            if ((playerInGoal >= playerCount - 1)|| (levelTimer > maxTime) || checkPlayer.Count <= 1)
            {
                gameStart = false;
                gameEnd = true;
            }

            
        }
    }


    
    public void checkGoal()
    {
       
        updateInGoal();
    }

    
    public void updateInGoal()
    {
        GameObject[] goalPlayers = GameObject.FindGameObjectsWithTag("Player");
        int count = 0;
        foreach (GameObject goalPlayer in goalPlayers)
        {
            if (goalPlayer.GetComponent<RollingScore>().isGoal == true)
            {
                count++;
            }
        }
        playerInGoal = count;
    }

    IEnumerator getPlayerCount()
    {
        playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        yield return null;
    }


    [Command (requiresAuthority = false)]
    public void gameEndSet()
    {
        if (gameEnd)
        {
            GameObject[] goalPlayers = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject goalPlayer in goalPlayers)
            {
                if (playerCount > 1)
                {
                    if (goalPlayer.GetComponent<RollingScore>().isGoal == false)
                    {
                        goalPlayer.GetComponent<PlayerStats>().health = 0;
                    }
                }
            }
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
        }else if (gameEnd)
        {
            bgmAudio.Stop();
            
        }

        if(countDown <= 3 && !readyHorn)
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
