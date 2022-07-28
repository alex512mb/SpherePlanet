using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Automaticaly create new build site when current is done
/// </summary>
public class BuildPlanetSequence : MonoBehaviour
{
    public BuildCost buildCost;

    private void Start()
    {
        TryCreateNewBuildSite();
    }

    private void TryCreateNewBuildSite()
    {
        PlanetTile tile = FindNewTileToBuild();
        BuildingSite buildSite = CreateBuildSite(tile);
        buildSite.OnBuildingCompleted += BuildSite_OnBuildingCompleted;
    }

    private static PlanetTile FindNewTileToBuild()
    {
        PlanetTile tileToBuild = null;
        var activeTiles = TilesRigistry.activeTiles;
        foreach (var activeTile in activeTiles)
        {
            if (activeTile.HasNotActiveNeighbor())
            {
                tileToBuild = activeTile.GetNotActiveNeighbor();
            }
        }

        return tileToBuild;
    }

    private BuildingSite CreateBuildSite(PlanetTile tile)
    {
        var buildSite = tile.gameObject.AddComponent<BuildingSite>();
        buildSite.cost = buildCost;
        return buildSite;
    }

    private void BuildSite_OnBuildingCompleted(BuildingSite site)
    {
        site.OnBuildingCompleted -= BuildSite_OnBuildingCompleted;
        TryCreateNewBuildSite();
    }
}
