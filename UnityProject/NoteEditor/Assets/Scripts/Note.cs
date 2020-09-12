using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Note : MonoBehaviour {

    bool selected = false;
    public bool isEnabled = true;

    int count = 0;
    public TextMesh countText;

    SpriteRenderer spRenderer;
    BoxCollider2D col2d;

    private void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        col2d = GetComponent<BoxCollider2D>();

        SetEnable(isEnabled);
    }
    
    public void SetSelect(bool flag)
    {
        StartCoroutine(SetSelecting(flag));
    }

    IEnumerator SetSelecting(bool flag)
    {
        if (isEnabled)
        {
            if (flag)
            {
                spRenderer.color = Color.grey;
            }
            else
            {
                spRenderer.color = Color.white;
            }
        }
        yield return new WaitForSeconds(0.01f);
        selected = flag;
    }

    public void SetEnable(bool flag)
    {
        if (flag)
        {
            count++;
            isEnabled = true;
            col2d.enabled = true;

            
            if (selected)   spRenderer.color = Color.grey;
            else            spRenderer.color = Color.white;

            if (count>=2)  //count=2 부터 표시
                countText.text = count.ToString();
        }
        else
        {
            spRenderer.color = Color.clear;

            count = 0;
            countText.text = "";
            isEnabled = false;
            col2d.enabled = false;
        }
    }

}
