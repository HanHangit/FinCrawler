using UnityEngine;
using System.Collections;

public class EnemyHealthController : MonoBehaviour {

    public float startHealth;
    float health;
    bool wasAttacked;

    void Start()
    {
        wasAttacked = false;
        health = startHealth;
    }

	void ApplyDamage(float damage)
    {
        wasAttacked = true;
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

    public bool getWasAttacked()
    {
        return wasAttacked;
    }
}
