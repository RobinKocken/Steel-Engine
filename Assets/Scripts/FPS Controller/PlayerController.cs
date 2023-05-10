using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public Transform orientation;

    [Header("Player Variables")]
    public int playerSpeed;
    public float drag;
    public float jumpForce;
    public float airMultiplier;

    // Input Value from Keys //
    int iForward, iBackwards, iLeft, iRight, iJump;

    // Value for direction of Player //
    int moveZ, moveX;

    // Spherecast Variables // 
    [Header("Spherecast")]
    public float sphereRadius;
    public float rayDistance;
    RaycastHit groundHit;

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

    void Update()
    {
        CheckIfJump();
        SpeedControl();
        CalculateDirection();
        CheckIfGrounded();
    }

    void FixedUpdate()
    {
        Movement();
    }

    public void GetKeyInput(int tForward, int tBackwards, int tLeft, int tRight, int tJump)
    {
        iForward = tForward;
        iBackwards = -tBackwards;
        iLeft = -tLeft;
        iRight = tRight;
        iJump = tJump;
    }

    void Movement()
    {
        if(grounded)
        {
            rb.AddForce(moveZ * playerSpeed * orientation.forward.normalized, ForceMode.VelocityChange);
            rb.AddForce(moveX * playerSpeed * orientation.right.normalized, ForceMode.VelocityChange);
        }
        else if(!grounded)
        {
            rb.AddForce(moveZ * playerSpeed * airMultiplier * orientation.forward.normalized, ForceMode.VelocityChange);
            rb.AddForce(moveX * playerSpeed * airMultiplier * orientation.right.normalized, ForceMode.VelocityChange);
        }
    }

    void CheckIfJump()
    {
        if(iJump == 1 && grounded && readyToJump)
        {
            Jump();
        }
        else if(iJump == 0 && grounded && !readyToJump)
        {
            ResetJump();
        }
    }

    void Jump()
    {
        readyToJump = false;

        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(orientation.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if(flatVel.magnitude > playerSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * playerSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void CalculateDirection()
    {
        moveZ = iForward + iBackwards;
        moveX = iLeft + iRight;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawRay(transform.position, -transform.up * rayDistance);
        Gizmos.DrawWireSphere(transform.position + -transform.up * rayDistance, sphereRadius);
    }
}
