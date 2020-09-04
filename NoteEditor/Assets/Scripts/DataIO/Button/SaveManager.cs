using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveManager : MonoBehaviour {

    bool saving = false;
    string audioName;
    string audioPath;


    Dictionary<int, int> notePattern = new Dictionary<int, int>();

    public Button saveButton;

    public ScrollController scrollController;
    public NoteGenerator noteGenerator;
    public AudioExplorer audioLoader;

    

    private void OnEnable()
    {
        saveButton.onClick.AddListener(ReadNoteData);
        audioLoader.onAudioLoaded.AddListener(SetButtonEnable);
    }

    void SetButtonEnable(string name, string path)
    {
        audioName = name;
        audioPath = path;

        if (saving)
            return;
        else
            saveButton.interactable = true;
    }
    

    void ReadNoteData()  //변수이름 정리좀;
    {
        if (saving)
            return;
        else
            saving = true;

        saveButton.interactable = false;
        notePattern.Clear();
        int holdersCount = noteGenerator.noteHolders.Length;
        int eightBeatCount = noteGenerator.noteHolders[0].childCount;

        for (int i = 0; i < eightBeatCount; i++)
        {
            for (int j = 0; j< holdersCount; j++)
            {
                Note note = noteGenerator.noteHolders[j].GetChild(i).GetComponentInChildren<Note>();
                if (note.isEnabled)
                {
                    int time = (int)(scrollController.GetScrollPosToTime(note.transform.parent.localPosition.x) * 1000);
                    if (!notePattern.ContainsKey(time))
                    {
                        notePattern.Add(time, 0);
                    }            

                    notePattern[time] |= 1 << j;
                }
            }
        }

        DeParse();
    }

    void DeParse()
    {
        string path = audioPath + audioName + "_NotePattern.txt";
        StreamWriter streamWriter = new StreamWriter(path);

        streamWriter.WriteLine("TIME[s]" + "," + "LINE(total:4)[binary]");

        foreach (KeyValuePair<int,int> data in notePattern)
        {
            string dd = data.Key.ToString() + "," + data.Value.ToString();
            streamWriter.WriteLine(dd);
        }

        streamWriter.Flush();   //버퍼삭제
        streamWriter.Close();
        

        saving = false;
        saveButton.interactable = true;
    }
}
