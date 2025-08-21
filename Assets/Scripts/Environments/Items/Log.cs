using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour, ICarryable
{
    [Header("Item Data")]
    [SerializeField] private ItemDataSO itemDataSO;
    public GameObject GetCarriableObject => this.gameObject;

    public ItemDataSO GetItemDataSO => itemDataSO;

    public void PickUp(PlayerCarryController player)
    {
        var gatherableResource = GetComponentInParent<GatherableResource>();
        gatherableResource.ResourceCollected(this.gameObject);
        gatherableResource.UpdateRespawnStatus();

        player.Carry(this);
    }

    

}
