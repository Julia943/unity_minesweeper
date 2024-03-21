using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    int xCoord;
    int yCoord;
    bool isBomb;
    bool isOpened = false;
    bool isFlaged = false;
    GameObject cellInstanse;


    public Cell(int xCoord, int yCoord)
    {
        this.xCoord = xCoord;
        this.yCoord = yCoord;
    }

    public bool IsBomb { get => isBomb; set => isBomb = value; }
    public int XCoord { get => xCoord;}
    public int YCoord { get => yCoord;}
    public GameObject CellInstance { get => cellInstanse; set => cellInstanse = value; }

    public OpenCellRessult OpenCell()
    {
        if (isOpened || isFlaged) return OpenCellRessult.None;
        if (isBomb) return OpenCellRessult.Gameover;

        isOpened = true;
        return OpenCellRessult.Opened;
    }
}

public enum OpenCellRessult
{
    Gameover,
    Opened,
    None
}
