using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerPositionZ : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "zPos")
        {
            collision.transform.root.gameObject.transform.position = collision.transform.position + new Vector3(0f, 0f, -2);
        }
    }

    Vector3 SetZ(Vector3 vector, float z)
    {
        vector.z = z;
        return vector;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "zPos")
        {
            collision.transform.root.gameObject.transform.position += new Vector3(0f, 0f, 2);
        }
    }
}
