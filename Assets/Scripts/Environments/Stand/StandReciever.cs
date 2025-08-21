using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StandReciever : MonoBehaviour
{
    public event Action<FlatbedItemHolder> OnItemDelivered;

    private Stand stand;
    private FlatbedItemHolder flatbeditemHolder;

    [Header("Timer Settings")]
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
            flatbeditemHolder = stand.GetFlatbedItemHolder();

            if (flatbeditemHolder != null)
            {
                if (!flatbeditemHolder.IsLoaded())
                {
                    if (flatbeditemHolder.CanPickUpItem(player))
                        ReceiveItemToFlatbed(player);
                    else
                        Debug.Log("Player need to find required item");
                }
                else
                {
                    ResetReceiveTime();
                    if (!player.IsCarrying())
                        player.ResetCarriedObjectName();
                    Debug.Log("Player can't give more item");
                }
            }
            else
            {
                Debug.Log("Player need to wait any flatbed");
            }
        }
    }
    private void ReceiveItemToFlatbed(PlayerCarryController playerCarryController)
    {
        receiverTime -= Time.deltaTime;

        if(receiverTime <= 0)
        {
            FlatbedItemHolder.Instance.DeliveredItem();
            OnItemDelivered?.Invoke(flatbeditemHolder);
            flatbeditemHolder.UpdateLoadedStatus();

            playerCarryController.DestroyLastListObject();
            playerCarryController.UpdateCarryingStatus();

            ResetReceiveTime();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerCarryController>(out PlayerCarryController playerCarryController))

        if (!playerCarryController.IsCarrying())
           playerCarryController.ResetCarriedObjectName();

        ResetReceiveTime();
    }
    private void ResetReceiveTime()
    {
        receiverTime = maxReceiverTime;
    }
}
