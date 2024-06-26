using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    [SerializeField] Minefield minefield;

    private void Awake()
    {
        GetComponent<Camera>().orthographicSize = 5f;
        transform.position = new Vector3(2f, 2f, -10);
    }
}
