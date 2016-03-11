using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
    float Damage;
    float AttackSpeed;
    string Name;
    GameObject Arrow;
    string KampfArt;
    
    public void SetAttack(float Damage,float AttackSpeed, string Name, GameObject Arrow, string KampfArt)
    {
        this.Damage = Damage;
        this.AttackSpeed = AttackSpeed;
        this.Name = Name;
        this.Arrow = Arrow;
        this.KampfArt = KampfArt;
    }
}
