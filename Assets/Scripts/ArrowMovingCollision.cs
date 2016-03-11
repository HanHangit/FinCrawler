using UnityEngine;
using System.Collections;

public class ArrowMovingCollision : MonoBehaviour {
    public float speed;

    float damage;
    Rigidbody2D r2bd;
	// Use this for initialization
	void Start () {
        r2bd = GetComponent<Rigidbody2D>();
        r2bd.AddForce(Vector2.up * speed);	    
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
