using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    private MusicPlayer _musicPlayer;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        _musicPlayer = FindObjectOfType<MusicPlayer>();
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
        MusicPlayer.Instance.PlayMusic(sceneIndex);
    }

    public int GetCurrentActiveScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
