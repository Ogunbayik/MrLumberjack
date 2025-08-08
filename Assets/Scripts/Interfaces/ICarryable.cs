using UnityEngine;
public interface ICarryable
{
    public string GetItemName { get; }
    public GameObject GetCarriableObject { get; }
    public float GetItemSpace { get; }
    public bool IsProduceItem { get; }
    public void PickUp(PlayerCarryController player);

}
