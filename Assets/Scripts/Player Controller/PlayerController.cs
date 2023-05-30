using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public BaseController baseController;
    public Transform orientation;

    [Header("Player Variables")]
    public float playerMaxSpeed;
    public float playerMoveSpeed;
    public float playerRunSpeed;
    public float drag;
    public float jumpForce;
    public float airMultiplier;
    public float baseSpeed;

    // Input Value for Keys //
    int iForward, iBackwards, iLeft, iRight;

    // Value for direction of Player //
    int moveZ, moveX;

    // Spherecast Variables // 
    [Header("Spherecast")]
    public float sphereRadius;
    public float rayDistance;
    RaycastHit groundHit;

    // Some Bools //
    public bool grounded;
    bool readyToJump;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        InitializePlayer();
    }

    void FixedUpdate()
    {
        Movement();
    }

    public void GetPlayerKeyInput(KeyCode kForward, KeyCode kBackwards, KeyCode kLeft, KeyCode kRight, KeyCode kSprint ,KeyCode kJump)
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

        CalculateInputDirection();
        CheckIfJump(kJump);
        Run(kSprint);
        SpeedControl();
        CheckIfGrounded();
    }

    void CalculateInputDirection()
    {
        moveZ = iForward + iBackwards;
        moveX = iLeft + iRight;
    }

    void Movement()
    {
        if(baseController != null)
            baseSpeed = baseController.maxForwardSpeed;

        if(grounded)
        {
            rb.AddForce(moveZ * (playerMaxSpeed + baseSpeed) * orientation.forward.normalized, ForceMode.VelocityChange);
            rb.AddForce(moveX * (playerMaxSpeed + baseSpeed) * orientation.right.normalized, ForceMode.VelocityChange);
        }
        else if(!grounded)
        {
            rb.AddForce(moveZ * (playerMaxSpeed + baseSpeed) * airMultiplier * orientation.forward.normalized, ForceMode.VelocityChange);
            rb.AddForce(moveX * (playerMaxSpeed + baseSpeed) * airMultiplier * orientation.right.normalized, ForceMode.VelocityChange);
        }
    }

    void CheckIfJump(KeyCode jump)
    {
        if(Input.GetKeyDown(jump) && grounded && readyToJump)
        {
            Jump();
        }
        else if(grounded && !readyToJump)
        {
            readyToJump = true;
        }
    }

    void Run(KeyCode run)
    {
        if(Input.GetKey(run))
            playerMaxSpeed = playerRunSpeed;
        else
            playerMaxSpeed = playerMoveSpeed;
    }

    void Jump()
    {
        readyToJump = false;

        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(orientation.up * jumpForce, ForceMode.Impulse);
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if(flatVel.magnitude > playerMaxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * playerMaxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void InitializePlayer()
    {
        rb.drag = drag;
    }

    void CheckIfGrounded()
    {
        if(Physics.SphereCast(transform.position, sphereRadius, -transform.up, out groundHit, rayDistance))
        {
            grounded = true;
            rb.drag = drag;
        }
        else
        {
            grounded = false;
            rb.drag = 0;
        }
    }

    public void StopMovement()
    {
        moveZ = 0;
        moveX = 0;
        iForward = 0;
        iBackwards = 0;
        iLeft = 0;
        iRight = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawRay(transform.position, -transform.up * rayDistance);
        Gizmos.DrawWireSphere(transform.position + -transform.up * rayDistance, sphereRadius);
    }
}
