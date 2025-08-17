using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private List<GameObject> produceList;

    [SerializeField] private string buildingID;
    [SerializeField] private List<GameObject> initialInactiveList;
    [SerializeField] private ItemDataSO requiredItemSO;
    [SerializeField] private ItemDataSO productItemSO;
    [SerializeField] private Transform producePosition;
    [SerializeField] private int materialNeededCount;
    [SerializeField] private int maxProduceTimer;
    [SerializeField] private int maxProduceCount;

    private float produceTimer;

    private int materialCount;

    private bool isProduce;
    void Start()
    {
        InitializeBuilding();
    }

    // Update is called once per frame
    void Update()
    {
        if (isProduce)
            ProduceItem();
    }
    private void InitializeBuilding()
    {
        produceTimer = maxProduceTimer;
        SetInitialObjects(false);
    }
    public void SetInitialObjects(bool isActive)
    {
        foreach (var obj in initialInactiveList)
        {
            obj.gameObject.SetActive(isActive);
        }
    }
    private void ProduceItem()
    {
        if (isProduce)
        {
            produceTimer -= Time.deltaTime;

            if (produceTimer <= 0)
            {
                var productItem = Instantiate(productItemSO.itemPrefab, producePosition);
                produceList.Add(productItem);
                var itemOffset = (float)produceList.Count / productItemSO.itemBetweenSpace;

                if (IsDirectionX())
                    productItem.transform.position = new Vector3(producePosition.position.x - itemOffset, producePosition.position.y + (productItem.transform.localScale.y / 2), producePosition.position.z);
                else
                    productItem.transform.position = new Vector3(producePosition.position.x, producePosition.position.y + (productItem.transform.localScale.y / 2), producePosition.position.z - itemOffset);

                DecreaseMaterialCount();
                SetProduceTimer(maxProduceTimer);
                UpdateProduceStatus();
            }
        }
        else
        {
            SetProduceTimer(maxProduceTimer);
        }
    }
    public void IncreaseMaterialCount()
    {
        materialCount++;
    }
    private void DecreaseMaterialCount()
    {
        materialCount--;
    }
    private void SetProduceTimer(float timer)
    {
        produceTimer = timer;
    }
    public bool IsDirectionX()
    {
        if (transform.rotation.y == 0 || transform.rotation.y == 180)
            return false;
        else
            return true;
    }
    public void UpdateProduceStatus()
    {
        if (materialCount >= materialNeededCount && produceList.Count < maxProduceCount)
            isProduce = true;
        else
            isProduce = false;
    }
    public bool HasRequiredMaterial(PlayerCarryController player)
    {
        return player.GetCarriedObjectName() == requiredItemSO.itemName;
    }
    public List<GameObject> GetProduceList()
    {
        return produceList;
    }
    public ItemDataSO GetRequiredItemSO()
    {
        return requiredItemSO;
    }
    public ItemDataSO GetProduceItemSO()
    {
        return productItemSO;
    }
    public string GetBuildingID()
    {
        return buildingID;
    }
}
