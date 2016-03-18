using UnityEngine;
using System.Collections;
using System;

public class Sword : Stuff
{

    public float damage;
    public float attackSpeed;
    public float range;
    public new string name;
    public Sprite sprite;

    public Sword(float damage, float attackSpeed, float range, string name, Sprite sprite)
    {
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
            Vector2 playersize = new Vector2(Player.GetComponent<SpriteRenderer>().sprite.textureRect.width, Player.GetComponent<SpriteRenderer>().sprite.textureRect.height);
            Player.GetComponent<PlayerQuickslot>().ResetTimer(); //Timer wird zurückgesetzt.


            Collider2D[] Enemy; //Array welche die Gegner speichert
            EnemyHealthController[] EnemyHealth; //Array welche die Enemy Health Controller speichert
            Vector2 pointa;
            Vector2 pointb;
            Vector2 lookdirection = Player.GetComponent<PlayerController>().LookingDirection();

            if (lookdirection.Equals(Vector2.up))
            {
                pointa = new Vector2(position.x - range, position.y + playersize.y);
                pointb = pointa + new Vector2(range, range);
            }
            else if (lookdirection.Equals(Vector2.right))
            {
                pointa = new Vector2(position.x + range, position.y + playersize.y);
                pointb = pointa + new Vector2(range, -range);
            }
            else if (lookdirection.Equals(Vector2.down))
            {
                pointa = new Vector2(position.x + range, position.y - playersize.y);
                pointb = pointa + new Vector2(-range, -range);
            }
            else if (lookdirection.Equals(Vector2.right))
            {
                pointa = new Vector2(position.x - range, position.y - playersize.y);
                pointb = pointa + new Vector2(-range, range);
            }
            else
            {
                pointa = Vector2.zero;
                pointb = Vector2.zero;
            }

            Debug.DrawLine(pointa,pointb,Color.red,3);

            Enemy = Physics2D.OverlapAreaAll(pointa, pointb,LayerMask.NameToLayer("Enemy"));
            EnemyHealth = new EnemyHealthController[Enemy.Length]; //Ein Array mit der größe der Anzahl der Gegner die in Reichweite sind, wird erstellt
            for(int i = 0; i < Enemy.Length; ++i)
            {
                EnemyHealth[i] = Enemy[i].GetComponent<EnemyHealthController>();
            }
            foreach (EnemyHealthController t in EnemyHealth)
            {
                t.SendMessage("ApplyDamage", damage);
            }
            Debug.Log("Schwert wird benutzt");


        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //TODO: Panel öffnen
            if (Input.GetKey(KeyCode.E))
            {
                other.GetComponent<PlayerQuickslot>().AddStuff(new Sword(damage, attackSpeed, range, name, sprite), 1);
                Destroy(gameObject);
            }
        }
    }
}
