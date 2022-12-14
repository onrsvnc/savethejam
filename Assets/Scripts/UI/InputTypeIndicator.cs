using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InputTypeIndicator : MonoBehaviour
{
    [SerializeField] private List<Sprite> inputCellTypeImages;
    public CellType currentInputCellType;

    private void Start()
    {
        ChangeInput();
    }

    public void ChangeInput()
    {
        int randInt = Random.Range(0, 3);
        if (randInt <2)
        {
            currentInputCellType = CellType.A;
            GetComponent<Image>().sprite = inputCellTypeImages[0];
        }
        else
        {
            currentInputCellType = CellType.B; 
            GetComponent<Image>().sprite = inputCellTypeImages[1];
        }
    }
}
