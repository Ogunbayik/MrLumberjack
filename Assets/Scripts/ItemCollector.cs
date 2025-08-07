using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private BuildingManager buildingManager;

    [SerializeField] private float maxCollectTimer;

    private float collectTimer;

    private void Awake()
    {
        buildingManager = GetComponentInParent<BuildingManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCarryController>())
            collectTimer = (float) maxCollectTimer / 2;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerCarryController>(out PlayerCarryController player))
        {
            var carryList = player.GetCarriedList();

            if(carryList.Count > 0)
            {
                CollectItem(player);
            }
            else
            {
                player.ResetCarryingState();
                player.ResetCarriedObjectName();
            }
        }
    }
    private void CollectItem(PlayerCarryController player)
    {
        collectTimer -= Time.deltaTime;

        if(collectTimer <= 0)
        {
            buildingManager.IncreaseMaterialCount();
            buildingManager.IsProducing();

            var carryList = player.GetCarriedList();
            var carryCount = carryList.Count;

            Destroy(carryList[carryCount - 1].gameObject);
            carryList.Remove(carryList[carryCount - 1]);
            collectTimer = maxCollectTimer;
        }
    }


}
