using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float fixedSpeed = 300.0f;
    [SerializeField] private float isMoving = 1;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float speed;
    private Vector2 moveDirection;
    [SerializeField] private Animator animator;

    [Header("Dash")]
    [SerializeField] float dashSpeed = 1000f;
    [SerializeField] float dashDuration = 0.5f;
    [SerializeField] float dashCooldown = 2f;
    private bool isDashing, isDashingOnCD = false;

    public bool isDialog = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x * 16) / 16, Mathf.Round(transform.position.y * 16) / 16, transform.position.z);
    }
    void Update()
    {
        //отмена управления персонажем при дэше
        if (isDashing)
        {
            return;
        }

        //направление движения
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(movement.x, movement.y).normalized;

        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);
        
        if (movement.y == 0)
        {
            animator.SetFloat("horizontal", movement.x);
        }
        else { animator.SetFloat("horizontal", 0);}

        //скорость по диагонали
        speed = fixedSpeed;
        if (movement.x != 0f && movement.y != 0f)
        {
            speed = fixedSpeed/1.4f;
        }

        //проверка движения
        isMoving = 1;
        if (movement.x == 0f && movement.y == 0f)
        {
            isMoving = 0;
        }

        //вызов дэша
        if (Input.GetKey(KeyCode.Mouse1) && isMoving==1 && !isDashingOnCD)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        //отмена управления персонажем при дэше и диалогах
        if (isDashing)
        {
            return;
        }
        if (isDialog)
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }
        //изменение скорости при обычном движении
        rb.velocity = new Vector2(moveDirection.x * speed * Time.fixedDeltaTime * isMoving, moveDirection.y * speed * Time.fixedDeltaTime * isMoving);
    }

    private IEnumerator Dash()
    {
        //дэш, его длительность и кд
        isDashingOnCD = true;
        isDashing = true;
        rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown - dashDuration);
        isDashingOnCD = false;
    }
}
