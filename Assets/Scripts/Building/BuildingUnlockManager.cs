using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingUnlockManager : MonoBehaviour
{
    public event Action OnBuildingUnlocked;

    [Header("Unlock Settings")]
    [SerializeField] private int buildingCost;
    [SerializeField] private bool isUnlocked;
    public void SetUnlocked(bool isUnlock)
    {
        this.isUnlocked = isUnlock;
    }
    public void UnlockBuilding()
    {
        OnBuildingUnlocked?.Invoke();
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
