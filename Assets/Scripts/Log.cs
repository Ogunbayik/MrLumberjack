using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour, ICarryable
{
    public GameObject GetCarriableObject => this.gameObject;

    public float GetItemSpace => 5f;

    public string GetItemName => "Log";

    public bool IsProduceItem => false;

    public void PickUp(PlayerCarryController player)
    {
        player.Carry(this);
    }

}
