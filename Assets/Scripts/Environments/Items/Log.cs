using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour, ICarryable
{
    [SerializeField] private ItemDataSO itemSO;
    public GameObject GetCarriableObject => this.gameObject;
    public float GetItemSpace => itemSO.itemBetweenSpace;
    public string GetItemName => itemSO.itemName;

    public void PickUp(PlayerCarryController player)
    {
        player.Carry(this);
    }

    

}
