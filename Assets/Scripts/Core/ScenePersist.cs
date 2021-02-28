using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    int startingSceneIndex;
    private void Awake()
    {
        int sceneCount = FindObjectsOfType<ScenePersist>().Length;
        if (sceneCount > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        startingSceneIndex = SceneLoader.instance.GetCurrentActiveScene();
    }

    private void Update()
    {
        int currentSceneIndex = SceneLoader.instance.GetCurrentActiveScene();

        if (currentSceneIndex != startingSceneIndex)
        {
            Destroy(gameObject);
        }
    }
}
