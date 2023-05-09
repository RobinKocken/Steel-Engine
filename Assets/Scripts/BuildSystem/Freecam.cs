using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freecam : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float sensitivity = 1f;

    private float yaw = 0f;
    private float pitch = 0f;

    private void Update()
    {
        // Move the camera with WSAD keys
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        transform.position += move * speed * Time.deltaTime;

        // Look around with the mouse
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -85f, 85f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }
}
