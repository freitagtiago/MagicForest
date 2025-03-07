using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
	[SerializeField] private float _range = 1f;
	[SerializeField] private bool _isPushing = false;
	private GameObject _pushableObject;

    void Update()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, _range, LayerMask.GetMask("Walkable"));

		if (hit.collider != null && hit.collider.GetComponent<PushableBox>())
		{
			if(Input.GetKeyDown(KeyCode.M))
            {
				if (_isPushing)
				{
					_isPushing = false;
					_pushableObject.GetComponent<FixedJoint2D>().enabled = false;
					_pushableObject.GetComponent<PushableBox>().beingPushed = false;
				}
				else
				{
					_isPushing = true;
					_pushableObject = hit.collider.gameObject;
					_pushableObject.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
					_pushableObject.GetComponent<FixedJoint2D>().enabled = true;
					_pushableObject.GetComponent<PushableBox>().beingPushed = true;
				}			
			}
		}
		else if (Input.GetKeyUp(KeyCode.M))
		{
            if (!_pushableObject) 
			{ 
				return; 
			}

			_pushableObject.GetComponent<FixedJoint2D>().enabled = false;
			_pushableObject.GetComponent<PushableBox>().beingPushed = false;
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * _range);
	}
}
