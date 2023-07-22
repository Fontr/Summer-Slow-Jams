using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PosZ : MonoBehaviour
{
    private new Transform transform;
    [SerializeField] private bool isStatic = true;
    [SerializeField] private float cChange = 0f;

    private void Start()
    {
        transform = GetComponent<Transform>();
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y-cChange);
        if (isStatic) { enabled = false; }
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y-cChange);
    }

    private void OnDrawGizmosSelected()
    {
        transform = GetComponent<Transform>();
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y-cChange), new Vector2(transform.position.x, transform.position.y));
    }
}
