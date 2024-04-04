using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Minefield : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
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
    private bool gameEnded = false;
    public Button startgame, menu;
    public Image victoryImage, loseImage, goose;
    public Text level1, results;
    public CanvasGroup canvasGroupLose, canvasGroupWin;

    public int Width { get => width; }
    public int Height { get => height; }

    public void SetFieldSize(int newWidth, int newHeight)
    {
        width = newWidth;
        height = newHeight;

        float totalCells = width * height;
        float defaultBombPercentage = 20f; 
        float adjustedBombPercentage = defaultBombPercentage * (totalCells / (5f * 5f)); 

        bombsToSetup = (int)(totalCells * (adjustedBombPercentage / 100f));
        remainedBombs = bombsToSetup;
        ClosedCells = (int)totalCells;
    }
    private void OpenRandomEmtyCell()
    {
        bool isOpened = false;
        List<Cell> cellsToChooseFrom = new List<Cell>(cells);

        while (!isOpened && cellsToChooseFrom.Count > 0)
        {
            int randomIndex = Random.Range(0, cellsToChooseFrom.Count);
            Cell cell = cellsToChooseFrom[randomIndex];
            if (cell.IsBomb || GetBombsAroundCell(cell) != 0)
            {
                cellsToChooseFrom.Remove(cell);
                continue;
            }
            OpenCellByCoords(new Vector3Int(cell.XCoord, cell.YCoord, 0));
            isOpened = true;
        }
    }
  
    public void StartGame()
    {
        
        CreateMinefield();
        visualizer.VisuializeCellsOnStart(cells);
        OpenRandomEmtyCell();
        
    }

    private void CreateMinefield()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject cellObject = new GameObject("Cell_" + i + "_" + j);
                Cell cellComponent = cellObject.AddComponent<Cell>();
                cellComponent.Initialize(i, j);
                cells.Add(cellComponent);
                positionToCell[new Vector3Int(i, j, 0)] = cellComponent;
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
        if (gameEnded)
        {
            return;
        }
        else
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
                int bombsAround = GetBombsAroundCell(cell);
                visualizer.OpenCell(cell, bombsAround);
                ShowGameOver();
            }

            if (ClosedCells == bombsToSetup)
            {
                ShowWin();
            }
        }

    }
    
    private void ShowWin()
    {
        results.text = "Победа!";
        victoryImage.gameObject.SetActive(true);
        canvasGroupWin.alpha = 1f;
        gameEnded = true;
        
    }

    private void ShowGameOver()
    {
        results.text = "В следующий раз повезет!";
        loseImage.gameObject.SetActive(true);
        canvasGroupLose.alpha = 1f;
        gameEnded = true;       
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

    public void SetBombFlag(Vector3Int cellCoords)
    {
        if (gameEnded)
        {
            return;
        }
        else
        {
            
            Cell cell = positionToCell[cellCoords];
            SetBombFlagResult result = cell.SetBombFlag();

            if (result == SetBombFlagResult.Setted)
            {
                settedFlags++;
                visualizer.SetBombFlag(cell, result);

                if (cell.IsBomb)
                {
                    remainedBombs--;
                    if (remainedBombs == 0 && settedFlags == bombsToSetup)
                    {
                        ShowWin();
                    }
                }
            }

            if (result == SetBombFlagResult.Unsetted)
            {
                visualizer.SetBombFlag(cell, result);
                settedFlags--;
                if (cell.IsBomb)
                {
                    remainedBombs++;
                }
            }
        }
    }
}
