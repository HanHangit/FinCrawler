using UnityEngine;
using System.Collections;
using System;

public class Bow : Stuff
{
    public float damage;
    public float attackSpeed;
    public new string name;
    public GameObject arrow;
    public Sprite sprite;

    public Bow(float damage, float attackSpeed, string name, GameObject arrow, Sprite sprite)
    {
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.name = name;
        this.arrow = arrow;
        this.sprite = sprite;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }

    public override void Use(float timer)
    {
        if (timer >= attackSpeed)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            Vector2 SpawnPoint = Player.GetComponent<Transform>().GetChild(0).transform.position;
            GameObject Instanz = Instantiate(arrow, SpawnPoint, Quaternion.identity) as GameObject;
            Instanz.GetComponent<ArrowMovingCollision>().SetDamage(damage);
            Debug.Log("Bogen wird benutzt");
            Player.GetComponent<PlayerQuickslot>().ResetTimer();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //TODO: Panel öffnen
            if (Input.GetKey(KeyCode.E))
            {
                other.GetComponent<PlayerQuickslot>().AddStuff(new Bow(damage, attackSpeed, name, arrow, sprite), 2);
                Destroy(gameObject);
            }
        }
    }
}
