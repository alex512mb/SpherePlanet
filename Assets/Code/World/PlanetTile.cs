using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshRenderer))]
public class PlanetTile : MonoBehaviour
{
    public event Action<bool> OnActveChanged;
    public PlanetTile[] linkedTiles;

    public bool IsActive => isActive;

    private MeshRenderer meshRenderer;
    private Color originColor;
    private bool isActive = true;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originColor = meshRenderer.material.color;

        Deactivate();
    }

    public void Activate()
    {
        if (isActive)
            return;

        TilesRigistry.inactiveTiles.Remove(this);
        TilesRigistry.activeTiles.Add(this);

        meshRenderer.enabled = true;

        isActive = true;

        OnActveChanged?.Invoke(isActive);
    }

    public void Deactivate()
    {
        if (!isActive)
            return;

        TilesRigistry.activeTiles.Remove(this);
        TilesRigistry.inactiveTiles.Add(this);

        meshRenderer.enabled = false;

        isActive = false;

        OnActveChanged?.Invoke(isActive);
    }

    public bool HasNotActiveNeighbor()
    {
        return linkedTiles.Any(tile => !tile.isActive);
    }

    public PlanetTile GetNotActiveNeighbor()
    {
        for (int i = 0; i < linkedTiles.Length; i++)
        {
            if (!linkedTiles[i].isActive)
            {
                return linkedTiles[i];
            }
        }

        Debug.LogError($"Not active neighbor does not found");
        return default;
    }
}
