using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float fixedSpeed = 300.0f;
    [SerializeField] private float isMoving = 1;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 movement;
    private float speed;
    private Vector2 moveDirection;


    [Header("Dash")]
    [SerializeField] float dashSpeed = 1000f;
    [SerializeField] float dashDuration = 0.5f;
    [SerializeField] float dashCooldown = 2f;
    private bool isDashing, isDashingOnCD = false;



    void Update()
    {
        //������ ���������� ���������� ��� ����
        if (isDashing)
        {
            return;
        }

        //����������� ��������
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(movement.x, movement.y).normalized;

        //�������� �� ���������
        speed = fixedSpeed;
        if (movement.x != 0f && movement.y != 0f)
        {
            speed = fixedSpeed/1.4f;
        }

        //�������� ��������
        isMoving = 1;
        if (movement.x == 0f && movement.y == 0f)
        {
            isMoving = 0;
        }

        //����� ����
        if (Input.GetKey(KeyCode.Mouse1) && isMoving==1 && !isDashingOnCD)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        //������ ���������� ���������� ��� ����
        if (isDashing)
        {
            return;
        }

        //��������� �������� ��� ������� ��������
        rb.velocity = new Vector2(moveDirection.x * speed * Time.fixedDeltaTime * isMoving, moveDirection.y * speed * Time.fixedDeltaTime * isMoving);
    }

    private IEnumerator Dash()
    {
        //���, ��� ������������ � ��
        isDashingOnCD = true;
        isDashing = true;
        rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown - dashDuration);
        isDashingOnCD = false;
    }
}
