﻿using UnityEngine;
using System.Collections;

public abstract class Stuff : MonoBehaviour {

    public abstract void Use(float timer);
    public abstract Sprite GetSprite();
}
