using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NoteParser))]
public class NoteDatabase : MonoBehaviour
{
    //첫 시작시 resource폴더안의 한 csv파일에서 노트패턴을 가져오는 클래스
    //즉, csv파일마다 NoteDataBase가 필요함

    public static NoteDatabase instance;

    [SerializeField] string csv_FileName;
    Dictionary<int, NoteData> noteDic = new Dictionary<int, NoteData>();

    public bool loadFinish = false;

    public void LoadNotePattern()
    {
        if (csv_FileName == "")
        {
            Debug.Log("파일 이름을 적어주세요.");
            return;
        }

        noteDic.Clear();

        NoteParser theParser = GetComponent<NoteParser>();
        NoteData[] notes = theParser.Parse(csv_FileName);
        for (int i = 0; i < notes.Length; i++)
        {
            noteDic.Add(i + 1, notes[i]);
        }
        loadFinish = true;
    }

    public NoteData[] GetNotePattern()
    {
        List<NoteData> note_List = new List<NoteData>();
        for (int i = 0; i < noteDic.Count; i++)
        {
            note_List.Add(noteDic[i + 1]);
        }
        return note_List.ToArray();
    }
}

