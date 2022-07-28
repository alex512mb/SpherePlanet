using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingResource : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRender;
    [SerializeField]
    private float distanceReachDestination = 0.1f;
    [SerializeField]
    private float moveSpeed = 10;

    private ResourcePack resources;
    private BuildingSite targetBuildSite;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetBuildSite.transform.position, moveSpeed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, targetBuildSite.transform.position);
        if (distance <= distanceReachDestination)
        {
            targetBuildSite.AddResources(resources);
            enabled = false;
            DestroyImmediate(gameObject);
        }
    }

    public void Setup(ResourcePack resources, BuildingSite target)
    {
        targetBuildSite = target;
        this.resources = resources;
        UpdateColor();
    }

    private void UpdateColor()
    {
        Color resourceColor = GameDefenitions.instance.dictResourceToColor[resources.id];
        meshRender.material.color = resourceColor;
    }
}
