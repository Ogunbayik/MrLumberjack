using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class GatherableResource : MonoBehaviour
{
    public event Action OnResourceRespawn;

    protected Collider resourceCollider;

    protected List<GameObject> resourceList = new List<GameObject>();

    [Header("Resource Settings")]
    [SerializeField] protected GameObject resourceVisual;
    [SerializeField] protected int minimumHealth;
    [SerializeField] protected int maximumHealth;
    [SerializeField] protected GameObject resourcePrefab;
    [SerializeField] protected float distanceBetweenSpawnObjects;

    private int currentHealth;
    private int randomIndex;

    private void Start()
    {
        randomIndex = UnityEngine.Random.Range(minimumHealth, maximumHealth);
        currentHealth = randomIndex;
        resourceCollider = GetComponent<Collider>();
        Debug.Log(randomIndex);
    }
    
    public void Update()
    {
        for (int i = 0; i < resourceList.Count; i++)
        {
            if (resourceList[i].gameObject == null)
                resourceList.RemoveAt(i);
        }
    }

    protected virtual void OnHitResource(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            SpawnResource();
            currentHealth = randomIndex;
            resourceVisual.SetActive(false);
            resourceCollider.enabled = false;
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
