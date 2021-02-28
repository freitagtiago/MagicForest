using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgain : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        SceneLoader.instance.LoadSceneNow(1);
    }
}
