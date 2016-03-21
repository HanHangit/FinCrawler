﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public Transform player;
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	public bool onlyDisplayPathGizmos;

	Node[,] grid;
	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Start()
	{
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

		CreateGrid();
	}

	public int MaxSize
	{
		get
		{
			return gridSizeX * gridSizeY;
		}

	}

	void CreateGrid()
	{
		grid = new Node[gridSizeX, gridSizeY];
		Vector2 worldBottomLeft = new Vector2(transform.position.x, transform.position.y) - Vector2.right * gridWorldSize.x/2 - Vector2.down * gridWorldSize.y/2;
		for (int x = 0; x < gridSizeX; x ++)
		{
			for (int y = 0; y < gridSizeY; y ++)
			{
				Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.down * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics2D.OverlapCircle (worldPoint, nodeRadius, unwalkableMask));
				grid[x, y] = new Node(walkable, worldPoint, x, y);
			}
		}
	}

	public List<Node> GetNeighbours(Node node)
	{
		List<Node> neighbours = new List<Node> ();
		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
				{
					continue;
				}

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					neighbours.Add (grid [checkX, checkY]);
				}
			}
		}	

		return neighbours;
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition)
	{
		float percentX = (worldPosition.x) / gridWorldSize.x;
		float percentY = - (worldPosition.y) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		return grid[x, y];
	}

	public List<Node> path;
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

		if (onlyDisplayPathGizmos)
		{
			if (path != null)
			{
				foreach(Node n in path)
				{
					Gizmos.color = Color.black;
					Gizmos.DrawCube (n.worldPosition, Vector3.one * (nodeDiameter - .1f));
				}
			}
		}
		else
		{

			if (grid != null)
			{
				Node playerNode = NodeFromWorldPoint (player.position);
				foreach (Node n in grid)
				{
					if (n.walkable)
					{
						Gizmos.color = Color.grey;
					} else
					{
						Gizmos.color = Color.red;
					}
					if (path != null)
					{
						if (path.Contains (n))
						{
							Gizmos.color = Color.black;
						}
					}
						
					if (playerNode == n)
					{
						Gizmos.color = Color.blue;
					}
					Gizmos.DrawCube (n.worldPosition, Vector3.one * (nodeDiameter - .1f));
				}
			}
		}
	}
}
