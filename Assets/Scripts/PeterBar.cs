using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PeterBar : MonoBehaviour
{
    List<Key> listkey;
    public int money;
    // Use this for initialization
    void Start()
    {
        listkey = new List<Key>();
    }

    public void AddMoney(int value)
    {
        money += value;
    }
    public bool CheckMoney(int value)
    {
        return money >= value;
    }
    public void AddKey(Key key)
    {
        listkey.Add(key);
    }
    public bool CheckKey(string keyname)
    {
        foreach(Key k in listkey)
        {
            if (k.keyname.Equals(keyname))
                return true;
        }
        return false;
    }


}
