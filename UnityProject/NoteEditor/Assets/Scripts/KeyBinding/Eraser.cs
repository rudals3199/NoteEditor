using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : Keybinding_UI{

    public int holder;
    public NoteGenerator noteGenerator;

    public override void OnButtonDownEvent()
    {
        noteGenerator.EraseNote(holder-1);
    }
}
