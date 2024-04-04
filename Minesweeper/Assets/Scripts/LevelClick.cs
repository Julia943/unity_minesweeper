using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelClick : MonoBehaviour
{
    public Minefield minefield;
    public void OnButtonClickStartGame()
    {
        minefield.victoryImage.gameObject.SetActive(false);
        minefield.loseImage.gameObject.SetActive(false);
        minefield.goose.gameObject.SetActive(false);
        minefield.startgame.gameObject.SetActive(false);
        minefield.level1.gameObject.SetActive(false);
        minefield.SetFieldSize(5, 5);
        minefield.StartGame();
    }

    public void OnButtonClickMenu()
    {
        SceneManager.LoadScene(1);
    }

    
}
