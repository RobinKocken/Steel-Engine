using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform orientation;

    public float targetDistance;

    // Input values of the Mouse //
    public float mouseX;
    public float mouseY;

    // Rotation Values //
    float xRotation;
    float yRotation;

    void Start()
    {
        targetDistance = Vector3.Distance(transform.position, orientation.transform.position);
    }

    void Update()
    {
        FPCamera();
        //TPCamera();
    }

    // FPS Camera Player //
    void FPCamera()
    {
        mouseX = Input.GetAxis("Mouse X") * OptionManager.playerMouseSens;
        mouseY = Input.GetAxis("Mouse Y") * OptionManager.playerMouseSens;

        xRotation += -mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.localRotation = Quaternion.Euler(0, yRotation, 0);
    }

    void TPCamera()
    {
        mouseX = Input.GetAxis("Mouse X") * OptionManager.playerMouseSens;
        mouseY += Input.GetAxis("Mouse Y") * OptionManager.playerMouseSens;

        xRotation = Mathf.Clamp(mouseY, -90, 90);

        transform.eulerAngles = new Vector3(-xRotation, transform.eulerAngles.y + mouseX, 0);
        transform.position = orientation.transform.position - (transform.forward * targetDistance);
    }
}
