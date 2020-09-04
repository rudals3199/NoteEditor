using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Keybinding_UI : MonoBehaviour, IPointerClickHandler{

    public string button;

    Image imgRenderer;
    Text keyCodeText;

    [Header("References")]
    public InputManager inputManager;
    
    void Start()
    {
        imgRenderer = GetComponent<Image>();
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
        //imgRenderer.color = new Color(0.5f, 0.5f, 0.5f, imgRenderer.color.a);
    }

    public virtual void OnButtonUpEvent()
    {
        //imgRenderer.color = new Color(0.18f, 0.18f, 0.18f, imgRenderer.color.a);
    }
}
