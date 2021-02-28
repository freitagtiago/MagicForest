using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] bool moveHorizontal = true;
    [SerializeField] bool moveToFinalPos = true;
    [SerializeField] float speed = 2f;
    [SerializeField] Vector3 range;
    [SerializeField] Vector3 initialPos;
    [SerializeField] Vector3 finalPos;

    private void Awake()
    {
        initialPos = transform.position - range;
        finalPos = transform.position + range;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckDirection();
    }

    private void CheckDirection()
    {
        if (moveHorizontal)
        {
            if (transform.position.x <= initialPos.x)
            {
                moveToFinalPos = true;
            }
            if (transform.position.x >= finalPos.x)
            {
                moveToFinalPos = false;
            }
        }
        else
        {
            if (transform.position.y <= initialPos.y)
            {
                moveToFinalPos = true;
            }
            if (transform.position.y >= finalPos.y)
            {
                moveToFinalPos = false;
            }
        }
    }
    private void Move()
    {
        float step = speed * Time.fixedDeltaTime;

        if (moveToFinalPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPos, step);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPos, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Mover player = other.GetComponent<Mover>();
        if (player)
        {
            player.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Mover player = other.GetComponent<Mover>();
        if (player)
        {
            player.transform.parent = null;
        }
    }
}
