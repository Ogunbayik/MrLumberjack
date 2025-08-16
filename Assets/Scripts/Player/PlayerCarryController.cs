using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
                IsCarrying();
                carryable.PickUp(this);
            }
            else
                Debug.Log("Player can not carry any log");
        }
    }
    public void Carry(ICarryable carryable)
    {
        var carryableObject = carryable.GetCarriableObject;
        var itemSpace = carryable.GetItemSpace;

        if (carriedObjectName == null)
            carriedObjectName = carryable.GetItemName;

        if(carriedObjectName == carryable.GetItemName && carriedList.Count < maximumCarryCount)
        {
            var desiredPosition = (float)carriedList.Count / itemSpace;
            carriedList.Add(carryableObject);
            carryableObject.transform.SetParent(carryPosition);

            carryableObject.transform.position = new Vector3(carryPosition.position.x, carryPosition.position.y + desiredPosition, carryPosition.position.z);
            carryableObject.transform.rotation = carryPosition.rotation;
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
        else
        {
            IsCarrying();
            ResetCarriedObjectName();
        }
    }
    public void ResetCarriedObjectName()
    {
        carriedObjectName = null;
    }
    public string GetCarriedObjectName()
    {
        return carriedObjectName;
    }
    public List<GameObject> GetCarriedList()
    {
        return carriedList;
    }
    public bool IsCarrying()
    {
        if (carriedList.Count > 0)
            isCarrying = true;
        else
            isCarrying = false;

        return isCarrying;
    }
    public Transform GetCarryPosition()
    {
        return carryPosition;
    }
}
