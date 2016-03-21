using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Pathfinding : MonoBehaviour
{

	Grid grid;
	public Transform seeker, target;

	void Awake()
	{
		grid = GetComponent<Grid>();
	}

	void Update ()
	{
		if(Input.GetButtonDown("Jump"))
		{
			FindPath (seeker.position, target.position);
		}
	}

	void FindPath(Vector3 startPos, Vector3 targetPos)
	{
		Stopwatch sw = new Stopwatch ();
		sw.Start();
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

		Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);

		while(openSet.Count > 0)
		{
			Node currentNode = openSet.RemoveFirst();

			closedSet.Add (currentNode);

			if (currentNode == targetNode)
			{
				sw.Stop();
				print ("Path found: " + sw.ElapsedMilliseconds + "ms");
				RetracePath (startNode, currentNode);
				return;
			}

			foreach (Node neighbor in grid.GetNeighbours(currentNode))
			{
				if (!neighbor.walkable || closedSet.Contains (neighbor))
				{
					continue;
				}

				int newMovementCostToNeighbor = currentNode.gCost + GetDistance (currentNode, neighbor);
				if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
				{
					neighbor.gCost = newMovementCostToNeighbor; 
					neighbor.hCost = GetDistance (neighbor, targetNode);
					neighbor.parent = currentNode;

					if (!openSet.Contains (neighbor))
					{
						openSet.Add (neighbor);
						openSet.UpdateItem (neighbor);
					}
				}
			}
		}
	}

	void RetracePath(Node startNode, Node endNode)
	{
		List<Node> Path = new List<Node> ();
		Node currentNode = endNode;

		while (currentNode != startNode)
		{
			Path.Add (currentNode);
			currentNode = currentNode.parent;
		}
		Path.Reverse ();

		grid.path = Path;
	}


	int GetDistance(Node nodeA, Node nodeB)
	{
		int dstX = Mathf.Abs (nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs (nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
		{
			return 14 * dstY + 10 * (dstX - dstY);
		} else
		{
			return 14 * dstX + 10 * (dstY - dstX);
		}

	}

}
