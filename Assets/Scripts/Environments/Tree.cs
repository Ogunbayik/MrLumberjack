using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IChopable
{
    private Respawning respawning;

    private List<GameObject> logList;

    [Header("Health Settings")]
    [SerializeField] private int minimumHealth;
    [SerializeField] private int maximumHealth;
    [Header("General Settings")]
    [SerializeField] private GameObject logPrefab;
    [Tooltip("If you increase number, logs are closer each other")]
    [SerializeField] private float logDistanceBetween;

    private int currentHealth;
    private int randomHealth;
    private void Awake()
    {
        respawning = GetComponent<Respawning>();
        logList = new List<GameObject>();
    }
    private void Start()
    {
        randomHealth = Random.Range(minimumHealth, maximumHealth);
        currentHealth = randomHealth;
    }
    public void Chop()
    {
        var chopDamage = 1;
        currentHealth -= chopDamage;

        if (currentHealth <= 0)
        {
            SpawnLogs();
            currentHealth = randomHealth;
            respawning.DeactivateVisual();
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
