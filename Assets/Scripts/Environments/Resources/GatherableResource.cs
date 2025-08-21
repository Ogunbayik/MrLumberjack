using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GatherableResource : MonoBehaviour
{
    private Collider resourceCollider;

    protected List<GameObject> resourceList = new List<GameObject>();

    [Header("Resource Settings")]
    [SerializeField] protected int minimumHealth;
    [SerializeField] protected int maximumHealth;
    [SerializeField] protected ItemDataSO resourceDataSO;
    [Header("Visual Settings")]
    [SerializeField] private GameObject resourceVisual;
    [Header("Spawn Settings")]
    [SerializeField] private float respawnDelayTimer;

    private int currentHealth;
    private int spawnCount;

    private float respawnTimer;
    private bool canRespawn = false;
    private void Awake()
    {
        resourceCollider = GetComponent<Collider>();
        respawnTimer = respawnDelayTimer;
    }
    private void Start()
    {
        currentHealth = GetRandomIndex();
        spawnCount = currentHealth;
    }
    private void Update()
    {
        if (canRespawn)
            RespawnGatherableResource();
    }
    protected virtual void OnHitResource(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            SpawnResource();
            SetObjectActivion(false);
            UpdateRespawnStatus(false);
        }
    }
    public void ResourceCollected(GameObject resource)
    {
        resourceList.Remove(resource);
    }
    public void UpdateRespawnStatus()
    {
        if (resourceList.Count == 0)
            UpdateRespawnStatus(true);
    }
    private void SpawnResource()
    {
        resourceList.Clear();
        var resourceCount = spawnCount;

        for (int i = 0; i < resourceCount; i++)
        {
            var resource = Instantiate(resourceDataSO.itemPrefab, transform);
            var resourceInterval = transform.forward * resourceList.Count * resourceDataSO.intervalHorizontal;

            resource.transform.position = transform.position + resourceInterval;
            resource.transform.rotation = Quaternion.identity;

            resourceList.Add(resource);
        }
    }
    private void RespawnGatherableResource()
    {
        respawnTimer -= Time.deltaTime;

        if (respawnTimer <= 0)
        {
            currentHealth = GetRandomIndex();
            spawnCount = currentHealth;
            SetObjectActivion(true);
            respawnTimer = respawnDelayTimer;
            UpdateRespawnStatus(false);
        }
    }
    public void UpdateRespawnStatus(bool canRespawn)
    {
        this.canRespawn = canRespawn;
    }
    public void SetObjectActivion(bool isActive)
    {
        resourceVisual.SetActive(isActive);
        resourceCollider.enabled = isActive;
    }

    private int GetRandomIndex()
    {
        var randomIndex = Random.Range(minimumHealth, maximumHealth);
        return randomIndex;
    }
}
