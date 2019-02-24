using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform Target;
    public float HorizontalMargin = 100;
    public float VerticalMargin = 100;
    public float LerpSpeed = 3;

    public float DampTime = 0.3f;

    public float VerticalOffset = 1;
    
    private Vector3 ScreenPos;
    private Vector3 Velocity;
    
    protected float GetX()
    {
        if (ScreenPos.x > HorizontalMargin && ScreenPos.x < Screen.width - HorizontalMargin)
            return transform.position.x;
        return Mathf.Lerp(transform.position.x, Target.position.x, Time.deltaTime * LerpSpeed);
    }

    protected float GetY()
    {
        if (ScreenPos.y > VerticalMargin && ScreenPos.y < Screen.height - VerticalMargin)
            return transform.position.y;
        return Mathf.Lerp(transform.position.y, Target.position.y, Time.deltaTime * LerpSpeed);
    }

    private void Update()
    {
        ScreenPos = Camera.main.WorldToScreenPoint(Target.position);
        
        var delta = Target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, ScreenPos.z));
        var destination = transform.position + delta + new Vector3(0,VerticalOffset,0);
        
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref Velocity, DampTime);
    }
}
