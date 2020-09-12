using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public int hitCombo;
    public int comboThreshold;

    [Space]
    public TextMesh comboText;
    public TextMesh accuracyText;
    public TextMesh delayText;

    [Header("References")]
    public ScrollController[] noteScrollers;


    private void Start()
    {
        hitCombo = 0;
        comboText.text = "";
        accuracyText.text = "";
        delayText.text = "";
    }



    #region HitFunc
    public void NoteHit()
    {
        hitCombo++;
        if (hitCombo >= comboThreshold)
            comboText.text = "<color=#FF0000>" + hitCombo.ToString() + "</color>";
        else
            comboText.text = hitCombo.ToString();

    }

    public void BadHit()
    {
        accuracyText.text = "<color=#FF0000>Bad</color>";
        NoteHit();
    }

    public void NormalHit()
    {
        accuracyText.text = "<color=#FFFFFF>Normal</color>";
        NoteHit();
    }


    public void GoodHit()
    {
        accuracyText.text = "<color=#FF8000>Good</color>";
        NoteHit();
    }

    public void PerfectHit()
    {
        accuracyText.text = "<color=#00FF00>Pefect</color>";
        NoteHit();
    }

    public void NoteMiss()
    {
        hitCombo = 0;
        comboText.text = hitCombo.ToString();
        accuracyText.text = "<color=#808080>Miss</color>";
    }
    #endregion

    public void _ShowDelay(float delayTime)
    {
        delayText.text = "Delay: " + delayTime;
    }
}
