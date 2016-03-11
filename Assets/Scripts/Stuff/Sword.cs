using UnityEngine;
using System.Collections;
using System;

public class Sword : Stuff {

    public float damage;
    public float attackSpeed;
    public new string name;
    public Sprite sprite;

    public Sword(float damage, float attackSpeed, string name, Sprite sprite)
    {
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.name = name;
        this.sprite = sprite;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }

    public override void Use(float timer)
    {
        //TODO: Bogen Angriff
        Debug.Log("Schwert wird benutzt");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //TODO: Panel öffnen
            if (Input.GetKey(KeyCode.E))
            {
                other.GetComponent<PlayerQuickslot>().AddStuff(new Sword(damage, attackSpeed, name, sprite), 1);
                Destroy(gameObject);
            }
        }
    }
}
