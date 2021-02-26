using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            FindObjectOfType<Lock>().Collect();
            Destroy(this.gameObject);
        }
    }
}
