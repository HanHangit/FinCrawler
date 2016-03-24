using UnityEngine;
using System.Collections;

public class EnemyMoveController : MonoBehaviour
{

    Rigidbody2D rb2d;
    public float fSpeed;
    Vector2 playerPosition;
    Vector2 movementVector;
    EnemyViewController viewControl;
    bool playerInViewRadius;

    //OverHeadHealth
    bool onCD;
    public int CouldownForDamage;


    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        viewControl = GetComponent<EnemyViewController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        movementVector = playerPosition - new Vector2(transform.position.x, transform.position.y);


    }

    void FixedUpdate()
    {
        if (viewControl.getPlayerInViewRadius())
        {
            rb2d.velocity = movementVector.normalized * fSpeed;
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }
   
    IEnumerator CoolDownDamage()
    {
        onCD = true;
        yield return new WaitForSeconds(CouldownForDamage);
        onCD = false;
    }
}