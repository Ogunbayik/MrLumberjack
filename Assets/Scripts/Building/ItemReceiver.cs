using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemReceiver : MonoBehaviour
{
    private Building building;
    private BuildingUIManager buildingUIManager;
    private BuildingUnlockManager buildingUnlockManager;
    private BuildingAnimationController buildingAnimator;
    private PlayerController playerController;

    [SerializeField] private Sprite coinSprite;
    [SerializeField] private float maxReceiveTime;

    private float receiveTimer;

    private void Awake()
    {
        building = GetComponentInParent<Building>();
        buildingUnlockManager = GetComponentInParent<BuildingUnlockManager>();
        buildingUIManager = GetComponentInParent<BuildingUIManager>();
        buildingAnimator = GetComponentInParent<BuildingAnimationController>();
    }
    private void OnEnable()
    {
        buildingUnlockManager.OnBuildingUnlocked += BuildingUnlockManager_OnBuildingUnlocked;
    }
    private void OnDisable()
    {
        buildingUnlockManager.OnBuildingUnlocked -= BuildingUnlockManager_OnBuildingUnlocked;
    }
    private void BuildingUnlockManager_OnBuildingUnlocked()
    {
        StartCoroutine(nameof(UnlockBuilding));
    }
    private void OnTriggerEnter(Collider other)
    {
        playerController = other.gameObject.GetComponent<PlayerController>();

        if(playerController)
        {
            if (!buildingUnlockManager.IsUnlocked())
                buildingUnlockManager.UnlockBuilding();
            else
                ResetReceiveTimer();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerCarryController>(out PlayerCarryController player))
        {
            if (building.HasRequiredMaterial(player) && buildingUnlockManager.IsUnlocked())
                ReceiveItem(player);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCarryController>())
        {
            buildingUIManager.ToggleReceiverPanel(false);
            playerController = null;
        }
    }
    private void ReceiveItem(PlayerCarryController player)
    {
        receiveTimer -= Time.deltaTime;
        if(receiveTimer <= 0)
        {
            building.IncreaseMaterialCount();
            building.UpdateProduceStatus();

            player.DestroyLastListObject();
            player.UpdateCarryingStatus();
            ResetReceiveTimer();
        }
    }
    private IEnumerator UnlockBuilding()
    {
        var currentMoney = MoneyManager.Instance.GetCurrentMoney();
        var buildingCost = buildingUnlockManager.GetBuildCost();

        if (currentMoney < buildingCost)
        {
            buildingUIManager.ToggleReceiverPanel(true);
            yield break;
        }

        var cinematicTransitionTime = 2f;
        playerController.SetPlayerController(false);
        CameraManager.Instance.InitializeCinematicCamera(this.transform);
        CameraManager.Instance.ActivateCinematicCamera();
        yield return new WaitForSeconds(cinematicTransitionTime);

        var unlockAnimationDuration = 2.5f;
        buildingAnimator.TriggerUnlockAnimation();

        yield return new WaitForSeconds(unlockAnimationDuration);

        var playerControlReactivationDelay = 2f;
        CameraManager.Instance.ActivateGameCamera();
        building.SetInitialObjects(true);
        buildingUIManager.ToggleBuildingPanel(true);
        buildingUnlockManager.SetUnlocked(true);
        yield return new WaitForSeconds(playerControlReactivationDelay);

        playerController.SetPlayerController(true);
    }

    private void ResetReceiveTimer()
    {
        receiveTimer = maxReceiveTime;
    }


}
