using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StandReciever : MonoBehaviour
{
    public event Action<FlatbedController> OnItemDelivered;

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
                if (flatbed.currentCount > 0 && flatbed.CanBeTaken(player))
                {
                    ReceiveItemToFlatbed(player);
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
            OnItemDelivered?.Invoke(flatbed);
            receiverTime = maxReceiverTime;
        }
    }
}
