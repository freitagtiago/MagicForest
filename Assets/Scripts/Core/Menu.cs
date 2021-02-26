using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Menu : MonoBehaviour
{
    bool isStartScene = false;
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
        isStartScene = (SceneManager.GetActiveScene().buildIndex == 0);
        if (!isStartScene)
        {
            gameSession = FindObjectOfType<GameSession>().gameObject;
            progress = FindObjectOfType<ScenePersist>().gameObject;
        }
    }

    private void Start()
    {
        if (isStartScene) return;

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
        levelNameText.text = levelName[SceneManager.GetActiveScene().buildIndex - 1];
        levelInfoText.text = levelInfo[SceneManager.GetActiveScene().buildIndex - 1];
        yield return new WaitForSecondsRealtime(timeToStartLevel);
        levelInfoPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartFirstLevel() 
    {
        SceneManager.LoadScene(1);
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
        SceneManager.LoadScene(1);
    }

    public void Resume()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
