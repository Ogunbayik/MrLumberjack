using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : MonoBehaviour
{
    private BuildingManager buildingManager;

    [SerializeField] private int maxGiveTime;
    [SerializeField] private float distanceBetweenItem;

    private float giveTimer;
    private void Awake()
    {
        buildingManager = GetComponentInParent<BuildingManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCarryController>())
            giveTimer = maxGiveTime / 2;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerCarryController>(out PlayerCarryController player))
        {
            giveTimer -= Time.deltaTime;

            if(giveTimer <= 0)
            {
                GiveItem(player);

                giveTimer = maxGiveTime;
            }
        }
    }

    private void GiveItem(PlayerCarryController player)
    {
        var produceList = buildingManager.GetProduceList();

        if (produceList.Count > 0)
        {
            var lastProduceItem = produceList[produceList.Count - 1];
            lastProduceItem.transform.SetParent(player.GetCarryPosition());
            player.IsCarrying();

            var carryPosition = player.GetCarryPosition();
            var carriedList = player.GetCarriedList();
            var itemOffsetY = (float) carriedList.Count / distanceBetweenItem;

            carriedList.Add(lastProduceItem);
            lastProduceItem.transform.position = new Vector3(carryPosition.position.x, carryPosition.position.y + itemOffsetY, carryPosition.position.z);
            lastProduceItem.transform.rotation = player.GetCarryPosition().rotation;

            produceList.Remove(lastProduceItem);
            buildingManager.IsProducing();
        }
        else
        {
            Debug.Log("Building need to produce item");
        }
    }
}
