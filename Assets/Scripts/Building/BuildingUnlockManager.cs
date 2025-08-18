using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingUnlockManager : MonoBehaviour
{
    public event Action OnBuildingUnlocked;

    private Building building;
    private BuildingUIManager buildingUIManager;
    private BuildingAnimationController buildingAnimationController;
    private ItemReceiver itemReceiver;

    [Header("Unlock Settings")]
    [SerializeField] private int buildingCost;
    [SerializeField] private bool isUnlocked;
    private void Awake()
    {
        itemReceiver = GetComponentInChildren<ItemReceiver>();
        building = GetComponent<Building>();
        buildingUIManager = GetComponent<BuildingUIManager>();
        buildingAnimationController = GetComponent<BuildingAnimationController>();
    }
    private void OnEnable()
    {
        OnBuildingUnlocked += BuildingUnlockManager_OnBuildingUnlocked;
    }
    private void OnDisable()
    {
        OnBuildingUnlocked -= BuildingUnlockManager_OnBuildingUnlocked;
    }
    private void BuildingUnlockManager_OnBuildingUnlocked()
    {
        StartCoroutine(nameof(TestUnlockBuilding));
    }
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

    private IEnumerator TestUnlockBuilding()
    {
        if (MoneyManager.Instance.TryToSpendMoney(buildingCost))
        {
            var cinematicTransitionTime = 2f;
            itemReceiver.GetPlayerController().SetPlayerController(false);
            CameraManager.Instance.InitializeCinematicCamera(this.transform);
            CameraManager.Instance.ActivateCinematicCamera();
            yield return new WaitForSeconds(cinematicTransitionTime);

            var unlockAnimationDuration = 2.5f;
            buildingAnimationController.TriggerUnlockAnimation();

            yield return new WaitForSeconds(unlockAnimationDuration);

            var playerControlReactivationDelay = 2f;
            CameraManager.Instance.ActivateGameCamera();
            building.SetInitialObjects(true);
            buildingUIManager.ToggleBuildingPanel(true);
            SetUnlocked(true);
            yield return new WaitForSeconds(playerControlReactivationDelay);

            UnlockedItemManager.Instance.AddUnlockedItem(building.GetProduceItemSO());
            itemReceiver.GetPlayerController().SetPlayerController(true);
        }
    }
}
