using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SliderController : MonoBehaviour {

    //output
    float sliderTime = 0;

    public Text timeText;
    public MySlider timeSlider;

    private void Awake()
    {
        timeSlider.onValueChange.AddListener(GetSliderValue);
    }

    public void InitializeSlider(float minValue, float maxValue)
    {
        timeSlider.minValue = minValue;
        timeSlider.maxValue = maxValue;

        SetSliderValue(0);
    }

    public void GetSliderValue(float value)
    {
        sliderTime = value;
        timeText.text = ((int)(sliderTime / 60)).ToString("00:") + (sliderTime % 60).ToString("00.00");

        onSliderChangeEvent.Invoke(sliderTime);
    }

    public void SetSliderValue(float input_time)   //audio로부터 받아와야함
    {
        sliderTime = input_time;
        timeText.text = ((int)(sliderTime / 60)).ToString("00:") + (sliderTime % 60).ToString("00.00");
        timeSlider.SetValue(sliderTime);
    }


    public OnSliderChangeEvent onSliderChangeEvent = new OnSliderChangeEvent();
}

public class OnSliderChangeEvent : UnityEvent<float> { }