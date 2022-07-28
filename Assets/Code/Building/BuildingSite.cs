using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BuildingSite : MonoBehaviour
{
    public event Action<BuildingSite> OnBuildingCompleted;
    public BuildCost cost;

    private GameObject buildSiteMarker => GameMarkers.instance.buildSiteMark;
    private Dictionary<ResourceID, int> dictRecievedResources = new Dictionary<ResourceID, int>();
    private PlanetTile tile;

    private void Awake()
    {
        tile = GetComponent<PlanetTile>();
    }

    private void Start()
    {
        buildSiteMarker.SetActive(true);
        buildSiteMarker.transform.position = tile.transform.position;
        buildSiteMarker.transform.rotation = tile.transform.rotation;
    }

    private void OnEnable()
    {
        BuildingSiteRegistry.buildingSites.Add(this);
    }

    private void OnDisable()
    {
        BuildingSiteRegistry.buildingSites.Remove(this);
    }

    public void AddResources(ResourcePack itemsPack)
    {
        if (dictRecievedResources.ContainsKey(itemsPack.id))
        {
            dictRecievedResources[itemsPack.id] += itemsPack.count;
        }
        else
        {
            dictRecievedResources.Add(itemsPack.id, itemsPack.count);
        }

        //check cost paid
        if (cost.resources.All(pack => IsResourceRecieved(pack)))
        {
            //build is done
            tile.Activate();
            buildSiteMarker.SetActive(false);
            OnBuildingCompleted?.Invoke(this);
            Destroy(this);
        }
    }

    private bool IsResourceRecieved(ResourcePack pack)
    {
        if (dictRecievedResources.ContainsKey(pack.id))
        {
            return dictRecievedResources[pack.id] >= pack.count;
        }
        else
        {
            return false;
        }
    }
    
}

[System.Serializable]
public struct BuildCost
{
    public ResourcePack[] resources;
}

[System.Serializable]
public struct ResourcePack
{
    public ResourceID id;
    public int count;

    public ResourcePack(ResourceID id, int count)
    {
        this.id = id;
        this.count = count;
    }
}
