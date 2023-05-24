using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BaseController : MonoBehaviour
{
    Rigidbody rb;
    public Animator animator;

    // Input Value for Keys //
    int iForward, iBackwards, iLeft, iRight;

    // Value for direction of Base //
    int moveZ, moveX;

    public void GetPlayerKeyInput(KeyCode kForward, KeyCode kBackwards, KeyCode kLeft, KeyCode kRight)
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
    }

    void CalculateInputDirection()
    {
        moveZ = iForward + iBackwards;
        moveX = iLeft + iRight;
    }
}
