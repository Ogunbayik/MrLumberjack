using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemReceiver : MonoBehaviour
{
    private BuildingManager buildingManager;
    private BuildingAnimationController buildingAnimator;
    private PlayerController playerController;

    [SerializeField] private Sprite coinSprite;
    [SerializeField] private float maxReceiveTime;

    private float receiveTimer;

    private void Awake()
    {
        buildingManager = GetComponentInParent<BuildingManager>();
        buildingAnimator = GetComponentInParent<BuildingAnimationController>();
    }
    private void OnEnable()
    {
        buildingManager.OnBuildingUnlocked += BuildingManager_OnBuildingUnlocked;
    }
    private void OnDisable()
    {
        buildingManager.OnBuildingUnlocked -= BuildingManager_OnBuildingUnlocked;
    }
    private void BuildingManager_OnBuildingUnlocked()
    {
        StartCoroutine(nameof(UnlockBuilding));
    }
    private void OnTriggerEnter(Collider other)
    {
        playerController = other.gameObject.GetComponent<PlayerController>();

        if(playerController)
        {
            receiveTimer = maxReceiveTime;

            if (!buildingManager.IsUnlocked())
                buildingManager.UnlockBuilding();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerCarryController>(out PlayerCarryController player))
        {
            if (buildingManager.HasRequiredMaterial(player) && buildingManager.IsUnlocked())
                ReceiveItem(player);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCarryController>())
        {
            buildingManager.ToggleReceiverPanel(false);
            playerController = null;
        }
    }
    private void ReceiveItem(PlayerCarryController player)
    {
        receiveTimer -= Time.deltaTime;

        if(receiveTimer <= 0)
        {
            buildingManager.IncreaseMaterialCount();
            buildingManager.UpdateProduceStatus();

            player.DestroyLastListObject();
            receiveTimer = maxReceiveTime;
        }
    }
    private IEnumerator UnlockBuilding()
    {
        var currentMoney = MoneyManager.Instance.GetCurrentMoney();
        var buildingCost = buildingManager.GetBuildCost();

        if (currentMoney < buildingCost)
        {
            Debug.Log("Put need more money warnings for player");
            buildingManager.ToggleReceiverPanel(true);
            yield break;
        }

        var cinematicTransitionTime = 2f;
        playerController.SetPlayerController(false);
        CameraManager.Instance.InitializeCinematicCamera(this.transform);
        CameraManager.Instance.ActivateCinematicCamera();
        Debug.Log("Total spend money is " + buildingCost);
        yield return new WaitForSeconds(cinematicTransitionTime);

        var unlockAnimationDuration = 2.5f;
        buildingAnimator.TriggerUnlockAnimation();

        yield return new WaitForSeconds(unlockAnimationDuration);

        var playerControlReactivationDelay = 2f;
        Debug.Log("Building is Unlocked");
        CameraManager.Instance.ActivateGameCamera();
        buildingManager.SetInitialObjects(true);
        buildingManager.ToggleBuildingPanel(true);
        buildingManager.SetUnlocked(true);

        yield return new WaitForSeconds(playerControlReactivationDelay);

        playerController.SetPlayerController(true);
    }



}
