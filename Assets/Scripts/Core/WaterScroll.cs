using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScroll : MonoBehaviour
{
    [Tooltip("Game units/seconds")]
    [SerializeField] float scrollRate = 0.2f;
    

    void Update()
    {
        float yMove = scrollRate * Time.fixedDeltaTime;
        transform.Translate(new Vector2(0f, yMove));
    }
}
