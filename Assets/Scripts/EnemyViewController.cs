using UnityEngine;
using System.Collections;

public class EnemyViewController : MonoBehaviour {

    public float viewRadius;
    bool playerInViewRadius;
    Vector2 playerPosition;
    float distanceToPlayer;

	// Use this for initialization
	void Start () {
        playerInViewRadius = false;
	}
	
    public bool getPlayerInViewRadius()
    {
        return playerInViewRadius;
    }

	// Update is called once per frame
	void Update () {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        distanceToPlayer = (playerPosition - new Vector2(transform.position.x, transform.position.y)).magnitude;

        if(distanceToPlayer > viewRadius)
        {
            playerInViewRadius = false;
        }
        else
        {
            playerInViewRadius = true;
        }
    }


}
