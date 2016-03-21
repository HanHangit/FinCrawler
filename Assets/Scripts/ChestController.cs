using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChestController : MonoBehaviour
{
    public Stuff[] content; // Der Inhalt der Truhe.

    public string Key; // Der Key, welcher für die Truhe benötigt wird. (RedKey, GreenKey, YellowKey, ...)

    Animator anim;
    float Range; //Der Radius des Kreises, auf dem die Objekte aus der Truhe spawnen
    bool Looted; //Überprüft, ob die Kiste schon geöffnet wurde.
    PeterBar barhandler;
    bool openpanel;
    Camera camera;
    PlayerQuickslot quickslot;
    KeyCode[] keyCodes ={
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9,
            };

    // Use this for initialization
    void Start()
    {

        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        quickslot = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerQuickslot>();
        Range = 1;
        Looted = false;
        openpanel = false;

        anim = GetComponent<Animator>();
        anim.SetBool("Open", false);

        barhandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PeterBar>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            openpanel = true;
            if (barhandler.CheckKey(Key) && !Looted)
            {
                Looted = true; //Die Truhe wurde gelooted.
                //TODO: Bild ändern auf Truhe geöffnet.
                anim.SetBool("Open", true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        openpanel = false;
    }

    void OnGUI()
    {
        if (Looted && openpanel)
        {
            Vector2 position = camera.WorldToScreenPoint(transform.position);
            position.y = Screen.height - position.y;
            Vector2 recposition = position;
            Vector2 size = new Vector2(200, 50);
            for (int i = 0; i < content.Length; ++i)
            {
                position.y = recposition.y + size.y * i;
                GUI.Box(new Rect(position, size), content[i].ToString());
                if (Input.GetKeyDown(keyCodes[i]))
                {
                    if (content[i] != null)
                        quickslot.AddStuff(content[i].CreateStuff());
                    content[i] = new Empty();
                }
            }
        }

        else if (openpanel)
        {
            //Draw needed Key for the Chest
            Vector2 spritesize = GetComponent<SpriteRenderer>().bounds.size;
            Vector2 myposition = transform.position;
            Vector2 size = new Vector2(51, 24);

            myposition.x -= spritesize.x;
            myposition.y += spritesize.y;
            myposition = camera.WorldToScreenPoint(myposition);
            myposition.y = Screen.height - myposition.y;

            Sprite sprite = Resources.Load<Sprite>(Key);
            Rect rectposition = new Rect(myposition, size);
            GUI.Box(rectposition, sprite.texture);
        }
    }
}
