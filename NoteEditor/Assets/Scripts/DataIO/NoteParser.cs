using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteParser : MonoBehaviour
{
    public NoteData[] Parse(string _CSVFileName)
    {
        List<NoteData> noteList = new List<NoteData>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);    //파일 읽기

        string[] data = csvData.text.Split(new char[] { '\n' }); // 엔터로 분리

        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });   //id,type,time 로나누기

            NoteData note = new NoteData();
            note.type = System.Convert.ToInt32(row[1]); //type
            note.time_ms = System.Convert.ToInt32(row[2]); //time

            noteList.Add(note);
        }

        return noteList.ToArray();
    }
}
