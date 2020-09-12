using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPauseButton : Keybinding_UI {

    [Space]
    public Button playButton;
    public Button pauseButton;

    public override void OnButtonDownEvent()
    {
        if (playButton.IsActive())
            playButton.onClick.Invoke();
        else
            pauseButton.onClick.Invoke();
    }
}
