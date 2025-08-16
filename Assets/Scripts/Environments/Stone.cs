using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, ICarryable
{
    [SerializeField] private ItemDataSO itemDataSO;
    public string GetItemName => itemDataSO.itemName;

    public GameObject GetCarriableObject => this.gameObject;

    public float GetItemSpace => itemDataSO.itemBetweenSpace;

    public void PickUp(PlayerCarryController player)
    {
        player.Carry(this);
    }
}
