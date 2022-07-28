using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetConstructionTool : MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 1;

    private HashSet<PlanetTile> neighborTiles = new HashSet<PlanetTile>();

    [ContextMenu(nameof(Construct))]
    private void Construct()
    {
        PlanetTile[] allTiles = FindObjectsOfType<PlanetTile>();
        for (int i = 0; i < allTiles.Length; i++)
        {
            allTiles[i].linkedTiles = FindNeighborTiles(allTiles, allTiles[i]);
        }
    }

    private PlanetTile[] FindNeighborTiles(PlanetTile[] allTiles, PlanetTile tile)
    {
        neighborTiles.Clear();
        for (int i2 = 0; i2 < allTiles.Length; i2++)
        {
            var secondTile = allTiles[i2];
            float distance = Vector3.Distance(tile.transform.position, secondTile.transform.position);
            if (distance < maxDistance && secondTile != tile && !neighborTiles.Contains(secondTile))
            {
                neighborTiles.Add(secondTile);
            }
        }

        return neighborTiles.ToArray();
    }
}
