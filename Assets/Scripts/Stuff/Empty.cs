using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Empty : Stuff {

   
    Sprite sprite;
    public Empty()
    {
       sprite = Resources.Load<Sprite>("Empty");
    }

    public override Stuff CreateStuff()
    {
        return null;
    }

    public override int GetQuickslotPosition()
    {
        return 1;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }

    public override string ToString()
    {
        return "Empty";
    }

    public override void Use(float timer)
    {
        throw new NotImplementedException();
    }
}
