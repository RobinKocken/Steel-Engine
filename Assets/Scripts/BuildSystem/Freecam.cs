using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCam : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private Vector3 targetOffset = Vector3.zero;

    [SerializeField] private float yaw = 0f;
    [SerializeField] private float pitch = 0f;
    [SerializeField] private float distance = 5f;

    private void Update()
    {
        // Rotate the camera with A and D keys
        float rotation = Input.GetAxis("Horizontal") * sensitivity;
        yaw += rotation;

        // Zoom in and out with W and S keys
        float zoom = Input.GetAxis("Vertical") * zoomSpeed;
        distance = Mathf.Clamp(distance - zoom, minDistance, maxDistance);

        // Position the camera using polar coordinates
        float radYaw = Mathf.Deg2Rad * yaw;
        float radPitch = Mathf.Deg2Rad * pitch;
        float x = distance * Mathf.Sin(radYaw) * Mathf.Cos(radPitch);
        float y = distance * Mathf.Sin(radPitch);
        float z = distance * Mathf.Cos(radYaw) * Mathf.Cos(radPitch);

        Vector3 position = new Vector3(x, y, z) + targetOffset;
        transform.position = position;

        // Look at the central point
        transform.LookAt(targetOffset);
    }
}
