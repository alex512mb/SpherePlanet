using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBuildTile : MonoBehaviour
{
    [SerializeField]
    private float resourceDropZoneRadius = 1;
    [SerializeField]
    private FlyingResource flyingResourcePrefab;
    [SerializeField]
    private float buildingRange = 2;
    [SerializeField]
    private int amountInOnePack = 100;

    private BuildingSite selectedBuildSite;
    private LinkedList<ResourcePack> resourceDropSequence = new LinkedList<ResourcePack>();



    private void Update()
    {
        if (selectedBuildSite == null)
        {
            for (int i = 0; i < BuildingSiteRegistry.buildingSites.Count; i++)
            {
                var site = BuildingSiteRegistry.buildingSites[i];
                float distance = Vector3.Distance(site.transform.position, transform.position);
                if (distance < buildingRange)
                {
                    selectedBuildSite = site;
                    site.enabled = false;
                    ConstructResourcesSequence(site);
                    return;
                }
            }
        }
        else if (resourceDropSequence.Count > 0)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * resourceDropZoneRadius;
            Quaternion randomRotation = Random.rotation;
            var flyingResource = Instantiate(flyingResourcePrefab, randomPos, randomRotation);
            flyingResource.Setup(resourceDropSequence.Last.Value, selectedBuildSite);
            resourceDropSequence.RemoveLast();
        }
        else
        {
            selectedBuildSite = null;
        }
    }

    private void ConstructResourcesSequence(BuildingSite site)
    {
        resourceDropSequence.Clear();

        for (int i = 0; i < site.cost.resources.Length; i++)
        {
            var itemsPack = site.cost.resources[i];
            int amountPacks = itemsPack.count / amountInOnePack;
            int amountInLast = itemsPack.count % amountInOnePack;
            for (int packNumberi = 0; packNumberi < amountPacks; packNumberi++)
            {
                resourceDropSequence.AddLast(new ResourcePack(itemsPack.id, amountInOnePack));
            }

            if(amountInLast > 0)
                resourceDropSequence.AddLast(new ResourcePack(itemsPack.id, amountInLast));

        }
    }
}
