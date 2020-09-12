using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour {


    [Header("Requirements")]
    public GameObject barPrefab;    //1마디
    public GameObject beatPrefab;   //1박
    public GameObject notePrefab;  //1/8박 = 32분음표
    [Space]
    public Transform barHolder;
    public Transform beatHolder;
    public Transform[] noteHolders;

    //생성
    public void GenerateBase(float bpm, int interval, int beatPerBar, float audioLength)    //개수 계산해서 만들기
    {
        float offset = 0;
        int beatCount = Mathf.CeilToInt((bpm / 60f) * audioLength);

        if(beatCount+1 > barHolder.childCount)  // 새로만들것 > 기존것 --> 추가생성
        {
            for (int i = barHolder.childCount; i < beatCount + 1; i++)
            {
                var barInstance = Instantiate(barPrefab, barHolder);
                barInstance.transform.localPosition = Vector3.right * (i * interval + offset);

                var beatInstance = Instantiate(beatPrefab, beatHolder);
                beatInstance.transform.localPosition = Vector3.right * (i * interval + offset);

                if (i % beatPerBar == 0)
                    beatInstance.gameObject.SetActive(false);
                else
                    barInstance.gameObject.SetActive(false);
            }

            for(int i = noteHolders[0].childCount; i < beatCount*8 + 1; i++)
            {
                foreach (Transform noteholder in noteHolders)
                {
                    var eightBeatInstance = Instantiate(notePrefab, noteholder);
                    eightBeatInstance.transform.localPosition = Vector3.right * (i * (interval / 8f) + offset);
                }  
            }
        }
        else        // 새로만들것 < 기존것 --> 여분 삭제
        {
            for(int i = barHolder.childCount-1; i> beatCount; i--)
            {
                DestroyImmediate(barHolder.GetChild(i).gameObject);
                DestroyImmediate(beatHolder.GetChild(i).gameObject);
            }
            for(int i = noteHolders[0].childCount-1; i>beatCount*8; i--)
            {
                foreach(Transform noteholder in noteHolders)
                    DestroyImmediate(noteholder.GetChild(i).gameObject);
            }
        }
    }

    public void GenerateNote(int holder)  //activator 호출
    {
        float snapDistanceX = Mathf.Abs(noteHolders[holder].GetChild(0).transform.position.x - noteHolders[holder].GetChild(1).transform.position.x);
        int index = Mathf.RoundToInt(-transform.position.x / snapDistanceX);

        noteHolders[holder].GetChild(index).GetComponentInChildren<Note>().SetEnable(true);
    }

    public void EraseNote(int holder)
    {
        float snapDistanceX = Mathf.Abs(noteHolders[holder].GetChild(0).transform.position.x - noteHolders[holder].GetChild(1).transform.position.x);
        int index = Mathf.RoundToInt(-transform.position.x / snapDistanceX);

        noteHolders[holder].GetChild(index).GetComponentInChildren<Note>().SetEnable(false);
    }

    public void ClearPattern()
    {
        for(int i = 0; i< noteHolders.Length; i++)
        {
            for(int j = 0; j<noteHolders[i].childCount; j++)
            {
                noteHolders[i].GetChild(j).GetComponentInChildren<Note>().SetEnable(false);
            }
        }
    }


    //재배치

    public void ReArrangeAll(int interval0, int interval1)
    {
        float interval_var = (float)interval1 / interval0;
        transform.position = new Vector3(transform.position.x * interval_var, transform.position.y);

        Transform target;
        for (int i = 0; i < barHolder.childCount; i++)
        {
            target = barHolder.GetChild(i);
            target.localPosition = new Vector3(target.localPosition.x * interval_var
                                            , target.localPosition.y);
        }
        for (int i = 0; i < beatHolder.childCount; i++)
        {
            target = beatHolder.GetChild(i);
            target.localPosition = new Vector3(target.localPosition.x * interval_var
                                            , target.localPosition.y);
        }
        for (int i = 0; i < noteHolders[0].childCount; i++)
        {
            foreach(Transform holder in noteHolders)
            {
                target = holder.GetChild(i);
                target.localPosition = new Vector3(target.localPosition.x * interval_var
                                                , target.localPosition.y);
            }
            
        }
    }

    public void ReArrangeBase(int beatPerBar0, int beatPerBar1)
    {
        for(int i = 0; i<barHolder.childCount; i++)
        {
            if((i % beatPerBar0 == 0) && (i % beatPerBar1 == 0))
            {
                continue;
            }
            else if(i % beatPerBar0 == 0)
            {
                barHolder.GetChild(i).gameObject.SetActive(false);
                beatHolder.GetChild(i).gameObject.SetActive(true);
            }
            else if(i % beatPerBar1 == 0)
            {
                barHolder.GetChild(i).gameObject.SetActive(true);
                beatHolder.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                continue;
            }
            
        }
    }

    //좌표값을 얻어와야해서 어쩔수없이 여기에 배치
    public bool SnapToGrid(bool toBeat_NotNote, bool toNext)
    {
        float snapDistanceX;
        int index;

        if (toBeat_NotNote)
            snapDistanceX = Mathf.Abs(beatHolder.GetChild(0).transform.position.x - beatHolder.GetChild(1).transform.position.x);
        else
            snapDistanceX = Mathf.Abs(noteHolders[0].GetChild(0).transform.position.x - noteHolders[0].GetChild(1).transform.position.x);

        if (toNext)
            index = Mathf.FloorToInt(-transform.position.x / snapDistanceX) + 1;
        else
            index = Mathf.CeilToInt(-transform.position.x / snapDistanceX) - 1;

        if (toBeat_NotNote)
        {
            if(0<= index && index < beatHolder.childCount)
            {
                transform.position = new Vector3(snapDistanceX * index, transform.position.y);
                return true;
            }
            else
                return false;
        }
        else
        {
            if(0<= index && index < noteHolders[0].childCount)
            {
                transform.position = new Vector3(snapDistanceX * index, transform.position.y);
                return true;
            }
            else
                return false;
        }
    }


    public void GeneratePattern(float speed, SaveData[] saveData)
    {
        float snapDistanceX = Mathf.Abs(noteHolders[0].GetChild(0).transform.position.x - noteHolders[0].GetChild(1).transform.position.x);

        foreach (SaveData note in saveData)
        {
            float xPos = (note.time_ms / 1000f) * speed;
            int index = Mathf.RoundToInt(xPos/ snapDistanceX);

            for(int i= 0; i< noteHolders.Length; i++)
            {
                if ((note.code & 1 << i) != 0)
                    noteHolders[i].GetChild(index).GetComponentInChildren<Note>().SetEnable(true);
            }
        }
    }
}
