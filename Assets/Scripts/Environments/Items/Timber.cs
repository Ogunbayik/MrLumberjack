using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timber : MonoBehaviour, ICarryable
{
    [Header("Item Data")]
    [SerializeField] private ItemDataSO itemDataSO;
    public GameObject GetCarriableObject => this.gameObject;
    public ItemDataSO GetItemDataSO => itemDataSO;

    public void PickUp(PlayerCarryController player)
    {
        player.Carry(this);
    }
}
