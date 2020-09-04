using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectController : MonoBehaviour{

    public enum Instruction {Idle = 0, Erase = 1};
    Instruction excutedInstruction = Instruction.Idle;

    bool multiSelectEnable = true;
    bool undoEnable = false;

    Vector2 startPos;
    Vector2 endPos;
    Collider2D[] selectedNote = new Collider2D[128];
    List<Note> commandedNotes = new List<Note>();


    //selection
    public BoxCollider2D selectZone;
    public ContactFilter2D noteLayer;
    public SpriteRenderer spRenderer;
    public Camera mainCamera;
    public InputManager inputManager;

    private void Update()
    {
        CheckSelecting();

        if(inputManager.GetButtonDown("Delete"))
        {
            EraseNotes(false);
        }
        else if(inputManager.GetButtonDown("Undo") && undoEnable)    //ctrl 유니티안에선 안먹는듯
        {         
            Undo();
        }  
    }

    #region Selection
    void CheckSelecting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSelect();
        }
        else if (Input.GetMouseButton(0) && multiSelectEnable)
        {
            Selecting();
        }
        else if (Input.GetMouseButtonUp(0) && multiSelectEnable)
        {
            EndSelect();
        }
    }

    void StartSelect()
    {
        if (!selectZone.OverlapPoint(mainCamera.ScreenToWorldPoint(Input.mousePosition)))
        {
            multiSelectEnable = false;
            return;
        }
            

        NoteSetSelection(false);

        startPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        endPos = startPos;
        spRenderer.transform.position = startPos;

        if (Physics2D.OverlapArea(startPos, startPos, noteLayer.layerMask) == null)
            multiSelectEnable = true;
        else
        {
            multiSelectEnable = false;

            System.Array.Clear(selectedNote, 0, selectedNote.Length);
            Physics2D.OverlapArea(startPos, endPos, noteLayer, selectedNote);
            NoteSetSelection(true);
        }
            

    }
    void Selecting()
    {
        endPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        DrawSelectUI(startPos, endPos);
    }
    void EndSelect()
    {     
        System.Array.Clear(selectedNote, 0, selectedNote.Length);
        Physics2D.OverlapArea(startPos, endPos, noteLayer, selectedNote);

        DrawSelectUI(endPos, endPos);
        NoteSetSelection(true);
    }

    void DrawSelectUI(Vector2 start, Vector2 end)
    {
        spRenderer.transform.position = start;
        spRenderer.size = new Vector2(end.x - start.x, end.y - start.y);
    }

    void NoteSetSelection(bool flag)
    {
        for(int i = 0; i< selectedNote.Length; i++)
        {
            if (selectedNote[i] != null)
            {
                selectedNote[i].GetComponent<Note>().SetSelect(flag);
            }  
            else
            {
                break;
            }
                
        }
    }

    #endregion

    //command

    bool SetUndoEnable(Instruction instruction)
    {
        Note target;
        List<Note> excutableNotes = new List<Note>();
        for (int i = 0; i < selectedNote.Length; i++)
        {
            if (selectedNote[i] != null)
            {
                target = selectedNote[i].GetComponent<Note>();
                if(target.isEnabled)
                    excutableNotes.Add(target);
            }
            else
                break;
        }

        if (excutableNotes.Count == 0)
            return false;
        else
        {
            undoEnable = true;
            excutedInstruction = instruction;
            commandedNotes.Clear();
            commandedNotes = excutableNotes;
            return true;
        }
            
    }

    void Undo()
    {
        undoEnable = false;
        switch (excutedInstruction)
        {
            case Instruction.Erase:
                EraseNotes(true);
                break;
            default:
                Debug.Log("실행을 취소할 명령이 없습니다.");
                break;
        }

        commandedNotes.Clear();
    }

    void EraseNotes(bool undoFlag = false)
    {
        if (!undoFlag)
        {
            if(SetUndoEnable(Instruction.Erase) == false)
                return;

            foreach (Note erasedNote in commandedNotes)
            {
                erasedNote.SetEnable(false);
            }
        }
        else
        {
            foreach (Note erasedNote in commandedNotes)
            {
                erasedNote.SetEnable(true);
            }
        }

    }

    //노트 이동(인터벌, eightBeat단위)
    //재생 스냅
    //아직 끝부분 슬라이딩 하면 에러가좀 있음
}
