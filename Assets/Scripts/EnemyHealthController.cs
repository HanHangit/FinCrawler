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
        CombatTextManager.Instance.CreateText(transform.position, "-" + damage.ToString(), Color.magenta, false);


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

    void OnGUI()
    {
       
    }
}
