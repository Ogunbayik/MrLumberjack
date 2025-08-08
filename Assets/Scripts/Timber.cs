using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timber : MonoBehaviour, ICarryable
{
    private BuildingManager buildManager;
    public GameObject GetCarriableObject => this.gameObject;

    public float GetItemSpace => 10f;

    public string GetItemName => "Timber";

    public bool IsProduceItem => true;
    private void Awake()
    {
        buildManager = GetComponentInParent<BuildingManager>();
    }

    public void PickUp(PlayerCarryController player)
    {
        player.Carry(this);
        buildManager.RemoveProduceItem(this.gameObject);
    }
}
