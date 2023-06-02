using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BaseController : MonoBehaviour
{
    public GameManager gameManager;

    public Animator animator;

    public Transform target;
    public Transform wheel;

    public bool letRot;

    [Header("Base Movement")]
    public float maxForwardSpeed;
    public float currentSpeed;
    public float speedBuildUp;

    [Header("Base Steering")]
    public float maxDegreesPerSec;
    public float currentDegreesPerSec;
    public float degreesBuildUpPerSec;
    public float targetDistance;

    [Header("Base Height")]
    public float height;
    public float lerpSpeed;

    [Header("Raycasts")]
    public Transform frontLeft, frontRight;
    public Transform rearLeft, rearRight;
    public LayerMask layerMask;

    // Input Value for Keys //
    int iForward, iBackwards, iLeft, iRight;
    // Value for direction of Base //
    public int moveZ, moveX;

    void Start()
    {
        target.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + -targetDistance);
    }

    void Update()
    {
        if(letRot)
            Raycasts();
        
        BaseMovement();
    }

    public void GetBaseKeyInput(KeyCode kForward, KeyCode kBackwards, KeyCode kLeft, KeyCode kRight, KeyCode kCamSwitch, KeyCode kInteraction)
    {
        if(Input.GetKeyDown(kForward))
            iForward = 1;
        else if(Input.GetKeyUp(kForward))
            iForward = 0;

        if(Input.GetKeyDown(kBackwards))
            iBackwards = -1;
        else if(Input.GetKeyUp(kBackwards))
            iBackwards = 0;

        if(Input.GetKeyDown(kLeft))
            iLeft = -1;
        else if(Input.GetKeyUp(kLeft))
            iLeft = 0;

        if(Input.GetKeyDown(kRight))
            iRight = 1;
        else if(Input.GetKeyUp(kRight))
            iRight = 0;

        if(Input.GetKeyDown(kInteraction))
            gameManager.SwitchStatePlayer(GameManager.PlayerState.player);

        CalculateInputDirection();
    }

    void CalculateInputDirection()
    {
        moveZ = iForward + iBackwards;
        moveX = iLeft + iRight;
    }

    void BaseMovement()
    {
        if(moveZ == 1)
        {
            if(currentSpeed < maxForwardSpeed)
                currentSpeed += (speedBuildUp + currentSpeed / 4) * Time.deltaTime;
            else if(currentSpeed >= maxForwardSpeed)
                currentSpeed = maxForwardSpeed;
        }
        else if(moveZ == -1)
        {
            if(currentSpeed > 0)
                currentSpeed -= (speedBuildUp + currentSpeed / 6) * Time.deltaTime;
            else if(currentSpeed <= 0)
                currentSpeed = 0;            
        }

        transform.Translate(currentSpeed * Time.deltaTime * -Vector3.forward);
    }

    void BaseRotation()
    {
        if(moveX == 1)
        {
            if(currentDegreesPerSec < maxDegreesPerSec)
                currentDegreesPerSec += (degreesBuildUpPerSec + Mathf.Abs(currentDegreesPerSec) / 8) * Time.deltaTime;
            else if(currentDegreesPerSec >= maxDegreesPerSec)
                currentDegreesPerSec = maxDegreesPerSec;

            Wheel(1);
        }
        else if(moveX == -1)
        {
            if(currentDegreesPerSec > -maxDegreesPerSec)
                currentDegreesPerSec -= (degreesBuildUpPerSec + Mathf.Abs(currentDegreesPerSec) / 8) * Time.deltaTime;
            else if(currentDegreesPerSec <= -maxDegreesPerSec)
                currentDegreesPerSec = -maxDegreesPerSec;

            Wheel(-1);
        }

        target.eulerAngles = new Vector3(0, target.eulerAngles.y + currentDegreesPerSec * Time.deltaTime, 0);
        target.position = transform.position - (target.forward * targetDistance);       

        Vector3 forwardDir = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(-new Vector3(forwardDir.x, 0, forwardDir.z), transform.up);
    }

    void Wheel(float rot)
    {
        wheel.Rotate(0, 0, wheel.eulerAngles.z + rot * Time.deltaTime);

        //Vector3 clampRot = wheel.localEulerAngles;
        //clampRot.z = Mathf.Clamp(wheel.localEulerAngles.z, -350, 350);
        //wheel.localEulerAngles = new Vector3(0, 0, clampRot.z);
    }

    void Raycasts()
    {
        Physics.Raycast(frontLeft.position + Vector3.up, Vector3.down, out RaycastHit hitFrontLeft, layerMask);
        Physics.Raycast(frontRight.position + Vector3.up, Vector3.down, out RaycastHit hitFrontRight, layerMask);

        Physics.Raycast(rearLeft.position + Vector3.up, Vector3.down, out RaycastHit hitRearLeft,layerMask);
        Physics.Raycast(rearRight.position + Vector3.up, Vector3.down, out RaycastHit hitRearRight, layerMask);

        Vector3 center = (hitFrontLeft.point + hitFrontRight.point + hitRearLeft.point + hitRearRight.point) / 4;

        Vector3 a = hitRearRight.point - hitRearLeft.point;
        Vector3 b = hitFrontRight.point - hitRearRight.point;
        Vector3 c = hitFrontLeft.point - hitFrontRight.point;
        Vector3 d = hitRearRight.point - hitFrontLeft.point;

        Vector3 crossBA = Vector3.Cross(b, a);
        Vector3 crossCB = Vector3.Cross(c, b);
        Vector3 crossDC = Vector3.Cross(d, c);
        Vector3 crossAD = Vector3.Cross(a, d);

        Vector3 newUp = (crossBA + crossCB + crossDC + crossAD).normalized;
        transform.up = Vector3.Lerp(transform.up, newUp, lerpSpeed * Time.deltaTime);

        BaseRotation();

        transform.position = new Vector3(transform.position.x, height + center.y, transform.position.z);

        Debug.DrawRay(hitFrontLeft.point, Vector3.up);
        Debug.DrawRay(hitFrontRight.point, Vector3.up);
        Debug.DrawRay(hitRearLeft.point, Vector3.up);
        Debug.DrawRay(hitRearRight.point, Vector3.up);
    }
}
