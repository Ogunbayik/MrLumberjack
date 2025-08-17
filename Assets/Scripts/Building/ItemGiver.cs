using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : MonoBehaviour
{
    private BuildingManager buildingManager;

    [Header("Give Settings")]
    [SerializeField] private int maxGiveTime;

    private float giveTimer;
    private void Awake()
    {
        buildingManager = GetComponentInParent<BuildingManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCarryController>())
            giveTimer = maxGiveTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerCarryController>(out PlayerCarryController player))
        {
            giveTimer -= Time.deltaTime;

            if(giveTimer <= 0)
            {
                GiveItemToPlayer(player);
                giveTimer = maxGiveTime;
            }
        }
    }

    private void GiveItemToPlayer(PlayerCarryController player)
    {
        var produceList = buildingManager.GetProduceList();

        if (produceList.Count > 0)
        {
            var lastProduceItem = produceList[produceList.Count - 1];
            lastProduceItem.transform.SetParent(player.GetCarryPosition());
            player.UpdateCarryingStatus();

            var carryPosition = player.GetCarryPosition();
            var carriedList = player.GetCarriedList();
            var itemOffsetY = (float)carriedList.Count / buildingManager.GetProduceItemSO().itemBetweenSpace;

            carriedList.Add(lastProduceItem);
            lastProduceItem.transform.position = new Vector3(carryPosition.position.x, carryPosition.position.y + itemOffsetY, carryPosition.position.z);
            lastProduceItem.transform.rotation = player.GetCarryPosition().rotation;

            produceList.Remove(lastProduceItem);
            buildingManager.UpdateProduceStatus();
        }
        else
        {
            Debug.Log("Building need to produce item");
        }
    }
}
