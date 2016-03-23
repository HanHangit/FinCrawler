using UnityEngine;
using System.Collections;

public class EnemyViewController : MonoBehaviour {

    public float viewRadius;
	bool followsPlayer;
    Vector2 playerPosition;
    float distanceToPlayer;
    bool wasAttacked;
    EnemyHealthController healthControl;

	// Use this for initialization
	void Start () {
        followsPlayer = false;
        wasAttacked = false;
        healthControl = GetComponent<EnemyHealthController>();
	}
	
    public bool FollowsPlayer()
    {
        return followsPlayer;
    }

	// Update is called once per frame
	void Update () {
        wasAttacked = healthControl.getWasAttacked();

        if (wasAttacked)
        {
            followsPlayer = true;
        }
        else
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            distanceToPlayer = (playerPosition - new Vector2(transform.position.x, transform.position.y)).magnitude;
            if (distanceToPlayer > viewRadius)
            {
                followsPlayer = false;
            }
            else
            {
                followsPlayer = true;
            }
        }
    }


}
