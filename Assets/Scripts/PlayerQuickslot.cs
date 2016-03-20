using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerQuickslot : MonoBehaviour
{
    public Image[] image;

    Animator anim;
    Stuff[] actionButtons;
    float timer;
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("WeaponAnimator").GetComponent<Animator>();
        
        timer = 0;
        actionButtons = new Stuff[4];
       for(int i = 0; i < actionButtons.Length; ++i)
       {
           actionButtons[i] = new Empty();
          
       }
    }
    void Update()
    {
        timer += Time.deltaTime;
        anim.SetBool("IsAttacking", false);
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            anim.SetBool("IsAttacking", true);
            actionButtons[0].Use(timer);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            actionButtons[1].Use(timer);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            actionButtons[2].Use(timer);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            actionButtons[3].Use(timer);
        }
    }

    public void AddStuff(Stuff ToAdd, int number)
    {
        actionButtons[--number] = ToAdd;
    }

    public void ResetTimer()
    {
        timer = 0;
    }

    void OnGUI()
    {
        
        
        
        //Vector2 position = new Vector2(100, Screen.height - 50);
        //Vector2 size = new Vector2(50, 50);
        for(int i = 0; i < actionButtons.Length; ++i)
        {
            //GUI.DrawTexture(new Rect(position, size), actionButtons[i].GetSprite().texture);
            //GUI.Box(new Rect(position, size), " Kein \nBild");
            //
            //ca = actionButtons[i].GetSprite().texture;
            image[i].sprite = actionButtons[i].GetSprite();
            

        }
    }
}
