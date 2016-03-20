using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFollowingPath : MonoBehaviour {

    public Transform target;
    public float updatepathtime;
    public float speed;

    Rigidbody2D rb2d;
    List<Vector2> path;
    float timer = -5;
    int nodepoint = 1;
    Vector2 direction;
    Vector2 nextnode;
    float range = 0.2f;
	// Use this for initialization
	void Start () {
        Vector2 position = transform.position;
        Vector2 targetposition = target.position;
        rb2d = GetComponent<Rigidbody2D>();
        path = GetComponent<TargetPathfinding>().Pathfinding(position,targetposition, GetComponent<SpriteRenderer>().sprite.rect.width / 8);
        Debug.Log("Pfad: " + path.Count);
        foreach(Vector2 t in path)
        {
            Debug.Log(t);
        }
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        Vector2 position = transform.position;
        nextnode = path[path.Count - nodepoint];
        direction =  nextnode - position;

        foreach (Vector2 v in path)
        {
            Debug.DrawLine(v, v + new Vector2(0.1f, 0.1f),Color.blue);
        }

        if (timer >= updatepathtime)
        {

            Vector2 targetposition = target.position;
            path = GetComponent<TargetPathfinding>().Pathfinding(position, targetposition, GetComponent<SpriteRenderer>().sprite.rect.width / 8);
            if (path.Count == 0)
                path.Add(position);
            nodepoint = 1;
            timer = 0;
        }

        if ((nextnode - position).magnitude < range && (nodepoint + 1 <= path.Count))
            nodepoint++;
    }

    void FixedUpdate()
    {
        rb2d.velocity = direction.normalized * speed;
    }
}
