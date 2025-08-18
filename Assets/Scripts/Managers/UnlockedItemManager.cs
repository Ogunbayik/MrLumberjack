using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedItemManager : MonoBehaviour
{
    private List<ItemDataSO> unlockedItemList = new List<ItemDataSO>();

    public static UnlockedItemManager Instance;
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
    public void AddUnlockedItem(ItemDataSO item)
    {
        unlockedItemList.Add(item);
    }
    public List<ItemDataSO> GetUnlockedItemList()
    {
        return unlockedItemList;
    }
}
