using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameDefenitions : MonoBehaviour
{
    public static GameDefenitions instance;

    [SerializeField]
    private Color[] resourceColors;

    public Dictionary<ResourceID, Color> dictResourceToColor = new Dictionary<ResourceID, Color>();

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < resourceColors.Length; i++)
        {
            dictResourceToColor.Add((ResourceID)i, resourceColors[i]);
        }
        
    }
}
