using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    public int size = 15;
    public float space = 1.2f;
    public GameObject prefab;

    private void Start()
    {
        Build();    
    }

    private void Build()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int z = 0; z < size; z++)
                {
                    Vector3 pos = transform.position + new Vector3(x * space, y * space, z * space);
                    Instantiate(prefab, pos, Quaternion.identity, transform);
                }
            }
        }
    }

}
