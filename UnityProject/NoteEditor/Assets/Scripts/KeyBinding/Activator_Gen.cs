using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator_Gen : Keybinding_UI {

    public int holder;
    public NoteGenerator noteGenerator;

    public override void OnButtonDownEvent()
    {
        noteGenerator.GenerateNote(holder-1);
    }
}
