using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timber : MonoBehaviour, ICarryable
{
    public GameObject GetCarriableObject => this.gameObject;
    public float GetItemSpace => 10f;
    public string GetItemName => "Timber";
    public void PickUp(PlayerCarryController player)
    {
        player.Carry(this);
    }
}
