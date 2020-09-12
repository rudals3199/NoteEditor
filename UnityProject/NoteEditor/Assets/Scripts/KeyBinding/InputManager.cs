using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    bool isWaiting = false;
    Text keyCodeText;

    //[Header("Requirements")]
    
 
    //[Header("References")]


    Dictionary<string, KeyCode> userButtons = new Dictionary<string, KeyCode>();

    private void OnEnable()
    {
        InitializeKeys();
    }
    void InitializeKeys()
    {
        userButtons["Gen1"] = KeyCode.A;
        userButtons["Gen2"] = KeyCode.S;
        userButtons["Gen3"] = KeyCode.D;
        userButtons["Gen4"] = KeyCode.F;
        //알트키
        userButtons["Alt_Gen1"] = KeyCode.H;
        userButtons["Alt_Gen2"] = KeyCode.J;
        userButtons["Alt_Gen3"] = KeyCode.K;
        userButtons["Alt_Gen4"] = KeyCode.L;

        userButtons["Erase1"] = KeyCode.Y;
        userButtons["Erase2"] = KeyCode.U;
        userButtons["Erase3"] = KeyCode.I;
        userButtons["Erase4"] = KeyCode.O;


        userButtons["Play&Pause"] = KeyCode.Space;
        userButtons["Stop"] = KeyCode.Q;
        userButtons["Rewind"] = KeyCode.R;

        userButtons["PrevBeat"] = KeyCode.Alpha1;
        userButtons["PrevNote"] = KeyCode.Alpha2;
        userButtons["NextNote"] = KeyCode.Alpha3;
        userButtons["NextBeat"] = KeyCode.Alpha4;
        


        userButtons["Delete"] = KeyCode.E;
        userButtons["Undo"] = KeyCode.Z;
    }



    public bool GetButtonDown(string button)
    {
        if(userButtons.ContainsKey(button) == false)
        {
            Debug.Log("ERROR: 해당이름의 버튼이 존재하지 않습니다.");
            return false;
        }
        return Input.GetKeyDown(userButtons[button]);
    }

    public bool GetButtonUp(string button)
    {
        if (userButtons.ContainsKey(button) == false)
        {
            Debug.Log("ERROR: 해당이름의 버튼이 존재하지 않습니다.");
            return false;
        }
        return Input.GetKeyUp(userButtons[button]);
    }


    //키셋팅
    public void TriggerKeyBind(string keyName,ref Text text)
    {
        keyCodeText = text;
        keyCodeText.text = "새입력";

        StartCoroutine(CheckKeyDown(keyName));
    }

    IEnumerator CheckKeyDown(string keyName)
    {
        isWaiting = true;
        while (isWaiting)
        {
            foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    isWaiting = false;
                    SetKeyForButton(keyName, kcode);
                }
            }
            yield return null;
        }
    }

    void SetKeyForButton(string keyName, KeyCode keyCode)
    {
        userButtons[keyName] = keyCode;
        keyCodeText.text = "[" + keyCode.ToString() + "]";
    }
}
