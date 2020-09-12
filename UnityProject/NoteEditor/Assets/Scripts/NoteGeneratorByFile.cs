using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NoteDatabase))]
public class NoteGeneratorByFile : MonoBehaviour {

    [SerializeField] private GameObject[] notePrefabs;

    [SerializeField] private float dist_per_bit;



    [Header("비균일노트생성 변수")] //비균일노트생성용
    public float bpm = 100;

    NoteDatabase noteDatabase;
    [SerializeField]NotePattern notePattern;


    public void GenerateSteadyNotes()
    {
        //if (noteHolders[0].childCount != 0)
        //{
        //    for (int j = noteHolders[0].childCount - 1; j >= 0; j--)
        //    {
        //        DestroyImmediate(noteHolders[0].GetChild(j).gameObject);
        //    }
        //}

        //for (int j = 0; j < noteCount; j++)
        //{
        //    var instance = Instantiate(notePrefab, noteHolders[0]);
        //    instance.transform.localPosition = Vector3.right * (-j * dist_per_bit + offset);
        //}
    }

    public void GenerateNotes()
    {
        //noteDatabase = GetComponent<NoteDatabase>();
        //notePattern.notes = noteDatabase.GetNotePattern();


        ////clear
        //if (noteHolders[0].childCount != 0)
        //{
        //    for (int j = noteHolders[0].childCount - 1; j >= 0; j--)
        //    {
        //        DestroyImmediate(noteHolders[0].GetChild(j).gameObject);
        //    }
        //}


        ////generate
        //for (int i = 0; i < notePattern.notes.Length; i++)
        //{
        //    ;
        //}
    }


    public void GenerateBaseLine(float interval, Transform noteHolder, GameObject basePrefab)
    {
        float offset = 0;

        if (noteHolder.childCount != 0)
        {
            for (int j = noteHolder.childCount - 1; j >= 0; j--)
            {
                DestroyImmediate(noteHolder.GetChild(j).gameObject);
            }
        }

        for (int j = 0; j < 100; j++)
        {
            var instance = Instantiate(basePrefab, noteHolder);
            instance.transform.localPosition = Vector3.right * (-j * interval + offset);
        }
    }

    public void GenerateNote(Transform noteHolder, GameObject notePrefab)
    {
        var instance = Instantiate(notePrefab, noteHolder);
        instance.transform.position = new Vector3(0, instance.transform.position.y, instance.transform.position.z);
    }

}
