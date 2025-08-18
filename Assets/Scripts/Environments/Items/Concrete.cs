using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concrete : MonoBehaviour, ICarryable
{
    [SerializeField] private ItemDataSO itemSO;
    public string GetItemName => itemSO.itemName;
    public GameObject GetCarriableObject => this.gameObject;
    public float GetItemSpace => itemSO.itemBetweenSpace;

    public void PickUp(PlayerCarryController player)
    {
        player.Carry(this);
    }

}
