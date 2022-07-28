using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameMarkers : MonoBehaviour
{
    public static GameMarkers instance;

    public GameObject buildSiteMark;

    private void Awake()
    {
        instance = this;
        buildSiteMark.SetActive(false);
    }
}
