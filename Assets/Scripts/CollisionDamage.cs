using UnityEngine;
using System.Collections;

public class CollisionDamage : MonoBehaviour {

    public float damage = 10;


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            other.SendMessage("ApplyDamage", damage);
        }
        Debug.Log("TriggerDamage");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("ApplyDamage", damage);
        }
        
    }
    public float GetDamage()
    {
        return damage;
    }
}
