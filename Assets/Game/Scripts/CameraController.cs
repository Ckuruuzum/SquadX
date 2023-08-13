using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Vector3 maxXZLimits = new Vector2(10.0f, 10.0f); // Adjust these values as needed

    private Vector3 dragOrigin;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = Input.mousePosition - dragOrigin;
            Vector3 newPosition = transform.position - new Vector3(difference.x, 0, difference.y) * Time.deltaTime * moveSpeed;

            newPosition.x = Mathf.Clamp(newPosition.x, -maxXZLimits.x, maxXZLimits.x);
            newPosition.z = Mathf.Clamp(newPosition.z, -maxXZLimits.y, maxXZLimits.y);

            transform.position = newPosition;

            dragOrigin = Input.mousePosition;
        }
    }
}
