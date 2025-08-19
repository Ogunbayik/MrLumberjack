using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public event Action OnBuildingUnlockStarted;
    public event Action OnBuildingUnlocked;

    public event Action OnStartReceived;
    public event Action<PlayerCarryController> OnPlayerReceiveItem;

    public static EventManager Instance;

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
    public void StartBuildingUnlock()
    {
        OnBuildingUnlockStarted?.Invoke();
    }
    public void BuildingUnlocked()
    {
        OnBuildingUnlocked?.Invoke();
    }
    public void StartReceiveItem()
    {
        OnStartReceived?.Invoke();
    }
    public void ReceiveItem(PlayerCarryController player)
    {
        OnPlayerReceiveItem?.Invoke(player);
    }
}
