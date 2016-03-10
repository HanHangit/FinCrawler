using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthController : MonoBehaviour {

    //--LebenVerwaltungsVariabeln
    public float startHealth; //Energie
    public int startLifePoints; // Maximale Versuche(leben)

    float health; //Leben des Spielers
    int lifePoints;//############### Maximal LEBEN des Spielers hier festlegen!
    Vector2 startPostion;

    bool isDead = false;
    bool isDamageable = true; //Kann der Spieler gerade schaden bekommen?

    //Referenzen
    Animator anim;
    PlayerController playerController;




    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        startPostion = new Vector2(playerController.transform.position.x, playerController.transform.position.y);

        //##########Startposition - Level, Guide Elemente
        if (Application.loadedLevel == 0)
        {
            health = startHealth;
            lifePoints = startLifePoints;
        }
        else
        {//Nehmen die Werte von einer anderen Szene mit
            health = PlayerPrefs.GetFloat("Health");
            lifePoints = PlayerPrefs.GetInt("LifePoints");

        }
	}
	
    void ApplyDamage(float damage)
    {
        if(isDamageable)
        {
            health -= damage;
            health = Mathf.Max(0, health);
            Debug.Log("Leben: " + health);
            if (!isDead)
            {
                if(health == 0)
                {
                    isDead = true;
                    Dying();
                }
                else
                {
                    Damaging();
                }
            }
            isDamageable = false;
            Invoke("ResetIsDamageable", 1f);
        }
    }

    void ResetIsDamageable()
    {
        isDamageable = true;
    }

    void Dying()
    {//-- evtl noch sterbeanimation etc
        //anim.SetBool("dead", true);
        playerController.enabled = false; //SpielerFalsesetzten
        lifePoints--;
        UpdateView();
        Debug.Log("TOT");

        if (lifePoints <= 0)
        {//--GAMEOVER
            //neuStarten
            //mainMenu usw
           
            Invoke("StartGame", 3f);

        }
        else
        {//Position neu gesetzt

            //Restart Level
            Invoke("RestartLevel", 3f);

        }

    }
    void StartGame()
    {//Startet Szene X, zb MainMenu 0, ERstes Lvl 1
        Application.LoadLevel(0);
    }

    void RestartLevel()
    {//--Startet das level neu an der gleichen Position
        playerController.transform.position = startPostion;
        isDead = false;
        health = startHealth;
        anim.SetBool("dead", false);
        playerController.enabled = true;

        //Level neu genierien - starten - spieler zurücksetzen
    }
    void Damaging()
    {//-- HitAnimation abspielen zb, Schaden verteilen, SoundDateiLaden
        UpdateView();
    }

    void OnDestroy()
    {//--Dientdazu die Variabeln wie zb Leben zu speichern für andere Szenen
        PlayerPrefs.SetFloat("Health", health);
        PlayerPrefs.SetInt("LifePoints", lifePoints);
    }

    void UpdateView()
    {//--GUI 
        
    }
}
