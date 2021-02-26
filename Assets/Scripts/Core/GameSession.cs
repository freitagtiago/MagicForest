using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int currentLives;

    [SerializeField] int coins;
    UIHandler uiHandler;
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
        uiHandler.UpdateCoins(coins);
        uiHandler.UpdateLives(currentLives);
    }

    private void TakeLife()
    {
        --currentLives;
        StartCoroutine(ReloadCurrentScene());
    }

    private IEnumerator ReloadCurrentScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
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
}
