using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    private Text txt;
    private Outline oLine;

    private void Start()
    {
        txt = GetComponent<Text>();
        oLine = GetComponent<Outline>();
    }

    private void Update()
    {
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, Mathf.PingPong(Time.time / 2.0f, 1.0f));
    }
}
