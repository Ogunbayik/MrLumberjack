using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawning : MonoBehaviour
{
    private Collider prefabCollider;

    [Header("Respawn Settings")]
    [SerializeField] private GameObject prefabVisual;
    [SerializeField] private int maxRespawnTime;

    private float respawnTimer;
    private void Awake()
    {
        prefabCollider = GetComponent<Collider>();
        respawnTimer = maxRespawnTime;
    }
    void Update()
    {
        RespawnObject();
    }

    private void RespawnObject()
    {
        if (!prefabVisual.activeInHierarchy)
        {
            respawnTimer -= Time.deltaTime;

            if (respawnTimer <= 0)
            {
                ActivateVisual();
                respawnTimer = maxRespawnTime;
            }
        }
    }

    public void ActivateVisual()
    {
        prefabVisual.SetActive(true);
        prefabCollider.enabled = true;
    }

    public void DeactivateVisual()
    {
        prefabVisual.SetActive(false);
        prefabCollider.enabled = false;
    }
}
