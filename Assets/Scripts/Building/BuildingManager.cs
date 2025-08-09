using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingManager : MonoBehaviour
{
    private List<GameObject> produceList;

    [Header("Building Settings")]
    [SerializeField] private string materialNeededName;
    [SerializeField] private int materialNeededCount;
    [Header("Produce Settings")]
    [SerializeField] private GameObject producePrefab;
    [SerializeField] private Transform producePosition;
    [SerializeField] private int maxProduceTimer;
    [SerializeField] private int maxProduceCount;
    [SerializeField] private float distanceBetweenItem;

    private int materialCount;

    private float produceTimer;

    private bool isProducing = false;
    private void Awake()
    {
        produceList = new List<GameObject>();
        produceTimer = maxProduceTimer;
    }
    void Update()
    {
        ProduceItem();
    }

    private void ProduceItem()
    {
        if (isProducing)
        {
            produceTimer -= Time.deltaTime;

            if(produceTimer <= 0)
            {
                var item = Instantiate(producePrefab, producePosition);
                produceList.Add(item);
                var itemOffsetY = (float)produceList.Count / distanceBetweenItem;

                item.transform.position = new Vector3(producePosition.position.x, producePosition.position.y + (item.transform.localScale.y / 2), producePosition.position.z - itemOffsetY);

                materialCount -= materialNeededCount;
                produceTimer = maxProduceTimer;
                IsProducing();
            }
        }
        else
        {
            produceTimer = maxProduceTimer;
        }
    }
    public void IncreaseMaterialCount()
    {
        materialCount++;
    }
    public bool IsProducing()
    {
        if (materialCount >= materialNeededCount && produceList.Count < maxProduceCount)
            isProducing = true;
        else
            isProducing = false;

        return isProducing;
    }
    public bool HasRequiredMaterial(PlayerCarryController player)
    {
        if (player.GetCarriedObjectName() == materialNeededName)
            return true;
        else
            return false;
    }
    public string GetMaterialNeededName()
    {
        return materialNeededName;
    }

    public List<GameObject> GetProduceList()
    {
        return produceList;
    }
}
