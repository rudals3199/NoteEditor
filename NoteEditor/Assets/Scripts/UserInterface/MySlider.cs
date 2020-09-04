using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MySlider : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public float minValue = 0;
    public float maxValue = 10;
    public float value = 0;

    [Space]
    [SerializeField]private Transform fill;
    [SerializeField]private Transform handle;

    float realValue_01;
    float realValue_min;
    float realValue_max;    //좌표값


    
    void Start()
    {
        Vector3[] sliderCorners = new Vector3[4];
        RectTransform sliderRect = GetComponent<RectTransform>();
        sliderRect.GetWorldCorners(sliderCorners);

        realValue_min = sliderCorners[0].x;
        realValue_max = sliderCorners[3].x;
    }

    void HandleMove(float targetX)
    {
        if (targetX <= realValue_min)
        {
            handle.position = new Vector2(realValue_min, handle.position.y);
            fill.position = new Vector2(realValue_min, fill.position.y);

            realValue_01 = 0;
            value = minValue;
        }
        else if (realValue_max <= targetX)
        {
            handle.position = new Vector2(realValue_max, handle.position.y);
            fill.position = new Vector2(realValue_max, handle.position.y);

            realValue_01 = 1;
            value = maxValue;
        }
        else
        {
            handle.position = new Vector2(targetX, handle.position.y);
            fill.position = new Vector2(targetX, fill.position.y);

            realValue_01 = (targetX - realValue_min) / (realValue_max - realValue_min);
            value = (maxValue - minValue) * realValue_01 + minValue;
        }

        onValueChange.Invoke(value);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        HandleMove(eventData.position.x);
    }

    public void OnDrag(PointerEventData eventData)
    {
        HandleMove(eventData.position.x);
    }

    
    public void SetValue(float input_value)
    {
        value = Mathf.Clamp(input_value, minValue, maxValue);

        realValue_01 = (value - minValue) / (maxValue - minValue);
        float targetX = realValue_01 * (realValue_max - realValue_min) + realValue_min;

        handle.position = new Vector2(targetX, handle.position.y);
        fill.position = new Vector2(targetX, fill.position.y);
    }


    public onValueChangeEvent onValueChange = new onValueChangeEvent();
}

public class onValueChangeEvent : UnityEvent<float> { }
