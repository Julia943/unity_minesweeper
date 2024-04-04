using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelField : MonoBehaviour
{
    public Text worms;
    public Image field, goose;
    public Button level1, level2, level3, startgame;
    
    public void OnButtonClickLevel1()
    {
        SceneManager.LoadScene(2);
        
    }

    public void OnButtonClickLevel2()
    {
        SceneManager.LoadScene(3);
        
    }

    public void OnButtonClickLevel3()
    {
        SceneManager.LoadScene(4);
    }
}
