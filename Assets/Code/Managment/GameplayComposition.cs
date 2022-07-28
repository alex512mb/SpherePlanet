using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameplayComposition : MonoBehaviour
{
    public PlanetTile[] activeTiles;


    private void Start()
    {
        for (int i = 0; i < activeTiles.Length; i++)
        {
            activeTiles[i].Activate();
        }
    }
}
