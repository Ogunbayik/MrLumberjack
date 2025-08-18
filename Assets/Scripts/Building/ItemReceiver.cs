using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemReceiver : MonoBehaviour
{
    private PlayerController playerController;

    private Building building;
    private BuildingUIManager buildingUIManager;
    private BuildingUnlockManager buildingUnlockManager;

    [SerializeField] private float maxReceiveTime;

    private float receiveTimer;

    private void Awake()
    {
        building = GetComponentInParent<Building>();
        buildingUnlockManager = GetComponentInParent<BuildingUnlockManager>();
        buildingUIManager = GetComponentInParent<BuildingUIManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        playerController = other.gameObject.GetComponent<PlayerController>();
        var playerCarryController = other.gameObject.GetComponent<PlayerCarryController>();

        if(playerController)
        {
            if (!buildingUnlockManager.IsUnlocked() && !playerCarryController.IsCarrying())
                buildingUnlockManager.UnlockBuilding();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerCarryController>(out PlayerCarryController player))
        {
            if (buildingUnlockManager.IsUnlocked())
            {
                if (building.HasRequiredMaterial(player))
                    ReceiveItem(player);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var playerCarryController = other.gameObject.GetComponent<PlayerCarryController>();

        if (playerCarryController)
        {
            playerCarryController.ResetCarriedObjectName();
            buildingUIManager.ToggleReceiverPanel(false);
            playerController = null;
        }
    }
    private void ReceiveItem(PlayerCarryController player)
    {
        receiveTimer -= Time.deltaTime;

        if (player.GetCarriedList().Count > 0)
        {
            if (receiveTimer <= 0)
            {
                building.IncreaseMaterialCount();
                building.UpdateProduceStatus();

                player.DestroyLastListObject();
                player.UpdateCarryingStatus();
                ResetReceiveTimer();
            }
        }
        else
            ResetReceiveTimer();
    }
    private void ResetReceiveTimer()
    {
        receiveTimer = maxReceiveTime;
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }


}
