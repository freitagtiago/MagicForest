using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    MusicPlayer musicPlayer;

    private void Awake()
    {
        if(FindObjectsOfType<SceneLoader>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        musicPlayer = FindObjectOfType<MusicPlayer>();
    }

    public void LoadSceneNow(int sceneIndex)
    {
        if(sceneIndex == 0)
        {
            GameSession gs = FindObjectOfType<GameSession>();
            if (gs)
            {
                Destroy(gs);
            }
        }
        if(sceneIndex > GetCurrentActiveScene())
        {
            ScenePersist sp = FindObjectOfType<ScenePersist>();
            if (sp)
            {
                Destroy(sp.gameObject);
            }
        }
        SceneManager.LoadScene(sceneIndex);
        MusicPlayer.instance.PlayMusic(sceneIndex);
    }

    public int GetCurrentActiveScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
