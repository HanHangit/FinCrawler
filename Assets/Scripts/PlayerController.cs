using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float fspeed;

    //--Referenz auf InspectorComponenten
    Rigidbody2D rb2d;
    private Animator anim;
    private Vector2 lastMove;
    private bool playerMoving;

    

    //--Leben/Tod
    private bool isdead = false;

    //--Waffe
    public GameObject WaffePrefab;
    private bool isAttacking = false;
    public Transform spawnPointFernkampfWaffe;
    public float arrowSpeed;



    // Use this for initialization
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>(); //Holt sich die Referenz auf die Componente RigidBody2D
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

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

}
