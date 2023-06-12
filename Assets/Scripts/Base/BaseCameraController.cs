using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BaseCameraController : MonoBehaviour
{
    public Transform orientation;

    public float targetDistance;

    // Input values of the Mouse //
    float mouseX;
    float mouseY;

    // Rotation Values //
    float xRotation;
    float yRotation;

    void Start()
    {
        InitializeCamera();
    }

    void InitializeCamera()
    {
        //targetDistance = Vector3.Distance(transform.position, orientation.transform.position);
    }

    void Update()
    {
        TPCamera();   
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
