﻿using UnityEngine;
using System.Collections;

public class EnemyViewController : MonoBehaviour {

    public float viewRadius;
    bool playerInViewRadius;
    Vector2 playerPosition;
    float distanceToPlayer;
    bool wasAttacked;
    EnemyHealthController healthControl;

	// Use this for initialization
	void Start () {
        playerInViewRadius = false;
        wasAttacked = false;
        healthControl = GetComponent<EnemyHealthController>();
	}
	
    public bool getPlayerInViewRadius()
    {
        return playerInViewRadius;
    }

	// Update is called once per frame
	void Update () {
        wasAttacked = healthControl.getWasAttacked();

        if (wasAttacked)
        {
            playerInViewRadius = true;
        }
        else
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            distanceToPlayer = (playerPosition - new Vector2(transform.position.x, transform.position.y)).magnitude;
            if (distanceToPlayer > viewRadius)
            {
                playerInViewRadius = false;
            }
            else
            {
                playerInViewRadius = true;
            }
        }
    }


}
