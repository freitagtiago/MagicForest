using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Menu : MonoBehaviour
{
    bool isStartScene = false;
    bool isWinScene = false;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject levelInfoPanel;

    [SerializeField] TMP_Text levelNameText;
    [SerializeField] TMP_Text levelInfoText;
    [SerializeField] float timeToStartLevel = 3f;

    [SerializeField] GameObject progress;
    [SerializeField] GameObject gameSession;

    [SerializeField] List<string> levelName;
    [SerializeField] List<string> levelInfo;

    private void Awake()
    {
       
    }

    private void Start()
    {
        isStartScene = (SceneLoader.instance.GetCurrentActiveScene() == 0);
        isWinScene = (SceneLoader.instance.GetCurrentActiveScene() == 5);
        if (!isStartScene)
        {
            gameSession = FindObjectOfType<GameSession>()?.gameObject;
            progress = FindObjectOfType<ScenePersist>()?.gameObject;
        }

        if (isStartScene || isWinScene) return;

        StartCoroutine(ShowLevelInfo());
    }

    private void Update()
    {
        if (isStartScene) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuPanel.activeInHierarchy)
            {
                OpenMenu();
            }
            else
            {
                Resume();
            }
        }
    }

    private IEnumerator ShowLevelInfo()
    {
        Time.timeScale = 0;
        levelInfoPanel.SetActive(true);
        levelNameText.text = levelName[SceneLoader.instance.GetCurrentActiveScene() - 1];
        levelInfoText.text = levelInfo[SceneLoader.instance.GetCurrentActiveScene() - 1];
        yield return new WaitForSecondsRealtime(timeToStartLevel);
        levelInfoPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartFirstLevel() 
    {
        SceneLoader.instance.LoadSceneNow(1);
    }

    public void OpenMenu()
    {
        Time.timeScale = 0;
        menuPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        Destroy(progress);
        Destroy(gameSession);
        SceneLoader.instance.LoadSceneNow(SceneLoader.instance.GetCurrentActiveScene());
    }

    public void Resume()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        SceneLoader.instance.LoadSceneNow(0);
    }
}
