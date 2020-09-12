using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Keybinding_UI : MonoBehaviour, IPointerClickHandler{

    public string button;

    Text keyCodeText;

    [Header("References")]
    public InputManager inputManager;
    
    void Start()
    {
        keyCodeText = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (inputManager.GetButtonDown(button))
        {
            OnButtonDownEvent();
        }
        else if(inputManager.GetButtonUp(button))
        {
            OnButtonUpEvent();
        }
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        inputManager.TriggerKeyBind(button, ref keyCodeText);
    }

    public virtual void OnButtonDownEvent()
    {
    }

    public virtual void OnButtonUpEvent()
    {
    }
}
