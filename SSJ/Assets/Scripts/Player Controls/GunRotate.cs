using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    private Camera _camera;
    private Vector2 mousePos;
    private Transform _transform;
    [SerializeField] private Transform _transformPlayer;
    private Rigidbody2D rb;
    private SpriteRenderer _renderer, _renderer1;
    private float gunPos = 0f;
    void Start()
    {
        _camera = Camera.main;
        _transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        _renderer = _transform.Find("gun").GetComponent<SpriteRenderer>();
        _renderer1 = _transform.Find("gun2").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        mousePos = _camera.ScreenToWorldPoint(Input.mousePosition) - _transform.position;
    }

    private void FixedUpdate()
    {
        float angle = Mathf.Atan2(mousePos.y, mousePos.x)*Mathf.Rad2Deg;
        rb.rotation = angle;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        if (Mathf.Abs(angle) > 90)
        {
            _renderer.flipY = true;
            _renderer1.flipY = true;
        }
        else 
        {
            _renderer.flipY = false;
            _renderer1.flipY = false;
        }

        if (angle > 35f & angle <= 145f)
        {
            gunPos = 0.1f;
        }
        else
        {
            gunPos = -0.1f;
        }
        _transform.position = new Vector3(transform.position.x, transform.position.y, _transformPlayer.position.z+gunPos);

    }
}
