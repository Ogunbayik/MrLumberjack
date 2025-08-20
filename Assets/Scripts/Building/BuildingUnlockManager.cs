using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingUnlockManager : MonoBehaviour
{
    public event Action<PlayerController> OnBuildingUnlocked;

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
    private void BuildingUnlockManager_OnBuildingUnlocked(PlayerController player)
    {
        StartCoroutine(TestUnlockBuilding(player));
    }
    public void SetUnlocked(bool isUnlock)
    {
        this.isUnlocked = isUnlock;
    }
    public void UnlockBuilding(PlayerController player)
    {
        OnBuildingUnlocked?.Invoke(player);
    }
    public bool IsUnlocked()
    {
        return isUnlocked;
    }
    private IEnumerator TestUnlockBuilding(PlayerController player)
    {
        if (MoneyManager.Instance.TryToSpendMoney(buildingCost))
        {
            var cinematicTransitionTime = 2f;
            CameraManager.Instance.InitializeCinematicCamera(this.transform);
            CameraManager.Instance.ActivateCinematicCamera();
            player.SetPlayerController(false);
            yield return new WaitForSeconds(cinematicTransitionTime);

            var unlockAnimationDuration = 2.5f;
            buildingAnimationController.TriggerUnlockAnimation();

            yield return new WaitForSeconds(unlockAnimationDuration);

            var playerControlReactivationDelay = 2f;
            CameraManager.Instance.ActivateGameCamera();
            building.SetInitialObjects(true);
            buildingUIManager.ToggleMaterialImage(true);
            SetUnlocked(true);
            yield return new WaitForSeconds(playerControlReactivationDelay);

            UnlockedItemManager.Instance.AddUnlockedItem(building.GetProduceItemSO());
            player.SetPlayerController(true);
        }
    }
}
