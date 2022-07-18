using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlatformTracker : NetworkBehaviour
{
   

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
    public float levelTimer = 80;
    public float maxTime;

    

    public GameObject levelTimeText;
    public GameObject maxLevelTimeText;

    public int collapseAmount = 2;

    public float collapseInterval = 10f;

    public GameObject fakePlatforms;

    // Start is called before the first frame update
    void Start()
    {
        maxTime = levelTimer;
        fakePlatforms.SetActive(false);
        maxLevelTimeText.GetComponent<TextMeshProUGUI>().text = maxTime.ToString("F0");

        roomManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkRoomManager>();
    }

   
    // Update is called once per frame
    void Update()
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
                //roomManager.ServerChangeScene(nextScene);
                SceneManager.LoadScene(nextScene);
                //roomManager.StopHost();            
            }
            else
            {

                gameEndTimeOut -= Time.deltaTime;
            }

            EndGame();
        }

        if (gameStart)
        {
            Collapse();
            

            
                
                
            
        }

        SetSound();
    }

    public float collapseTimer = 0;

    [ServerCallback]
    public void Collapse()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        List<GameObject> checkPlatforms = new List<GameObject>();

        foreach(GameObject platform in platforms)
        {
            if(platform.GetComponent<Platform>().isCollapse == false)
            {
                checkPlatforms.Add(platform);
            }
        }
        Debug.Log(checkPlatforms.Count.ToString());
        if (collapseTimer > collapseInterval)
        {
            collapseTimer = 0;
            for (int i = 0; i < collapseAmount; i++)
            {
                int random = Random.Range(0, checkPlatforms.Count - 1);
                checkPlatforms[random].GetComponent<Platform>().isCollapse = true;
                checkPlatforms.RemoveAt(random);
            }
        }        
        else
        {
            collapseTimer += Time.deltaTime;
        }
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
