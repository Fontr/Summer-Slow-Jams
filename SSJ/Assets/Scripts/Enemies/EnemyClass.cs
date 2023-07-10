using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public float maxHP = 100.0f;
    public float speed = 3.0f;
    private bool isMoving;

    public GameObject player;
    private float viewDist = 15.0f;

    public void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.fixedDeltaTime);
    }
}
