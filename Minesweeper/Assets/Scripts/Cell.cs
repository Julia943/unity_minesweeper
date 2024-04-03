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


    public void Initialize(int xCoord, int yCoord)
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
        isOpened = true;
        if (isBomb) return OpenCellRessult.Gameover;
        return OpenCellRessult.Opened;
    }

    public SetBombFlagResult SetBombFlag()
    {
        if (isOpened) return SetBombFlagResult.None;
        if (isFlaged)
        {
            isFlaged = false;
            return SetBombFlagResult.Unsetted;
        }
        isFlaged = true;
        return SetBombFlagResult.Setted;
    }

}

public enum OpenCellRessult
{
    Gameover,
    Opened,
    None
}

public enum SetBombFlagResult
{
    Setted,
    Unsetted,
    None
}
