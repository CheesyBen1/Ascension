using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScoreTracker : MonoBehaviour
{
    public static PlayerScoreTracker track;
    private void OnEnable()
    {
        if (PlayerScoreTracker.track == null)
        {
            PlayerScoreTracker.track = this;
        }
        else
        {
            if (PlayerScoreTracker.track != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public int roundCount = 0;
    

    public string menuScene = "!2Menu";

    // Update is called once per frame
    void Update()
    {
        //if(SceneManager.GetActiveScene().name == menuScene)
        //{
        //    Destroy(this.gameObject);
        //}
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
        
    }
}
