using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    public int playerSpeed;
    public float drag;
    public float jumpForce;

    // Input Value from Keys //
    int iForward, iBackwards, iLeft, iRight, iJump;

    // Value for direction of Player //
    int moveZ, moveX;

    public bool jump;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.drag = drag;
    }

    void Update()
    {
        SpeedControl();
        CalculateDirection();
    }

    public void GetKeyInput(int tForward, int tBackwards, int tLeft, int tRight, int tJump)
    {
        iForward = tForward;
        iBackwards = -tBackwards;
        iLeft = -tLeft;
        iRight = tRight;
        iJump = tJump;
    }

    void FixedUpdate()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        rb.AddForce(moveZ * playerSpeed * transform.forward.normalized, ForceMode.VelocityChange);
        rb.AddForce(moveX * playerSpeed * transform.right.normalized, ForceMode.VelocityChange);

        
    }

    void Jump()
    {
        if(iJump == 1 && jump)
        {
            Debug.Log("Jump");
            rb.drag = 0;

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            iJump = 0;
            jump = false;
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        rb.drag = drag;
        jump = true;
    }
}
