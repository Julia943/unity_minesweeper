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
}
