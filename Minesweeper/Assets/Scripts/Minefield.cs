using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Minefield : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int heigth;
    [SerializeField] MinefieldVisualize visualizer;

    [Range(15, 50)]
    [SerializeField] int bombPercentage;
    List<Cell> cells = new List<Cell>();
    Dictionary<Vector3Int, Cell> positionToCell = new Dictionary<Vector3Int, Cell>();

    int totalCells;
    int bombsToSetup;
    int remainedBombs;

    int settedFlags = 0;
    int ClosedCells;

    private void Awake()
    {
        totalCells = width * heigth;
        bombsToSetup = totalCells * bombPercentage / 100;
        remainedBombs = bombsToSetup;
        ClosedCells = totalCells;
    }
    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        CreateMinefield();
        visualizer.VisuializeCellsOnStart(cells);
    }

    private void CreateMinefield()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < heigth; j++)
            {
                Cell cell = new Cell(i, j);
                cells.Add(cell);
                positionToCell[new Vector3Int(i, j, 0)] = cell;

            }
        }
        SetBombs();
    }
    private void SetBombs()
    {
        int setBombs = 0;
        while (setBombs < bombsToSetup)
        {
            int randomIndex = Random.Range(0, cells.Count);
            if (cells[randomIndex].IsBomb) continue;
            cells[randomIndex].IsBomb = true;
            setBombs++;
        }
    }

    public void OpenCellByCoords(Vector3Int cellCoords)
    {
        Cell cell = positionToCell[cellCoords];
        OpenCellRessult result = cell.OpenCell();
        if (result == OpenCellRessult.Opened)
        {
            int bombsAround = GetBombsAroundCell(cell);
            visualizer.OpenCell(cell, bombsAround);
            ClosedCells--;

            if (bombsAround == 0)
            {
                foreach (Cell neighbour in GetNeighbourCells(cell))
                {
                    OpenCellByCoords(new Vector3Int(neighbour.XCoord, neighbour.YCoord, 0));
                }
            }
        }
        if (result == OpenCellRessult.Gameover)
        {
            print("you loze");
        }

        if (ClosedCells == bombsToSetup)
        {
            print("you win");
        }

    }

    private IEnumerable<Cell> GetNeighbourCells(Cell cell)
    {
        List<Cell> neighbourCells = new List<Cell>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                Vector3Int neighbourCoords = new Vector3Int(cell.XCoord + i, cell.YCoord + j, 0);
                if (!positionToCell.ContainsKey(neighbourCoords)) continue;
                if (i == 0 && j == 0) continue;
                Cell neighbourCell = positionToCell[neighbourCoords];
                neighbourCells.Add(neighbourCell);
            }
        }
        return neighbourCells;
    }

    private int GetBombsAroundCell(Cell cell)
    {
        int bombsAround = 0;
        foreach (var neighbour in GetNeighbourCells(cell)) 
        {
            if (neighbour.IsBomb)
            {
                bombsAround++;
            }
        }
        return bombsAround;
    }

    internal void SetBombFlag(Vector3Int cellCoords)
    {
        throw new System.NotImplementedException();
    }
}
