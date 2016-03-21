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
		_grid.onlyDisplayPathGizmos = true;
		if(Input.GetButton("Jump"))			
		{
			List<Vector3> vectorPath = _Pathfinding.findVectorPath (transform.position, target.position);
			/*
			foreach (Vector3 vector in vectorPath)
			{
				Debug.Log("Vector(" + vector.x + "|" + vector.y + "|" + vector.z + ")");
			}
			*/
			/*
			Debug.Log("Vector(" + vectorPath[0].x + "|" + vectorPath[0].y + "|" + vectorPath[0].z + ")");
			Debug.Log("Rigidbody(" + rb2d.position.x + "|" + rb2d.position.y + ")");
			Debug.Log("Transform(" + transform.position.x + "|" + transform.position.y + "|" + transform.position.z + ")");
			*/
			rb2d.velocity = ( vectorPath[0] - transform.position).normalized;
		}

	}
}
