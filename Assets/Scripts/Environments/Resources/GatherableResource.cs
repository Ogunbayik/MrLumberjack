using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class GatherableResource : MonoBehaviour
{
    public event Action OnHit;
    public event Action OnDead;
    public event Action OnRespawn;

    protected ResourceAnimationController animationController;
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
        animationController = GetComponent<ResourceAnimationController>();
        resourceCollider = GetComponent<Collider>();
        respawnTimer = respawnDelayTimer;
    }
    private void Start()
    {
        currentHealth = GetRandomIndex();
        spawnCount = currentHealth;
    }
    private void OnEnable()
    {
        animationController.OnDeadAnimationComplete += AnimationController_OnDeadAnimationComplete;
        animationController.OnRespawnAnimationComplete += AnimationController_OnRespawnAnimationComplete;
    }
    private void OnDisable()
    {
        animationController.OnDeadAnimationComplete -= AnimationController_OnDeadAnimationComplete;
        animationController.OnRespawnAnimationComplete -= AnimationController_OnRespawnAnimationComplete;
    }
    private void AnimationController_OnRespawnAnimationComplete()
    {
        UpdateRespawnStatus(false);
        currentHealth = GetRandomIndex();
        spawnCount = currentHealth;
    }

    private void AnimationController_OnDeadAnimationComplete()
    {
        UpdateRespawnStatus(false);
        SpawnResource();
        SetResourceVisual(false);
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
            OnDead?.Invoke();
        }
        else
        {
            OnHit?.Invoke();
        }
    }
    public void ResourceCollected(GameObject resource)
    {
        resourceList.Remove(resource);
    }
    public void CheckRespawnStatus()
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
            respawnTimer = respawnDelayTimer;
            SetResourceVisual(true);
            OnRespawn?.Invoke();
        }
    }
    public void UpdateRespawnStatus(bool canRespawn)
    {
        this.canRespawn = canRespawn;
    }
    public void SetResourceVisual(bool isActive)
    {
        resourceVisual.SetActive(isActive);
        resourceCollider.enabled = isActive;
    }
    private int GetRandomIndex()
    {
        var randomIndex = UnityEngine.Random.Range(minimumHealth, maximumHealth);
        return randomIndex;
    }
}
