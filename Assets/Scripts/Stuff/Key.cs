using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {
    public string keyname;

    public Key(string name)
    {
        keyname = name;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PeterBar>().AddKey(this);
            Destroy(gameObject);
        }
    }
}
