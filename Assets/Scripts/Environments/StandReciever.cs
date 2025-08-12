using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StandReciever : MonoBehaviour
{
    public event Action<FlatbedController> OnAllItemDelivered;

    private Stand stand;

    private FlatbedController flatbed;

    [SerializeField] private float maxReceiverTime;

    private float receiverTime;

    private void Awake()
    {
        stand = GetComponentInParent<Stand>();
    }
    private void Start()
    {
        receiverTime = maxReceiverTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerCarryController>(out PlayerCarryController player))
        {
            flatbed = stand.GetFlatbed();
            if (flatbed != null)
            {
                if (flatbed.CanBeGiven(player) && !flatbed.HasTruckLoaded())
                {
                    ReceiveItemToFlatbed(player);
                }
                else if (flatbed.HasTruckLoaded())
                {
                    OnAllItemDelivered?.Invoke(flatbed);
                }
            }
            else
            {
                Debug.Log("Player need to wait any flatbed");
            }
        }
    }
    private void ReceiveItemToFlatbed(PlayerCarryController player)
    {
        receiverTime -= Time.deltaTime;

        if(receiverTime <= 0)
        {
            player.DestroyLastListObject();
            flatbed.DecreaseRequiredItemCount();
            stand.UpdateUI(flatbed.requiredItemCount);
            receiverTime = maxReceiverTime;
        }
    }
}
