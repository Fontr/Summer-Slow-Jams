using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    private Camera _camera;
    private Vector2 mousePos;
    private Transform _transform;
    private Animator animator;
    private float condition = 1f;

    void Start()
    {
        _camera = Camera.main;
        _transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        mousePos = _camera.ScreenToWorldPoint(Input.mousePosition) - _transform.position;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        condition = angle + 180;
        if (condition <= 325 & condition > 210)
        {
            animator.SetFloat("direct", 1);
        }
        else if (condition <= 210 & condition > 145)
        {
            animator.SetFloat("direct", 2);
        }
        else if (condition <= 145 & condition > 35)
        {
            animator.SetFloat("direct", 3);
        }
        else
        {
            animator.SetFloat("direct", 4);
        }
    }
}

