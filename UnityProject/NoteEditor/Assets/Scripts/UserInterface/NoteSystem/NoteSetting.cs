using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSetting : MonoBehaviour
{
    float bpm = 92.5f;  // [b/m] = (1/60)[b/s]                  //1박자 =  1 사분음표
    int interval = 4;   // [m/b]                                //1박의 거리
                        // scrollspeed: [m/s] = [m/b] * [b/s]
    int beatPerBar = 4; //마디당 박자수

    [Header("References")]
    public ScrollController scrollController;
    public NoteGenerator noteGenerator;


    public void InitializeSetting(float audioLength)
    {
        noteGenerator.GenerateBase(bpm, interval, beatPerBar, audioLength);
        scrollController.Initialize(bpm, interval);
    }

    public void SetBpm(string input_bpm)
    {
        float parseResult;
        if (float.TryParse(input_bpm, out parseResult))
        {
            bpm = parseResult;

            scrollController.SetSpeed(bpm, interval);
        }
    }

    public void SetInterval(string input_interval)
    {
        int parseResult;
        if(int.TryParse(input_interval, out parseResult))
        {
            noteGenerator.ReArrangeAll(interval, parseResult);
            interval = parseResult;

            scrollController.SetSpeed(bpm, interval);
        }
    }

    public void SetBeatPerBar(string input_beatPerBar)
    {
        int parseResult;
        if (int.TryParse(input_beatPerBar, out parseResult))
        {
            noteGenerator.ReArrangeBase(beatPerBar, parseResult);
            beatPerBar = parseResult;
        }
    }


    public float SnapToGrid(bool toBeat_NotNote, bool toNext)
    {
        if (noteGenerator.SnapToGrid(toBeat_NotNote, toNext))
            return scrollController.GetScrollPosToTime();
        else
            return -1;
    }

    public void OpenNotePattern(SaveData[] saveData)
    {
        float speed = interval * (bpm / 60f);
        noteGenerator.GeneratePattern(speed, saveData);
    }
}
