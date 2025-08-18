using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnResource : MonoBehaviour
{
    private Collider resourceCollider;

    [Header("Visual Settings")]
    [SerializeField] private GameObject resourceVisual;
    [Header("Spawn Settings")]
    [SerializeField] private float maxRespawnTime;

    private float respawnTimer;
    private bool canRespawn = false;
    private void Awake()
    {
        resourceCollider = GetComponent<Collider>();
        respawnTimer = maxRespawnTime;
    }
    void Update()
    {
        UpdateRespawnStatus();

        if (canRespawn)
            SpawnResource();
    }

    private void UpdateRespawnStatus()
    {
        var isVisualActive = resourceVisual.activeInHierarchy;

        if (!isVisualActive)
            canRespawn = true;
        else
            canRespawn = false;
    }

    private void SpawnResource()
    {
        respawnTimer -= Time.deltaTime;

        if(respawnTimer <= 0)
        {
            SetObjectActivion(true);
            respawnTimer = maxRespawnTime;
        }
    }
    public void SetObjectActivion(bool isActive)
    {
        resourceVisual.SetActive(isActive);
        resourceCollider.enabled = isActive;
    }
}
