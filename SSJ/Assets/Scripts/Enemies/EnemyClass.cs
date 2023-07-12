using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public float maxHP = 100.0f;
    public float speed = 3.0f;
    private bool isMoving;

    public GameObject player;

    private Ray ray;
    //private float PLdistance;

    public void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.fixedDeltaTime);
    }

    /*
    public void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        ray = new Ray(transform.position, direction);
        Debug.DrawRay(transform.position, direction, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform);
        }
    }

    /*
    public void FindPath()
    {
        PLdistance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;
    }   */
}
