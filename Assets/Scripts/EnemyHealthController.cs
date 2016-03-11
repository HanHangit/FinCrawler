using UnityEngine;
using System.Collections;

public class EnemyHealthController : MonoBehaviour {

    public float startHealth;
    float health;

    void Start()
    {
        health = startHealth;
    }

	void ApplyDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Dying();
        }
    }

    void Dying()
    {
        Destroy(gameObject);
    }
}
