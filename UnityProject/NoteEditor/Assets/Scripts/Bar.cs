using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class Bar : MonoBehaviour {

    public float maxY = 800;
    public int meter = 1;
    int maxMeter = 16;

    GridLayoutGroup gridLayoutGroup;

    public void DivideMeter()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        if (meter == maxMeter)
        {
            InitializeBar();
        }
        else
        {
            meter *= 2;
            gridLayoutGroup.cellSize = new Vector2(gridLayoutGroup.cellSize.x, maxY / meter);

            //meter가 증가할때
            int gap = maxMeter * 4 / meter;
            for (int i = 0; i < meter; i++)
            {   //짝수만 이거해도됨
                for (int j = 0; j < 4; j++)
                {
                    transform.GetChild(i * gap + j).gameObject.SetActive(true);
                }
            }
        }


    }

    void InitializeBar()
    {
        meter = 1;
        gridLayoutGroup.cellSize = new Vector2(gridLayoutGroup.cellSize.x, maxY / meter);

        for (int i = 0; i < meter * 4; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        for (int i = meter * 4; i < maxMeter * 4; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
