using UnityEngine;
using System.Collections;

public class EnemyMoveController : MonoBehaviour {

	public Transform target;
	public float speed;
	Vector3[] path;
	int targetIndex;
	EnemyViewController viewControl;

	void Start()
	{
		viewControl = GetComponent<EnemyViewController> ();
	}

	void FixedUpdate()
	{
		if (viewControl.FollowsPlayer ())
		{
			PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
		}
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			path = newPath;
			StopCoroutine ("FollowPath");
			StartCoroutine ("FollowPath");
		}
	}

	IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = path [0];
		while (true)
		{
			if (transform.position == currentWaypoint)
			{
				targetIndex++;
				if (targetIndex >= path.Length)
				{
					yield break;
				}
				currentWaypoint = path [targetIndex];
			}
			transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, speed * Time.deltaTime);
			// move to next frame
			yield return null;
		}
	}

	public void OnDrawGizmos()
	{
		if (path != null)
		{
			for (int i = targetIndex; i < path.Length; i++)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawCube (path [i], new Vector3(0.2f, 0.2f, 0.2f));

				if (i == targetIndex)
				{
					Gizmos.DrawLine (transform.position, path [i]);
				} else
				{
					Gizmos.DrawLine (path [i - 1], path [i]);
				}
			}
		}


	}

}
