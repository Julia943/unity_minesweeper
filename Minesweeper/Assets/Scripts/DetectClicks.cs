using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DetectClicks : MonoBehaviour
{
    public GameObject button;
    public Text playTxt, gameName;
    private bool clicked;
    public Image imageToFade;

    void OnMouseDown()
    {
        if (!clicked)
        {
            clicked = true;
            playTxt.gameObject.SetActive(false);
            gameName.gameObject.SetActive(false);
            button.GetComponent<ScrolObjects>().speed = -5f;
            button.GetComponent<ScrolObjects>().chekPos = -125f;
            Color tempColor = imageToFade.color;
            tempColor.a = 0.0f;
            imageToFade.color = tempColor;
            SceneManager.LoadScene(1);
        }
    }

}
