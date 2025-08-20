using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingUIManager buildingUIManager;

    private List<GameObject> produceList;

    [Header("Building Settings")]
    [SerializeField] private List<GameObject> initialInactiveList;
    [Header("Produce Settings")]
    [SerializeField] private ItemDataSO requiredItemSO;
    [SerializeField] private ItemDataSO productItemSO;
    [SerializeField] private float maxProduceTime;
    [SerializeField] private int materialNeededCount;
    [SerializeField] private int maxProduceCount;
    [SerializeField] private Transform producePosition;

    private float produceTimer;

    private int materialCount;

    private bool isProduce;
    private void Awake()
    {
        buildingUIManager = GetComponent<BuildingUIManager>();
    }
    void Start()
    {
        InitializeBuilding();
    }
    void Update()
    {
        ProduceItem();
    }
    private void InitializeBuilding()
    {
        SetProduceTimer(maxProduceTime);
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
        if(!IsProduce())
        {
            SetProduceTimer(maxProduceTime);
            return;
        }    

        produceTimer -= Time.deltaTime;
        buildingUIManager.SetFillAmount(maxProduceTime, produceTimer);

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
            SetProduceTimer(maxProduceTime);
            UpdateProduceStatus();
        }
        
    }
    public void IncreaseMaterialCount()
    {
        materialCount++;
    }
    private void DecreaseMaterialCount()
    {
        materialCount -= materialNeededCount;
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
        if(produceList.Count <= maxProduceCount)
        {
            isProduce = true;
        }
        else
            isProduce = false;
    }
    public bool HasRequiredMaterial(PlayerCarryController player)
    {
        return player.GetCarriedObjectName() == requiredItemSO.itemName;
    }
    public bool IsProduce()
    {
        return isProduce;
    }
    public ItemDataSO GetRequiredItemSO()
    {
        return requiredItemSO;
    }
    public ItemDataSO GetProduceItemSO()
    {
        return productItemSO;
    }
    public float GetProduceItemTimer()
    {
        return produceTimer;
    }
    public float GetMaximumProduceTime()
    {
        return maxProduceTime;
    }

}
