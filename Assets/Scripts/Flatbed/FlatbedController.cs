using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatbedController : MonoBehaviour
{
    private IFlatbedState currentState;
    [Header("Visual Settings")]
    public GameObject cargoVisual;
    public GameObject[] strapsVisual;
    [Header("Movement Settings")]
    public Transform movementPosition;
    public float maximumSpeed;
    [Header("Item Settings")]
    public string requiredItemName;
    public int minRequiredItemCount;
    public int maxRequiredItemCount;
    [Header("Distance Settings")]
    public float deceleratingDistance;

    [HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public bool isLoaded;
    public int requiredItemCount;
    private void Start()
    {
        InitializeFlatbed();
    }
    private void InitializeFlatbed()
    {
        currentState = new FlatbedAcceleratingState();
        currentState.EnterState(this);

        requiredItemCount = Random.Range(minRequiredItemCount, maxRequiredItemCount);
        HasTruckLoaded();
    }

    public void SetState(IFlatbedState newState)
    {
        if(currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = newState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }
    public void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movementPosition.position, currentSpeed * Time.deltaTime);
    }
    private bool HasTruckLoaded()
    {
        if (isLoaded)
            ActivateLoadedVisual(true);
        else
            ActivateLoadedVisual(false);

        return isLoaded;
    }
    private void ActivateLoadedVisual(bool isLoaded)
    {
        cargoVisual.SetActive(isLoaded);

        foreach (var strap in strapsVisual)
        {
            strap.SetActive(isLoaded);
        }
    }

    public void DecreaseRequiredItemCount()
    {
        requiredItemCount--;
    }

}
