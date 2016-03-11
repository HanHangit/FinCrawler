using UnityEngine;
using System.Collections;

public class PlayerQuickslot : MonoBehaviour
{
    Stuff[] actionButtons;
    void Start()
    {
        actionButtons = new Stuff[4];
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad1))
            actionButtons[0].Use();
        if (Input.GetKey(KeyCode.Keypad2))
            actionButtons[1].Use();
        if (Input.GetKey(KeyCode.Keypad3))
            actionButtons[2].Use();
        if (Input.GetKey(KeyCode.Keypad4))
            actionButtons[3].Use();
    }

    public void AddStuff(Stuff ToAdd, int number)
    {
        actionButtons[--number] = ToAdd;
    }

}
