using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlatbedItemHolder : MonoBehaviour
{
    public static FlatbedItemHolder Instance;

    public event Action<FlatbedItemHolder> OnDeliveredItem;
    public event Action OnFlatbedLoaded;

    private Dictionary<string, int> requiredItemList = new Dictionary<string, int>();

    [Header("Visual Settings")]
    [SerializeField] private GameObject cargoVisual;
    [SerializeField] private GameObject[] strapsVisual;
    [Header("Item Settings")]
    [SerializeField] private List<ItemDataSO> allItemList;
    [SerializeField] private int minimumRequiredCount;
    [SerializeField] private int maximumRequiredCount;

    private ItemDataSO requiredItemSO;

    private int requiredItemCount;
    private int initialRequiredItemCount;

    private bool isLoaded;
    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    private void Start()
    {
        InitializeItem();
    }
    private void OnEnable()
    {
        OnDeliveredItem += FlatbedItemHolder_OnDeliveredItem;
    }
    private void OnDisable()
    {
        OnDeliveredItem -= FlatbedItemHolder_OnDeliveredItem;
    }

    private void FlatbedItemHolder_OnDeliveredItem(FlatbedItemHolder flatbed)
    {
        UpdateLoadedStatus();
    }

    public void InitializeItem()
    {
        var unlockedItemList = UnlockedItemManager.Instance.GetUnlockedItemList();
        if(unlockedItemList.Count != 0)
        {
            foreach (var item in unlockedItemList)
            {
                if (!allItemList.Contains(item))
                    allItemList.Add(item);
                else
                    Debug.Log("Flatbed has this item");
            }
        }

        requiredItemSO = GetRandomItemSO();
        requiredItemCount = GetRandomCount();
        initialRequiredItemCount = requiredItemCount;

        if (!requiredItemList.ContainsKey(requiredItemSO.itemName))
            requiredItemList.Add(requiredItemSO.itemName, requiredItemCount);

        UpdateLoadedStatus();
    }
    public void UpdateLoadedStatus()
    {
        if(initialRequiredItemCount == requiredItemCount)
        {
            Debug.Log("Flatbed is unloaded");
            ToggleLoadObjectActivation(false);
            isLoaded = false;
        }
        else if(requiredItemCount == 0)
        {
            Debug.Log("Flatbed is loaded full");
            isLoaded = true;
            OnFlatbedLoaded?.Invoke();
        }
        else
        {
            Debug.Log("Flatbed is loaded but still need more item for loaded");
            isLoaded = false;
        }
    }
    public void DeliveredItem()
    {
        OnDeliveredItem?.Invoke(this);
    }
    public void ToggleLoadObjectActivation(bool isActive)
    {
        cargoVisual.SetActive(isActive);

        foreach (var strap in strapsVisual)
        {
            strap.SetActive(isActive);
        }
    }
    public bool CanPickUpItem(PlayerCarryController player)
    {
        if (player.IsCarrying())
        {
            if (requiredItemList.ContainsKey(player.GetCarriedObjectName()))
                return true;
            else
                return false;
        }
        return false;
    }
    public void DecreaseRequiredItemCount()
    {
        if(requiredItemCount > 0)
        {
            requiredItemCount--;
            requiredItemList[requiredItemSO.itemName] = requiredItemCount;
        }
        else
        {
            requiredItemCount = 0;
            requiredItemList.Clear();
        }
    }
    private int GetRandomCount()
    {
        var randomIndex = UnityEngine.Random.Range(minimumRequiredCount, maximumRequiredCount);
        return randomIndex;
    }
    private ItemDataSO GetRandomItemSO()
    {
        var randomIndex = UnityEngine.Random.Range(0, allItemList.Count);
        var randomItemSO = allItemList[randomIndex];

        return randomItemSO;
    }
    public ItemDataSO GetRequiredItemSO()
    {
        return requiredItemSO;
    }
    public int GetRequiredItemCount()
    {
        return requiredItemCount;
    }
    public int GetInitialRequiredItemCount()
    {
        return initialRequiredItemCount;
    }
    public Dictionary<string,int> GetRequiredItemList()
    {
        return requiredItemList;
    }
    public bool IsLoaded()
    {
        return isLoaded;
    }
}
