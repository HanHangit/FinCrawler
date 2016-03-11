using UnityEngine;
using System.Collections;

public class PlayerQuickslot : MonoBehaviour
{
    Stuff[] actionButtons;
    float timer;
    void Start()
    {
        timer = 0;
        actionButtons = new Stuff[4];
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Keypad1))
        {
            actionButtons[0].Use(timer);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            actionButtons[1].Use(timer);
        }
        if (Input.GetKey(KeyCode.Keypad3))
        {
            actionButtons[2].Use(timer);
        }
        if (Input.GetKey(KeyCode.Keypad4))
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

}
