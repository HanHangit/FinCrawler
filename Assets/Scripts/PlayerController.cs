using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float fspeed;

    //--Referenz auf InspectorComponenten
    Rigidbody2D rb2d;
    private Animator anim;
    private Vector2 lastMove;
    private bool playerMoving;

    bool lookRight;
    bool lookTop;
   


    //--Leben/Tod
    private bool isdead = false;

    //--Waffe
    public GameObject WaffePrefab;
    private bool isAttacking = false;
    public Transform spawnPointFernkampfWaffe;
    public float arrowSpeed;
    Vector2 direction;
    Vector2 movement_vector;


    // Use this for initialization
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>(); //Holt sich die Referenz auf die Componente RigidBody2D
        anim = GetComponent<Animator>();
        direction = Vector2.zero;

    }

    // Update is called once per frame
    void Update()
    {

        //Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        

        if(Input.GetAxisRaw("Horizontal") != 0)
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
}
