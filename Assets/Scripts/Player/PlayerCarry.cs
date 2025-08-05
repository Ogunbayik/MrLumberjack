using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    private PlayerToolController toolController;

    private List<GameObject> carryList;

    private void Awake()
    {
        toolController = GetComponent<PlayerToolController>();

        carryList = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<ICarryable>(out ICarryable carryable))
        {
            if (toolController.IsCarrying())
                carryable.Carry();
            else
                Debug.Log("Player can not carry any log");
        }
    }
}
