using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideButton : MonoBehaviour
{
    public Transform slideTransform;
    public float slideSpeed;
    public Vector2 maxPos;
    public Vector2 currentPos;

    void Start()
    {
        slideTransform.localPosition = currentPos;
    }

    public void ButtonSlideX(float incrementsX)
    {
        if(currentPos.x + incrementsX < -maxPos.x || currentPos.x + incrementsX > maxPos.x)
            return;
        else
        {
            StopCoroutine(SettingsAnimation( -1, -1));
            StartCoroutine(SettingsAnimation(incrementsX, 0));
        }
    }

    public void ButtonSlideY(float incrementsY)
    {
        if(currentPos.y + incrementsY < -maxPos.y || currentPos.y + incrementsY > maxPos.y)
            return;
        else
        {
            StopCoroutine(SettingsAnimation(-1, -1));
            StartCoroutine(SettingsAnimation(0, incrementsY));
        }
    }

    IEnumerator SettingsAnimation(float posX, float posY)
    {
        bool run = true;
        Vector3 targetPos = new Vector3(currentPos.x + posX, currentPos.y + posY, 0);
        currentPos = targetPos;

        while(run)
        {
            slideTransform.localPosition = Vector2.MoveTowards(slideTransform.localPosition, targetPos, slideSpeed * Time.deltaTime);

            if(slideTransform.localPosition == targetPos)
                run = false;

            yield return new WaitForEndOfFrame();
        }
    }
}
