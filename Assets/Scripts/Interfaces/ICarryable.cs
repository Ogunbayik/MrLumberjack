using UnityEngine;
public interface ICarryable
{
    public ItemDataSO GetItemDataSO { get; }
    public GameObject GetCarriableObject { get; }
    public void PickUp(PlayerCarryController player);

}
