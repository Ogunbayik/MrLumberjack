using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingUIManager buildingUIManager;

    private List<GameObject> produceList = new List<GameObject>();

    [Header("Visual Settings")]
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
        if(!IsProduce() || produceList.Count >= maxProduceCount)
        {
            SetProduceTimer(maxProduceTime);
            isProduce = false;
            return;
        }    

        produceTimer -= Time.deltaTime;
        buildingUIManager.SetFillAmount(maxProduceTime, produceTimer);

        if (produceTimer <= 0)
        {
            var productItem = Instantiate(productItemSO.itemPrefab, producePosition);
            var itemInterval = -producePosition.transform.forward * GetProduceItemSO().intervalHorizontal * produceList.Count;

            productItem.transform.position = producePosition.transform.position + itemInterval;
            produceList.Add(productItem);
           
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
    public void UpdateProduceStatus()
    {
        if (materialCount >= materialNeededCount)
            isProduce = true;
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
    public float GetProduceItemTimer()
    {
        return produceTimer;
    }
    public float GetMaximumProduceTime()
    {
        return maxProduceTime;
    }

}
