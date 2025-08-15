using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemReceiver : MonoBehaviour
{
    public event Action OnBuildingUnlocking;

    private BuildingManager buildingManager;
    private BuildingAnimationController buildingAnimator;

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
        StartCoroutine(nameof(UnlockingBuilding));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerCarryController>())
        {
            receiveTimer = maxReceiveTime;
            CameraManager.Instance.SetItemReceiver(buildingManager);

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
            CameraManager.Instance.SetItemReceiver(null);
        }
    }
    private void ReceiveItem(PlayerCarryController player)
    {
        receiveTimer -= Time.deltaTime;

        if(receiveTimer <= 0)
        {
            buildingManager.IncreaseMaterialCount();
            buildingManager.IsProducing();

            player.DestroyLastListObject();
            receiveTimer = maxReceiveTime;
        }
    }
    private IEnumerator UnlockingBuilding()
    {
        var currentMoney = MoneyManager.Instance.GetCurrentMoney();
        var buildingCost = buildingManager.GetBuildCost();

        if(currentMoney < buildingCost)
        {
            Debug.Log("Put need more money warnings for player");
            buildingManager.ToggleReceiverPanel(true);
            yield break;
        }

        OnBuildingUnlocking?.Invoke();
        Debug.Log("Total spend money is " + buildingCost);
        yield return new WaitForSeconds(2f);
        buildingAnimator.TriggerUnlockAnimation();
        yield return new WaitForSeconds(2.5f);
        Debug.Log("Building is Unlocked");
        buildingManager.SetInitialObjects(true);
        buildingManager.ToggleBuildingPanel(true);
        buildingManager.SetUnlocked(true);
    }



}
