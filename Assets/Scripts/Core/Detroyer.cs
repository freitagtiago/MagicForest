using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detroyer : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 1f;
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

}
