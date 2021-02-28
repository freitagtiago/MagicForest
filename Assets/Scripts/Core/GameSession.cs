using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int currentLives;
    [SerializeField] int coins;
    [SerializeField] UIHandler uiHandler;
    private void Awake()
    {
        int countGameSession = FindObjectsOfType<GameSession>().Length;

        if(countGameSession > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        uiHandler = FindObjectOfType<UIHandler>();
    }

    private void Start()
    {
        UpdateDisplays();
    }

    private void TakeLife()
    {
        --currentLives;
        StartCoroutine(ReloadCurrentScene());
    }

    private IEnumerator ReloadCurrentScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneLoader.instance.LoadSceneNow(SceneLoader.instance.GetCurrentActiveScene());
    }

    public void UpdateDisplays()
    {
        uiHandler.UpdateCoins(coins);
        uiHandler.UpdateLives(currentLives);
    }

    private void ResetGameSession()
    {
        SceneLoader.instance.LoadSceneNow(0);
        Destroy(this.gameObject);
    }

    public void ProcessPlayerDeath()
    {
        if(currentLives > 1)
        {
            TakeLife();
            uiHandler.UpdateLives(currentLives);
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddCoins()
    {
        coins++;
        uiHandler.UpdateCoins(coins);
    }

    public int GetCoins()
    {
        return coins;
    }

    public int GetLives()
    {
        return currentLives;
    }

    public void SetUIHandler(UIHandler ui)
    {
        uiHandler = ui;
        UpdateDisplays();
    }

}
