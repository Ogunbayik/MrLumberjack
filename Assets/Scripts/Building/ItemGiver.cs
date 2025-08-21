using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : MonoBehaviour
{
    private Building building;

    [Header("Timer Settings")]
    [SerializeField] private int maxGiveTime;

    private float giveTimer;
    private void Awake()
    {
        building = GetComponentInParent<Building>();
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
        var produceList = building.GetProduceList();

        if (produceList.Count > 0)
        {
            if (player.GetCarriedObjectName() == building.GetProduceItemSO().itemName || player.GetCarriedObjectName() == null)
            {
                var carriedList = player.GetCarriedList();
                var lastProduceItem = produceList[produceList.Count - 1];
                var itemInterval = player.GetCarryPosition().transform.up * player.GetCarriedList().Count * building.GetProduceItemSO().intervalVertical; 

                carriedList.Add(lastProduceItem);

                lastProduceItem.transform.position = player.GetCarryPosition().position + itemInterval;
                lastProduceItem.transform.rotation = player.GetCarryPosition().rotation;
                lastProduceItem.transform.SetParent(player.GetCarryPosition());
                
                player.SetCarriedObjectName(building.GetProduceItemSO().itemName);

                produceList.Remove(lastProduceItem);
                player.UpdateCarryingStatus();
                building.UpdateProduceStatus();
            }
        }
        else
        {
            Debug.Log("Building need to produce item");
        }
    }
}
