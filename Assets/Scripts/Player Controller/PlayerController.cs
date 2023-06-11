using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public Transform orientation;
    Vector3 moveDir;

    [Header("Player Variables")]
    public float playerCurrentSpeed;
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

    [Header("Raycast")]
    public float raycastDistance;
    RaycastHit slopHit;

    // Some Bools //
    bool grounded;
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
        CheckIfGrounded();
        CheckIfJump(kJump);
        Run(kSprint);
        SpeedControl();
    }

    void CalculateInputDirection()
    {
        moveZ = iForward + iBackwards;
        moveX = iLeft + iRight;
    }

    void Movement()
    {
        moveDir = orientation.forward * moveZ + orientation.right * moveX;        

        if(grounded)
        {
            rb.AddForce(playerCurrentSpeed * GetSlopeDir(), ForceMode.VelocityChange);
        }
        else if(!grounded)
        {
            rb.AddForce(playerCurrentSpeed * airMultiplier * moveDir.normalized, ForceMode.VelocityChange);
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
            playerCurrentSpeed = playerRunSpeed;
        else
            playerCurrentSpeed = playerMoveSpeed;
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

        if(flatVel.magnitude > playerCurrentSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * playerCurrentSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void InitializePlayer()
    {
        rb.drag = drag;
    }

    Vector3 GetSlopeDir()
    {
        Physics.Raycast(transform.position, Vector3.down, out slopHit, raycastDistance);
        return Vector3.ProjectOnPlane(moveDir, slopHit.normal).normalized;
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

        Debug.DrawRay(transform.position, Vector3.down * raycastDistance, Color.green);
    }
}
