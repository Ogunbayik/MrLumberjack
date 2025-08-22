using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryController : MonoBehaviour
{
    private PlayerToolController toolController;

    private List<GameObject> carriedList;

    [Header("Carry Settings")]
    [SerializeField] private Transform carryPosition;
    [SerializeField] private int maximumCarryCount;

    private bool isCarrying;
    private string carriedObjectName = null;
    private void Awake()
    {
        toolController = GetComponent<PlayerToolController>();

        carriedList = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<ICarryable>(out ICarryable carryable))
        {
            if (toolController.IsCarrying())
            {
                carryable.PickUp(this);
                UpdateCarryingStatus();
            }
            else
                Debug.Log("Player can not carry any log");
        }
    }
    public void Carry(ICarryable carryable)
    {
        var carryableObject = carryable.GetCarriableObject;

        if (carriedObjectName == null)
            carriedObjectName = carryable.GetItemDataSO.itemName;

        if(carriedObjectName == carryable.GetItemDataSO.itemName && carriedList.Count < maximumCarryCount)
        {
            var itemInterval = carryPosition.transform.up * carriedList.Count * carryable.GetItemDataSO.intervalVertical;
            carryableObject.transform.SetParent(carryPosition);
            carryableObject.transform.position = carryPosition.transform.position + itemInterval;
            carryableObject.transform.rotation = carryPosition.rotation;
            
            carriedList.Add(carryableObject);
        }
        else
        {
            Debug.Log("You need to find same type item");
        }
    }
    public void DestroyLastListObject()
    {
        if (carriedList.Count > 0)
        {
            Destroy(carriedList[carriedList.Count - 1].gameObject);
            carriedList.Remove(carriedList[carriedList.Count - 1]);
        }
    }
    public void ResetCarriedObjectName()
    {
        if (carriedList.Count == 0)
            carriedObjectName = null;
    }
    public void SetCarriedObjectName(string itemName)
    {
        carriedObjectName = itemName;
    }
    public string GetCarriedObjectName()
    {
        return carriedObjectName;
    }
    public List<GameObject> GetCarriedList()
    {
        return carriedList;
    }
    public void UpdateCarryingStatus()
    {
        if (carriedList.Count > 0)
            isCarrying = true;
        else
            isCarrying = false;

    }
    public bool IsCarrying()
    {
        return isCarrying;
    }
    public Transform GetCarryPosition()
    {
        return carryPosition;
    }
}
