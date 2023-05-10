using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform orientation;

    // Input values of the Mouse //
    float mouseX;
    float mouseY;

    // Rotation Values //
    float xRotation;
    float yRotation;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        FPSCamera();
    }

    void FPSCamera()
    {
        mouseX = Input.GetAxis("Mouse X") * Options.playerMouseSens * 100 * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * Options.playerMouseSens * 100 * Time.deltaTime;

        xRotation += -mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        orientation.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
}
