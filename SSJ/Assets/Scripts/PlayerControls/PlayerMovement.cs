using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float fixedSpeed = 6.0f, isMoving = 1;
    private Vector2 movement;
    private float speed;
    public Rigidbody2D rb;

    
    void Update()
    {
        speed = fixedSpeed;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0f && movement.y != 0f)
        {
            speed = fixedSpeed/1.4f;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime * isMoving);
    }
}
