using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingManager : MonoBehaviour
{
    private List<GameObject> produceList;

    [Header("Building Settings")]
    [SerializeField] private string materialNeededName;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform producePosition;
    [SerializeField] private int materialNeededCount;
    [SerializeField] private int maxProduceTimer;
    [SerializeField] private float distanceBetweenItem;
    [SerializeField] private int maxProduceCount;

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
        if(isProducing)
        {
            produceTimer -= Time.deltaTime;

            if(produceTimer <= 0)
            {
                materialCount -= materialNeededCount;
                produceTimer = maxProduceTimer;

                ProduceItem();
                IsProducing();
            }
        }
    }

    private void ProduceItem()
    {
        var item = Instantiate(itemPrefab, producePosition);
        produceList.Add(item);
        var produceCount = produceList.Count;
        var desiredPosition = (float)produceCount / distanceBetweenItem;

        item.transform.position = new Vector3(producePosition.position.x, producePosition.position.y + (item.transform.localScale.y / 2), producePosition.position.z - desiredPosition);
    }

    public void IncreaseMaterialCount()
    {
        materialCount++;
    }
    public void IsProducing()
    {
        if (materialCount >= materialNeededCount && produceList.Count < maxProduceCount)
            isProducing = true;
        else
            isProducing = false;
    }

    public string GetMaterialNeededName()
    {
        return materialNeededName;
    }

    public void RemoveProduceItem(GameObject produceItem)
    {
        produceList.Remove(produceItem);
    }
}
