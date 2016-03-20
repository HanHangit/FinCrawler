using UnityEngine;
using System.Collections;

public class PlayerSpawnPointController : MonoBehaviour
{
    Vector2 playersize;
    Transform weaponanimtransform;
    // Use this for initialization
    void Start()
    {
        playersize = GetComponentInParent<SpriteRenderer>().bounds.size;
        weaponanimtransform = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerpos = transform.parent.position;
        Vector2 lookdir = GetComponentInParent<PlayerController>().LookingDirection();
        Vector2 setpos = playerpos;
        if (lookdir.Equals(Vector2.up))
        {
            setpos.y += playersize.y / 2;
            weaponanimtransform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (lookdir.Equals(Vector2.right))
        {
            setpos.x += playersize.x / 2;
            weaponanimtransform.eulerAngles = new Vector3(0, 0, 270);
        }
        if (lookdir.Equals(Vector2.down))
        {
            setpos.y -= playersize.y / 2;
            weaponanimtransform.eulerAngles = new Vector3(0, 0, 180);
        }
        if (lookdir.Equals(Vector2.left))
        {
            setpos.x -= playersize.x / 2;
            weaponanimtransform.eulerAngles = new Vector3(0, 0, 90);
        }

        transform.position = setpos;

    }
}
