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
    public int quickslotposition;

    bool openpanel = false;

    public Bow(float damage, float attackSpeed, int quickslotposition, string name, GameObject arrow, Sprite sprite)
    {
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.quickslotposition = quickslotposition;
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
            openpanel = true;
            if (Input.GetKey(KeyCode.E))
            {
                other.GetComponent<PlayerQuickslot>().AddStuff(CreateStuff());
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        openpanel = false;
    }

    void OnGUI()
    {
        if (openpanel)
        {
            Vector2 position = GameObject.FindGameObjectWithTag("Player").transform.position;
            position += new Vector2(Screen.width / 2, Screen.height / 2 - 100);
            Vector2 size = new Vector2(200, 50);
            GUI.Box(new Rect(position, size), ToString());

        }
    }

    public override string ToString()
    {
        return "Name: " + name + "\nSchaden: " + damage + "\nAttackSpeed: " + attackSpeed;
    }

    public override Stuff CreateStuff()
    {
        return new Bow(damage, attackSpeed, quickslotposition, name, arrow, sprite);
    }

    public override int GetQuickslotPosition()
    {
        return quickslotposition;
    }
}
