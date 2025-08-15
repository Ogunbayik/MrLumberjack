using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class BuildingManager : MonoBehaviour
{
    public event Action OnBuildingUnlocked;

    private List<GameObject> produceList;

    [Header("Building Settings")]
    [SerializeField] private List<GameObject> initialInactiveList = new List<GameObject>();
    [SerializeField] private string materialNeededName;
    [SerializeField] private int materialNeededCount;
    [Header("Produce Settings")]
    [SerializeField] private GameObject producePrefab;
    [SerializeField] private Transform producePosition;
    [SerializeField] private int maxProduceTimer;
    [SerializeField] private int maxProduceCount;
    [SerializeField] private float distanceBetweenItem;
    [Header("UI Settings")]
    [SerializeField] private Image imageBuilding;
    [SerializeField] private Image imageItemReciever;
    [SerializeField] private Sprite spriteCoin;
    [SerializeField] private Sprite spriteMaterial;
    [SerializeField] private Vector3 imageRotation;
    [Header("Unlock Settings")]
    [SerializeField] private int buildingCost;
    [SerializeField] private bool isUnlocked;
    
    private int materialCount;

    private float produceTimer;

    private bool isProducing = false;
    
    private void Awake()
    {
        produceList = new List<GameObject>();
    }
    private void Start()
    {
        InitializeBuilding();
    }
    private void InitializeBuilding()
    {
        produceTimer = maxProduceTimer;
        SetInitialObjects(false);
        UpdateBuildingUI();
        ToggleBuildingPanel(false);
        ToggleReceiverPanel(false);
    }
    void Update()
    {
        ProduceItem();
    }
    public void SetInitialObjects(bool isActive)
    {
        foreach (var obj in initialInactiveList)
        {
            obj.gameObject.SetActive(isActive);
        }
    }
    private void UpdateBuildingUI()
    {
        imageBuilding.sprite = spriteMaterial;
        imageBuilding.transform.rotation = Quaternion.Euler(imageRotation);
        imageItemReciever.sprite = spriteCoin;
        imageItemReciever.transform.rotation = Quaternion.Euler(imageRotation);
    }
    public void ToggleBuildingPanel(bool isActive)
    {
        imageBuilding.gameObject.SetActive(isActive);
    }
    public void ToggleReceiverPanel(bool isActive)
    {
        imageItemReciever.gameObject.SetActive(isActive);
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
                var itemOffset = (float)produceList.Count / distanceBetweenItem;

                if (IsDirectionX())
                    item.transform.position = new Vector3(producePosition.position.x - itemOffset, producePosition.position.y + (item.transform.localScale.y / 2), producePosition.position.z);
                else
                    item.transform.position = new Vector3(producePosition.position.x, producePosition.position.y + (item.transform.localScale.y / 2), producePosition.position.z - itemOffset);

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
    public bool IsDirectionX()
    {
        if (transform.rotation.y == 0 || transform.rotation.y == 180)
            return false;
        else
            return true;
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
    public List<GameObject> GetProduceList()
    {
        return produceList;
    }
    public void UnlockBuilding()
    {
        OnBuildingUnlocked?.Invoke();
    }
    public void SetUnlocked(bool isUnlock)
    {
        this.isUnlocked = isUnlock;
    }
    public string GetMaterialNeededName()
    {
        return materialNeededName;
    }
    public bool IsUnlocked()
    {
        return isUnlocked;
    }
    public int GetBuildCost()
    {
        return buildingCost;
    }
}
