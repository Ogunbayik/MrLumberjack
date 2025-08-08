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
                isCarrying = true;
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
        var isProduceItem = carryable.IsProduceItem;

        if (carriedObjectName == null)
            carriedObjectName = carryable.GetItemName;

        if (isProduceItem)
            Debug.Log("Player is carrying produce item");
        else
            Debug.Log("Player is carrying normal item");


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
    public void ResetCarriedObjectName()
    {
        carriedObjectName = null;
    }

    public void ResetCarryingState()
    {
        isCarrying = false;
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
        return isCarrying;
    }
}
