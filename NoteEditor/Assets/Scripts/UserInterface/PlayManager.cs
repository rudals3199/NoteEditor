using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour {

    bool isPlaying = false;
    bool audioLoaded = false;

    float audioTime = 0;
    float markedTime = 0;
    

    public Button playButton;
    public Button pauseButton;
    public Button stopButton;
    public Button rewindButton;
    [Space]
    public Button prevBeatButton;
    public Button prevNoteButton;
    public Button nextNoteButotn;
    public Button nextBeatButton;
    

    [Space]
    public NoteSetting noteSetting;
    [Space]
    public AudioController audioController;
    public ScrollController scrollController;
    public SliderController sliderController;

    [Space]
    public AudioExplorer audilLoader;

    private void Awake()
    {
        audilLoader.onAudioChange.AddListener(Initialize);
        
    }

    private void Initialize(float audioLength)
    {
        audioController.SetAudioTime(0, false);
        sliderController.InitializeSlider(0, audioLength);
        noteSetting.InitializeSetting(audioLength);
        if(!audioLoaded)
        {
            playButton.onClick.AddListener(OnPlay);
            pauseButton.onClick.AddListener(OnPause);
            stopButton.onClick.AddListener(OnStop);
            rewindButton.onClick.AddListener(OnRewind);

            prevBeatButton.onClick.AddListener(OnSnapPrevBeat);
            prevNoteButton.onClick.AddListener(OnSnapPrevNote);
            nextNoteButotn.onClick.AddListener(OnSnapNextNote);
            nextBeatButton.onClick.AddListener(OnSnapNextBeat);

            audioController.audioFinishEvent.AddListener(OnFinish);
            sliderController.onSliderChangeEvent.AddListener(OnSetTime);
        }
        audioLoaded = true;
    }


    IEnumerator Playing()
    {
        while(isPlaying)
        {
            audioTime = audioController.Playing();

            scrollController.Scrolling(audioTime);
            sliderController.SetSliderValue(audioTime);

            yield return null;
        }
    }


    void OnPlay()
    {
        isPlaying = true;

        markedTime = audioController.SetPlaying(true);
        StartCoroutine(Playing());
    }

    void OnPause()
    {
        StopAllCoroutines();

        isPlaying = false;
        audioController.SetPlaying(isPlaying);
    }

    void OnStop()
    {
        StopAllCoroutines();

        isPlaying = false;
        audioTime = 0;

        audioController.SetAudioTime(audioTime, isPlaying);
        scrollController.SetScrollPos(audioTime);
        sliderController.SetSliderValue(audioTime);
    }

    void OnRewind()
    {
        audioController.SetAudioTime(markedTime, isPlaying);
        scrollController.SetScrollPos(markedTime);
        sliderController.SetSliderValue(markedTime);
    }

    void OnFinish()
    {
        pauseButton.onClick.Invoke();
    }

    void OnSetTime(float sliderTime)
    {
        audioController.SetAudioTime(sliderTime, isPlaying);
        scrollController.SetScrollPos(sliderTime);
    }


    //snap
    void OnSnapPrevBeat()
    {
        float scrollTime = noteSetting.SnapToGrid(true, false);
        if (scrollTime >= 0)
        {
            audioTime = scrollTime;

            audioController.SetAudioTime(audioTime, isPlaying);
            sliderController.SetSliderValue(audioTime);
        }
    }

    void OnSnapPrevNote()
    {
        float scrollTime = noteSetting.SnapToGrid(false, false);
        if (scrollTime >= 0)
        {
            audioTime = scrollTime;

            audioController.SetAudioTime(audioTime, isPlaying);
            sliderController.SetSliderValue(audioTime);
        }
    }

    void OnSnapNextNote()
    {
        float scrollTime = noteSetting.SnapToGrid(false, true);
        if (scrollTime >= 0)
        {
            audioTime = scrollTime;

            audioController.SetAudioTime(audioTime, isPlaying);
            sliderController.SetSliderValue(audioTime);
        }



    }

    void OnSnapNextBeat()
    {
        float scrollTime = noteSetting.SnapToGrid(true, true);
        if (scrollTime >= 0)
        {
            audioTime = scrollTime;

            audioController.SetAudioTime(audioTime, isPlaying);
            sliderController.SetSliderValue(audioTime);
        }
    }

    

    
}
