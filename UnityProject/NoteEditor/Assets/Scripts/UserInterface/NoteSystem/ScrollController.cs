using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollController : MonoBehaviour
{

    float speed;
    Vector2 origPos;

    private void Start()
    {
        origPos = transform.position;
    }

    public void Initialize(float bpm, int interval)
    {
        SetSpeed(bpm, interval);
        SetScrollPos(0);
    }

    public void SetSpeed(float bpm, int interval)
    {
        speed = interval * (bpm / 60f);
    }


    public void Scrolling(float audioTime)
    {
        Vector3 scrollPos = (Vector2.left * audioTime * speed) + origPos;
        transform.Translate(scrollPos - transform.position);
    }

    public void SetScrollPos(float inputTime)
    {
        transform.position = new Vector2(-inputTime * speed, 0) + origPos;
    }

    public float GetScrollPosToTime()
    {
        float scrollTime = Mathf.Abs(transform.localPosition.x / speed);
        return scrollTime;
    }
   
    public float GetScrollPosToTime(float notePos)
    {
        return Mathf.Abs(notePos / speed);
    }

}