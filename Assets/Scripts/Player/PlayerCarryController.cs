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

                carryable.Carry(this,carryPosition);
            }
            else
                Debug.Log("Player can not carry any log");
        }
    }
    public void ResetCarryingState()
    {
        isCarrying = false;
    }
    public int GetMaximumCarryCount()
    {
        return maximumCarryCount;
    }

    public List<GameObject> GetCarryList()
    {
        return carriedList;
    }

    public bool IsCarrying()
    {
        return isCarrying;
    }
}
