using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPiece : MonoBehaviour
{
    [SerializeField] GameObject key;
    [SerializeField] bool canShow = false;
    [SerializeField] bool locked = false;

    void Update()
    {
        if (canShow && !locked)
        {
            key.SetActive(true);
        }
        else
        {
            key.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Mover>())
        {
            canShow = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Mover>())
        {
            canShow = false;
        }
    }

    public void Unlock()
    {
        locked = false;
    }
}


