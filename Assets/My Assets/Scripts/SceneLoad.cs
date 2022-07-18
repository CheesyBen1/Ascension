using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public static SceneLoad SceneLoader;

    public string menuScene = "!2Menu";
    
    public string testScene;

    private void OnEnable()
    {
        if (SceneLoad.SceneLoader == null)
        {
            SceneLoad.SceneLoader = this;
        }
        else
        {
            if (SceneLoad.SceneLoader != this)
            {
                Destroy(this.gameObject);
            }
        }
        
    }

    public void loadMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void loadTesting()
    {
        SceneManager.LoadScene(testScene);
    }
}
