using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewFile : MonoBehaviour {

    bool isEnable = false;
    public Button newfileButton;
    public NoteGenerator noteGenerator;
    public AudioExplorer audioLoader;

    void OnEnable()
    {
        newfileButton.interactable = false;

        audioLoader.onAudioChange.AddListener(SetEnableButton);
        newfileButton.onClick.AddListener(ClearNotes);
    }

    void SetEnableButton(float aL)
    {
        if(!isEnable)
        {
            isEnable = true;
            newfileButton.interactable = true;
        }
    }

    void ClearNotes()
    {
        noteGenerator.ClearPattern();
    }
}
