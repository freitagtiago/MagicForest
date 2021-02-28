using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBox : MonoBehaviour
{
	float defaultMass = 1f;
	[SerializeField] float imovableMass = 1000;
	[SerializeField] public bool beingPushed;
	float xPos;

	public Vector3 lastPos;

	void Start()
	{
		xPos = transform.position.x;
		lastPos = transform.position;
	}

	void FixedUpdate()
	{
		if (beingPushed == false)
		{
			GetComponent<Rigidbody2D>().mass = imovableMass;
		}
		else
		{
			GetComponent<Rigidbody2D>().mass = defaultMass;
		}
	}
}
