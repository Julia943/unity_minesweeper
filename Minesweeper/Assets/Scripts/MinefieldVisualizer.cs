using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinefieldVisualize : MonoBehaviour
{
    [SerializeField] GameObject closedCell;
    [SerializeField] Transform cellContainer;
    [SerializeField] DigitSprites[] digitSprites;

    public void VisuializeCellsOnStart(List<Cell> cells)
    {
        foreach (var cell in cells)
        {
            GameObject cellInstance = Instantiate(closedCell, new Vector3(cell.XCoord, cell.YCoord, 0), Quaternion.identity);
            cellInstance.transform.parent = cellContainer;
            cell.CellInstance = cellInstance;
        }

    }

    public void OpenCell(Cell cell, int bombsAround)
    {
        cell.CellInstance.GetComponent<SpriteRenderer>().sprite = GetBombsAroundSprite(bombsAround);
    }

    private Sprite GetBombsAroundSprite(int bombsAround)
    {
        foreach (var sprite in digitSprites) 
        {
            if (sprite.numberOfBombs == bombsAround) return sprite.digitSprite;
        }
        return null;
    }
}

[System.Serializable]
public class DigitSprites
{
    public int numberOfBombs;
    public Sprite digitSprite;
}
