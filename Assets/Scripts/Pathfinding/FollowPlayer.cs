using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPlayer : MonoBehaviour {

	public Pathfinding _Pathfinding;
	public Grid _grid;
	public Transform target;
	Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		_Pathfinding.seeker = transform;

	}
	
	void FixedUpdate () 
	{
		rb2d.velocity = Vector2.zero;
		_grid.onlyDisplayPathGizmos = false;
		if(Input.GetButtonDown("Jump"))
		{
			List<Vector3> vectorPath = _Pathfinding.findVectorPath (transform.position, target.position);
			Debug.Log("Vector(" + vectorPath[1].x + "|" + vectorPath[1].y + "|" + vectorPath[1].z + ")");
			rb2d.velocity = vectorPath [1].normalized * 1;
		}

	}
}
