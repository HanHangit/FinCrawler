﻿using UnityEngine;
using System.Collections;

public class ChestController : MonoBehaviour {
    public GameObject[] Content; // Der Inhalt der Truhe.
    public string Key; // Der Key, welcher für die Truhe benötigt wird. (RedKey, GreenKey, YellowKey, ...)

    float Range; //Der Radius des Kreises, auf dem die Objekte aus der Truhe spawnen
    bool Looted; //Überprüft, ob die Kiste schon geöffnet wurde.
	// Use this for initialization
	void Start () {
        Range = 2;
        Looted = false;
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player")) 
        {
            if(true && !Looted) //TODO: Überprüfen ob Spieler den notewendigen Schlüssel besitzt
            {
                SpawnContent();
                Looted = true; //Die Truhe wurde gelooted.
                //TODO: Bild ändern auf Truhe geöffnet.
            }
        }
    }

    void SpawnContent()
    {
        Vector2 MyPosition = transform.position;
        foreach(GameObject t in Content) //Für jedes GameObject
        {
            Vector2 SpawnPosition; //Die Position wird gesetzt, wo das Objekt gespawnt werden soll.
            do
            {
                SpawnPosition = new Vector2(Random.Range(-1f,1f), Random.Range(-1f, 1f)); //Eine zufällige Position

            }
            while (false); //TODO: Überprüfen, ob die SpawnPosition auch erreichbar ist durch den Spielern. Nicht in der Wand etc.
            SpawnPosition = SpawnPosition.normalized * Range + MyPosition; //Die SpawnPosition wird in Relation zu der eigenen Position gesetzt.
            Instantiate(t, SpawnPosition, Quaternion.identity); //Objekt wird erstellt.
        }
    }
}