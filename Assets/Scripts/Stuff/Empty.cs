using UnityEngine;
using System.Collections;
using System;

public class Empty : Stuff {


    Sprite sprite;
    public Empty()
    {
        sprite = Resources.Load<Sprite>("Empty");
        Debug.Log(sprite);
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }

    public override void Use(float timer)
    {
        throw new NotImplementedException();
    }
}
