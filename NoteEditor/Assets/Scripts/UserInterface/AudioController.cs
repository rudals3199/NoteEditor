using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AudioController : MonoBehaviour {

    public float audioTime = 0;


    [Header("Requirements")]
    public AudioSource audioSource;


    public float Playing()
    {
        if (!audioSource.isPlaying)
        {
            audioFinishEvent.Invoke();
        }
        audioTime = audioSource.time;
        return audioTime;
    }

    public float SetPlaying(bool flag)
    {
        if (flag)
        {
            audioSource.Play();
            audioTime = audioSource.time;
        }
        else
        {
            audioSource.Pause();
        }

        return audioTime;
    }

    public void SetAudioTime(float time, bool playFlag)
    {
        audioTime = Mathf.Clamp(time, 0, audioSource.clip.length);

        if (audioTime >= audioSource.clip.length)
        {
            audioFinishEvent.Invoke();
            audioSource.time = audioSource.clip.length-0.1f;
        }
        else
        {
            audioSource.time = audioTime;
        }

        SetPlaying(playFlag);
    }

    
    public UnityEvent audioFinishEvent = new UnityEvent();
}

