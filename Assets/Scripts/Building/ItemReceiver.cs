using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReceiver : MonoBehaviour
{
    private BuildingManager buildingManager;

    [SerializeField] private float maxReceiveTime;

    private float receiveTimer;

    private void Awake()
    {
        buildingManager = GetComponentInParent<BuildingManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCarryController>())
            receiveTimer = (float) maxReceiveTime / 2;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerCarryController>(out PlayerCarryController player))
        {
            if(buildingManager.IsUnlocked())
            {
                UnlockBuilding();
            }
            else
            {
                if (buildingManager.HasRequiredMaterial(player))
                    ReceiveItem(player);
            }
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
    private void UnlockBuilding()
    {
        var currentMoney = MoneyManager.Instance.GetCurrentMoney();
        if (currentMoney > buildingManager.GetBuildCost())
        {
            buildingManager.UnlockBuilding();
        }
        else
        {
            Debug.Log("Player need to earn enough money");
        }
    }

    private IEnumerator UnlockingBuilding()
    {
        var currentMoney = MoneyManager.Instance.GetCurrentMoney();

        yield return new WaitForSeconds(2f);

    }



}
