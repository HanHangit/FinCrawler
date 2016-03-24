using UnityEngine;
using System.Collections;

public class MoneyCollision : MonoBehaviour {

    public float money;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("AddMoney", money);
            Destroy(gameObject);
        }
    }
}
