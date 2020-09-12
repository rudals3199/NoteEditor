using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NoteSelector : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    public enum Instruction { Idle = 0, Erase = 1 };
    Instruction excutedInstruction = Instruction.Idle;

    bool undoEnable = false;

    Vector2 startPos;
    Vector2 endPos;
    Collider2D[] selectedNote = new Collider2D[128];
    List<Note> commandedNotes = new List<Note>();


    //selection
    public ContactFilter2D noteLayer;
    public SpriteRenderer spRenderer;
    public Camera mainCamera;
    public InputManager inputManager;


    void Update()
    {
        if (inputManager.GetButtonDown("Delete"))
        {
            EraseNotes(false);
        }
        else if (inputManager.GetButtonDown("Undo") && undoEnable)    //ctrl 유니티안에선 안먹는듯
        {
            Undo();
        }
    }

    #region Interface
    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = mainCamera.ScreenToWorldPoint(eventData.position);
        endPos = startPos;
        spRenderer.transform.position = startPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        endPos = mainCamera.ScreenToWorldPoint(eventData.position);
        DrawSelectUI(startPos, endPos);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        NoteSetSelection(false);
        System.Array.Clear(selectedNote, 0, selectedNote.Length);
        Physics2D.OverlapArea(startPos, endPos, noteLayer, selectedNote);

        DrawSelectUI(endPos, endPos);
        NoteSetSelection(true);      
    }
    #endregion

    #region selectFunc
    void DrawSelectUI(Vector2 start, Vector2 end)
    {
        spRenderer.transform.position = start;
        spRenderer.size = new Vector2(end.x - start.x, end.y - start.y);
    }

    void NoteSetSelection(bool flag)
    {
        for (int i = 0; i < selectedNote.Length; i++)
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

    #region Command
    bool SetUndoEnable(Instruction instruction)
    {
        Note target;
        List<Note> excutableNotes = new List<Note>();
        for (int i = 0; i < selectedNote.Length; i++)
        {
            if (selectedNote[i] != null)
            {
                target = selectedNote[i].GetComponent<Note>();
                if (target.isEnabled)
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
            if (SetUndoEnable(Instruction.Erase) == false)
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
    #endregion
}
