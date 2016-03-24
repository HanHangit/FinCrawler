using UnityEngine;
using System.Collections;

public class LebenstrankCollision : MonoBehaviour {

    public float leben;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("ApplyDamage", -leben);
            Destroy(gameObject);
        }
    }
}
