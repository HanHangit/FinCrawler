using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float fspeed;

    //--Referenz auf InspectorComponenten
    Rigidbody2D rb2d;
    private Animator anim;
    private Vector2 lastMove;
    private bool playerMoving;

    enum LookinDirection { Up,Right,Down,Left};

    LookinDirection lookdirection;

    //--Leben/Tod
    private bool isdead = false;

    //--Waffe
    private bool isAttacking = false;
    Vector2 direction;
    Vector2 movement_vector;


    // Use this for initialization
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>(); //Holt sich die Referenz auf die Componente RigidBody2D
        anim = GetComponent<Animator>();
        direction = Vector2.zero;
        lookdirection = LookinDirection.Down;

    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 position = transform.position;
        Vector2 vectorsize = new Vector2(1, 1);
        int mask = 1 << 9;
        mask = ~mask;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider2D[] hit = Physics2D.OverlapAreaAll(position - vectorsize, position + vectorsize);
            foreach (Collider2D c in hit)
            {
                Debug.Log("Hit: " + c.CompareTag("Wall"));
            }
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            direction = movement_vector;


        }
        else if (Input.GetAxisRaw("Vertical") != 0)
        {
            movement_vector = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            direction = movement_vector;
        }
        else
        {
            movement_vector = Vector2.zero;
        }

        if (movement_vector.x == 1)
            lookdirection = LookinDirection.Right;
        else if (movement_vector.x == -1)
            lookdirection = LookinDirection.Left;
        else if (movement_vector.y == 1)
            lookdirection = LookinDirection.Up;
        else if (movement_vector.y == -1)
            lookdirection = LookinDirection.Down;

        // if (Input.GetAxisRaw("Horizontal") == Input.GetAxisRaw("Vertical") )
        // {
        //
        //     direction.x = 1;
        // }
        // if (Input.GetAxisRaw("Horizontal") == -1)
        // {
        //     direction.x = -1;
        // }
        // if (Input.GetAxisRaw("Vertical") == 1)
        // {
        //     direction.y = 1;
        // }
        // if (Input.GetAxisRaw("Vertical") == -1)
        // {
        //     direction.y = -1;
        // }




        if (movement_vector != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("input_x", movement_vector.x);
            anim.SetFloat("input_y", movement_vector.y);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        rb2d.MovePosition(rb2d.position + movement_vector * Time.deltaTime * fspeed);
    }
    public Vector2 MoveDirectionPlayer()
    {
        return direction;
    }

    public Vector2 LookingDirection()
    {
        if (lookdirection.Equals(LookinDirection.Up))
            return Vector2.up;
        if (lookdirection.Equals(LookinDirection.Right))
            return Vector2.right;
        if (lookdirection.Equals(LookinDirection.Down))
            return Vector2.down;
        if (lookdirection.Equals(LookinDirection.Left))
            return Vector2.left;
        return Vector2.zero;
    }
}
