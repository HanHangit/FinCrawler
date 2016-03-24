using UnityEngine;
using System.Collections;

public class ArrowMovingCollision : MonoBehaviour
{
    public float speed;

    float damage;
    Rigidbody2D r2bd;
    // Use this for initialization
    void Start()
    {
        Vector2 direction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().MoveDirectionPlayer();
        if (direction.x == -1)
            transform.Rotate(Vector3.forward, 180);
        else if (direction.y == 1)
            transform.Rotate(Vector3.forward, 90);
        else if (direction.y == -1)
            transform.Rotate(Vector3.forward, 270);

        r2bd = GetComponent<Rigidbody2D>();
        r2bd.AddForce(direction.normalized * speed);

    }

    public void SetDamage(float Set)
    {
        damage = Set;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.SendMessage("ApplyDamage", damage);

            Destroy(gameObject);
        }
        if (!other.CompareTag("Player") && !other.CompareTag("Munition"))
        {
            Destroy(gameObject);
        }
    }
}
