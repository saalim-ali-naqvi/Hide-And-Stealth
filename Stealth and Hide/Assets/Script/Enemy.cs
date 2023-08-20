using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{


    public Transform[] waypoints;
    private int _currentWaypointIndex = 0;
    private float _speed = 2f;

    [SerializeField]
    Transform player;
    [SerializeField]
    float agroRange;
    [SerializeField]
    float moveSpeed;
    Rigidbody2D rb2d;
    private bool isEnemyChaseToPlayer;
    private float xDirectionVelocity, yDirectionVeloccity;


    void Start()
    {
        //player = FindObjectOfType<Player>();
        //targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();   
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isEnemyChaseToPlayer)
            Patronlling_Path();
        ChaseEnemy();
    }

    
    void ChaseEnemy()
    {

        // distance to player
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < agroRange)
        {
            ChasePlayer();
            isEnemyChaseToPlayer = true;


            /*Debug.DrawRay(transform.position, (player.position - transform.position) * agroRange, Color.red);
            Vector2 direction = player.position - transform.position;
            RaycastHit2D raycast = Physics2D.Raycast(
            transform.position,
            direction,
            agroRange);

            if (raycast)
            {
                if (raycast.collider.CompareTag("Wall"))
                    return;
                else if (raycast.collider.CompareTag("Player"))
                {
                    ChasePlayer();
                    isEnemyChaseToPlayer = true;
                }

            }*/
        }
        else
        {
            //stop chasing player
            StopChasingPlayer();
            isEnemyChaseToPlayer = false;
        }
    }

    void ChasePlayer()
    {
        xDirectionVelocity = transform.position.x <= player.position.x ? moveSpeed : -moveSpeed;

        yDirectionVeloccity = transform.position.y <= player.position.y ? moveSpeed : -moveSpeed;

        rb2d.velocity = new Vector2(xDirectionVelocity, yDirectionVeloccity);

    }

    void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
    }


    void Patronlling_Path()
    {
        Transform wp = waypoints[_currentWaypointIndex];
        if (Vector3.Distance(transform.position, wp.position) < 0.01f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                wp.position,
                _speed * Time.deltaTime);
        }

    }

}


















// void StopChasingBehindWall()
// {
//     RaycastHit2D raycastHit2D = Physics2D.Raycast(GetPosition(),agroRange,viewDistance);
//     if(raycastHit2D.collider != null)
//     {
//         ChasePlayer();
//         isEnemyChaseToPlayer=true;
//     }
//     if(raycastHit2D.collider.gameObject.GetComponent<Player>()!=null)
//     {
//         StopChasingPlayer();
//         isEnemyChaseToPlayer=false;
//     }
// }


// void OnTriggerEnter2D(Collider2D other)
// {
//     if (other.gameObject.CompareTag("Wall"))
//     {
//         CanHide = true;
//     }
// }


// void OnTriggerExit2D(Collider2D other)
// {
//     if (other.gameObject.CompareTag("Wall"))
//     {
//         CanHide = false;
//     }
// }



// void OnCollisionEnter2D(Collision2D collision)
// {
//     if (collision.collider.tag == "Player")
//     { 
//         //player hit point and effect
//         //reset player position
//         //player.ResetPlayerToInitialPosition();
//        // Player.Instance.Loselife();
//     }        
// }