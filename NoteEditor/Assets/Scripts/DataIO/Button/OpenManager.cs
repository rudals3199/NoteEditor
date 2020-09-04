using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;
using System.IO;

public class OpenManager : MonoBehaviour {


    bool isEnable = false;

    public Button openFileButton;
    public NoteSetting noteSetting;
    public AudioExplorer audioLoader;

    private void OnEnable()
    {
        audioLoader.onAudioChange.AddListener(SetEnableButton);
        openFileButton.onClick.AddListener(OpenExplorer);
    }

    void SetEnableButton(float al)
    {
        if (!isEnable)
        {
            isEnable = true;
            openFileButton.interactable = true;
        }
            
    }


    void OpenExplorer()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Text", ".txt"));
        FileBrowser.SetDefaultFilter(".txt");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);

        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(false, true, null, "Load File", "Load");
        if (FileBrowser.Success)
        {
            noteSetting.OpenNotePattern(Parse(FileBrowser.Result[0]));
        }
        else
        {
            openFileButton.interactable = true;
        }
    }

    SaveData[] Parse(string path)
    {
        List<SaveData> noteList = new List<SaveData>();
        string[] data = File.ReadAllLines(path);

        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });   //time,code 로나누기

            SaveData note = new SaveData();
            note.time_ms = System.Convert.ToInt32(row[0]); //time
            note.code = System.Convert.ToInt32(row[1]); //code

            noteList.Add(note);
        }

        return noteList.ToArray();
    }

}
