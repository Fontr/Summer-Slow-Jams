using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyClass enemy;

    private float viewDist = 15.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<EnemyClass>();
    }

    void FixedUpdate()
    {
        float PLdistance = Vector2.Distance(transform.position, enemy.player.transform.position);
        enemy.MoveToPlayer();
        /*
        if (Input.GetKey(KeyCode.H))
        {
            enemy.FollowPlayer();
        }*/
        
    }
}
