using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NoteGeneratorByFile))]
public class NoteGenerateButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        NoteGeneratorByFile generator = (NoteGeneratorByFile)target;
        if (GUILayout.Button("균일 노트 생성"))
        {
            generator.GenerateSteadyNotes();
        }

        if (GUILayout.Button("비균일 노트 생성"))
        {
            generator.GenerateNotes();
        }

    }
}