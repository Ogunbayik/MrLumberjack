using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(RespawnResource))]
public abstract class GatherableResource : MonoBehaviour
{
    private RespawnResource respawnResource;

    protected List<GameObject> resourceList = new List<GameObject>();

    [Header("Resource Settings")]
    [SerializeField] protected int minimumHealth;
    [SerializeField] protected int maximumHealth;
    [SerializeField] protected GameObject resourcePrefab;
    [SerializeField] protected float distanceBetweenSpawnObjects;

    private int currentHealth;
    private int randomIndex;
    private void Awake()
    {
        respawnResource = GetComponent<RespawnResource>();
    }
    private void Start()
    {
        randomIndex = UnityEngine.Random.Range(minimumHealth, maximumHealth);
        currentHealth = randomIndex;
    }
    protected virtual void OnHitResource(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            SpawnResource();
            respawnResource.SetObjectActivion(false);
            currentHealth = randomIndex;
        }
    }

    private void SpawnResource()
    {
        var resourceCount = randomIndex;

        for (int i = 0; i < resourceCount; i++)
        {
            var resource = Instantiate(resourcePrefab);
            var offsetPosition = (float)resourceList.Count / distanceBetweenSpawnObjects;
            resourceList.Add(resource);

            resource.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + offsetPosition);
            resource.transform.rotation = Quaternion.identity;
        }
    }


}
