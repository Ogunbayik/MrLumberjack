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
            var carryList = player.GetCarriedList();

            if(carryList.Count > 0)
            {
                if (buildingManager.HasRequiredMaterial(player))
                    ReceiveItem(player);
                else
                    Debug.Log("Can't take this item");
            }
            else
            {
                player.IsCarrying();
                player.ResetCarriedObjectName();
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

            var carryList = player.GetCarriedList();
            var carryCount = carryList.Count;

            Destroy(carryList[carryCount - 1].gameObject);
            carryList.Remove(carryList[carryCount - 1]);
            receiveTimer = maxReceiveTime;
        }
    }


}
