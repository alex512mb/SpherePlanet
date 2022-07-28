using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlanetTile))]
public class PlanetTileNav : MonoBehaviour
{
    private PlanetTile tile;

    private void Awake()
    {
        tile = GetComponent<PlanetTile>();
        SetWalkable(tile.IsActive);
    }

    private void OnEnable()
    {
        tile.OnActveChanged += Tile_OnActveChanged;
    }

    private void OnDisable()
    {
        tile.OnActveChanged -= Tile_OnActveChanged;
    }

    private void Tile_OnActveChanged(bool isActive)
    {
        SetWalkable(isActive);
    }

    private void SetWalkable(bool isWalkable)
    {
        if (AstarPath.active == null)
            return;

        Vector3 pos = transform.position;
        AstarPath.active.AddWorkItem(new AstarWorkItem(() =>
        {
            // Safe to update graphs here
            var node = AstarPath.active.GetNearest(pos).node;
            node.Walkable = isWalkable;
        }));
    }
}
