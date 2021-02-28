using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
	[SerializeField] float range = 1f;
	[SerializeField] bool isPushing = false;
	GameObject box;

    void Update()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, range, LayerMask.GetMask("Walkable"));

		if (hit.collider != null && hit.collider.GetComponent<PushableBox>())
		{
			if(Input.GetKeyDown(KeyCode.M))
            {
				if (isPushing)
				{
					isPushing = false;
					box.GetComponent<FixedJoint2D>().enabled = false;
					box.GetComponent<PushableBox>().beingPushed = false;
				}
				else
				{
					isPushing = true;
					box = hit.collider.gameObject;
					box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
					box.GetComponent<FixedJoint2D>().enabled = true;
					box.GetComponent<PushableBox>().beingPushed = true;
				}			
			}
		}
		else if (Input.GetKeyUp(KeyCode.M))
		{
            if (!box) { return; }

			box.GetComponent<FixedJoint2D>().enabled = false;
			box.GetComponent<PushableBox>().beingPushed = false;
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * range);
	}
}
