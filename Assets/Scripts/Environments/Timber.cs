using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timber : MonoBehaviour, ICarryable
{
    public string GetItemName { get; } 
    public GameObject GetCarriableObject { get; }
    public float GetItemSpace { get; }

    public void PickUp(PlayerCarryController player)
    {
        player.Carry(this);
    }
}
