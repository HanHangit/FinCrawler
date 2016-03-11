using UnityEngine;
using System.Collections;

public class ArrowMovingCollision : MonoBehaviour {
    public float speed;

    float damage;
    Rigidbody2D r2bd;
	// Use this for initialization
	void Start () {
        Vector2 direction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().MoveDirectionPlayer();
        r2bd = GetComponent<Rigidbody2D>();
        r2bd.AddForce(direction.normalized * speed);	
            
	}

    public void SetDamage(float Set)
    {
        damage = Set;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if(other.CompareTag("Enemy"))
        {
            //TODO: Enemy-Damage
        }
        if(!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
