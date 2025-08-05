using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tree : MonoBehaviour, IChopable
{
    private List<GameObject> logList;

    [Header("General Settings")]
    [SerializeField] private int minimumHealth;
    [SerializeField] private int maximumHealth;
    [Header("Log Settings")]
    [SerializeField] private GameObject logPrefab;
    [Tooltip("If you increase number, logs are closer each other")]
    [SerializeField] private float logDistanceBetween;

    private int currentHealth;
    private int randomHealth;
    private void Awake()
    {
        logList = new List<GameObject>();
    }
    private void Start()
    {
        randomHealth = UnityEngine.Random.Range(minimumHealth, maximumHealth);
        currentHealth = randomHealth;
    }
    public void Chop()
    {
        var chopDamage = 1;
        currentHealth -= chopDamage;

        if (currentHealth <= 0)
        {
            this.gameObject.SetActive(false);

            SpawnLogs();
        }
    }

    public void SpawnLogs()
    {
        var logCount = randomHealth;
        
        for (int i = 0; i < logCount; i++)
        {
            var log = Instantiate(logPrefab);
            logList.Add(log);

            var desiredLogPosition = (float)logList.Count / logDistanceBetween;

            log.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + desiredLogPosition);
        }
    }

    
}
