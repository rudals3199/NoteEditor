using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindButton : Keybinding_UI {

    [Space]
    public Button bindButton;


    public override void OnButtonDownEvent()
    {
        bindButton.onClick.Invoke();
    }
}
