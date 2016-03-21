using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Sword : Stuff
{

    public float damage;
    public float attackSpeed;
    public float range;
    public new string name;
    public Sprite sprite;
    public int quickslotposition;
    bool openpanel;

    public Sword(float damage, float attackSpeed, float range,int quickslotposition, string name, Sprite sprite)
    {
        this.quickslotposition = quickslotposition;
        openpanel = false;
        this.range = range;
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
        if (timer >= attackSpeed)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player"); //Reference zum Player
            Vector2 position = Player.transform.position;
            Vector2 playersize = new Vector2(Player.GetComponent<SpriteRenderer>().bounds.size.x / 2, Player.GetComponent<SpriteRenderer>().bounds.size.y / 2);
            Player.GetComponent<PlayerQuickslot>().ResetTimer(); //Timer wird zurückgesetzt.


            Collider2D[] Enemy; //Array welche die Gegner speichert
            List<EnemyHealthController> EnemyHealth =  new List<EnemyHealthController>(); //Array welche die Enemy Health Controller speichert
            Vector2 pointa;
            Vector2 pointb;
            Vector2 lookdirection = Player.GetComponent<PlayerController>().LookingDirection();

            if (lookdirection.Equals(Vector2.up))
            {
                pointa = new Vector2(position.x - playersize.x, position.y + playersize.y);
                pointb = pointa + new Vector2(playersize.x * 2, range);
            }
            else if (lookdirection.Equals(Vector2.right))
            {
                pointa = new Vector2(position.x + playersize.x, position.y + playersize.y);
                pointb = pointa + new Vector2(range, -playersize.y * 2);
            }
            else if (lookdirection.Equals(Vector2.down))
            {
                pointa = new Vector2(position.x + playersize.x, position.y - playersize.y);
                pointb = pointa + new Vector2(-playersize.x * 2, -range);
            }
            else if (lookdirection.Equals(Vector2.left))
            {
                pointa = new Vector2(position.x - playersize.x, position.y - playersize.y);
                pointb = pointa + new Vector2(-range, playersize.y * 2);
            }
            else
            {
                pointa = Vector2.zero;
                pointb = Vector2.zero;
            }
            Debug.DrawLine(pointa,pointb,Color.red,3);

            Enemy = Physics2D.OverlapAreaAll(pointa, pointb);
            foreach(Collider2D c in Enemy)
            {
                if (c.CompareTag("Enemy"))
                    EnemyHealth.Add(c.GetComponent<EnemyHealthController>());
            }
            foreach (EnemyHealthController t in EnemyHealth)
            {
                t.SendMessage("ApplyDamage", damage);
            }
            Debug.Log("Schwert wird benutzt");


        }
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

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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

    public override string ToString()
    {
        return "Name: " + name + "\nSchaden: " + damage + "\nAttackSpeed: " + attackSpeed;
    }

    public override Stuff CreateStuff()
    {
        return new Sword(damage, attackSpeed, range,quickslotposition, name, sprite);
    }

    public override int GetQuickslotPosition()
    {
        return quickslotposition;
    }
}
