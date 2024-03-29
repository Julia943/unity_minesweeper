using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrolObjects : MonoBehaviour
{
    public float speed = 5f, chekPos = 0f;
    private RectTransform rec;

    void Start()
    {
        rec = GetComponent<RectTransform> ();
    }

    void Update() 
    {
        if (rec.offsetMin.y != chekPos)
        {
            rec.offsetMin += new Vector2(rec.offsetMin.x, speed);
            rec.offsetMax += new Vector2(rec.offsetMax.x, speed);
        }
    }
}
