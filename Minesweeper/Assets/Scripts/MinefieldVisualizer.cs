using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinefieldVisualize : MonoBehaviour
{
    [SerializeField] GameObject closedCell;
    [SerializeField] Transform cellContainer;

    public void VisuializeCellsOnStart(List<Cell> cells)
    {
        foreach (var cell in cells)
        {
            GameObject cellInstance = Instantiate(closedCell, new Vector3(cell.XCoord, cell.YCoord, 0), Quaternion.identity);
            cellInstance.transform.parent = cellContainer;
            cell.CellInstance = cellInstance;
        }

    }
}
