using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandReciever : MonoBehaviour
{
    private Stand stand;

    private FlatbedController flatbed;

    private string carriedObjectName;

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerCarryController>(out PlayerCarryController player))
            carriedObjectName = player.GetCarriedObjectName();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerCarryController>(out PlayerCarryController player))
        {
            flatbed = stand.GetFlatbed();
            if (flatbed != null)
            {
                if (flatbed.requiredItemName == player.GetCarriedObjectName())
                {
                    ReceiveItemToFlatbed(player);
                }
                else
                {
                    Debug.Log("Player need to find required item for flatbed");
                }
            }
        }
    }
    private void ReceiveItemToFlatbed(PlayerCarryController player)
    {
        receiverTime -= Time.deltaTime;

        if(receiverTime <= 0)
        {
            receiverTime = maxReceiverTime;

            if (flatbed.requiredItemCount > 0)
            {
                flatbed.DecreaseRequiredItemCount();
                player.DestroyLastListObject();
            }
            else
                Debug.Log("Flatbed receive all item");
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCarryController>())
            carriedObjectName = null;
    }

    public string GetPlayerCarriedName()
    {
        return carriedObjectName;
    }
}
