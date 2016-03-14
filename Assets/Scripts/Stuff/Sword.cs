using UnityEngine;
using System.Collections;
using System;

public class Sword : Stuff {

    public float damage;
    public float attackSpeed;
    public float range;
    public new string name;
    public Sprite sprite;

    public Sword(float damage, float attackSpeed,float range, string name, Sprite sprite)
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

            Player.GetComponent<PlayerQuickslot>().ResetTimer(); //Timer wird zurückgesetzt.

            int size = 0;
            int help = 0;
            GameObject[] Enemy; //Array welche die Gegner speichert
            EnemyHealthController[] EnemyHealth; //Array welche die Enemy Health Controller speichert
            Enemy = GameObject.FindGameObjectsWithTag("Enemy"); //Alle Enemy werden gesucht und gespeichert
            foreach (GameObject t in Enemy) //Für jedes Enemy
            {
                Vector2 EnemyPosition = t.transform.position;  //Position des Gegner wird gespeichert
                if ((EnemyPosition - position).magnitude <= range) //Bei jedem Gegner der in Reichweite ist, wird size um eins erhöht
                {
                    ++size;
                    
                }
            }
            EnemyHealth = new EnemyHealthController[size]; //Ein Array mit der größe der Anzahl der Gegner die in Reichweite sind, wird erstellt
            for (int i = 0; i < Enemy.Length; ++i)
            {
                Vector2 EnemyPosition = Enemy[i].transform.position;
                if ((EnemyPosition - position).magnitude <= range)
                {
                    EnemyHealth[help] = Enemy[i].GetComponent<EnemyHealthController>();
                    help++;
                }
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
                other.GetComponent<PlayerQuickslot>().AddStuff(new Sword(damage, attackSpeed,range, name, sprite), 1);
                Destroy(gameObject);
            }
        }
    }
}
