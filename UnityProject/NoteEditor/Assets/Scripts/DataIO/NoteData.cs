[System.Serializable]
public class NoteData
{
    public int type;
    public int time_ms;    //노트의 위치를 비트로 표시한것
}

[System.Serializable]
public class NotePattern
{
    public string name;
    public NoteData[] notes;
}

[System.Serializable]
public class SaveData
{
    public int time_ms;
    public int code;

}