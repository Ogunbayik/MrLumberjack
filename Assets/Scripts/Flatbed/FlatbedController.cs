using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlatbedController : MonoBehaviour
{
    private IFlatbedState currentState;

    [Header("Distance Settings")]
    public float deceleratingDistance;
    [Header("Movement Settings")]
    public float maximumSpeed;

    [HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public Transform standPosition;
    [HideInInspector]
    public Transform exitPosition;
    [HideInInspector]
    public Transform movementPosition;
    private void Awake()
    {
        standPosition = GameObject.Find(Consts.FlatbedMovementPositions.STAND_POSITION).transform;
        exitPosition = GameObject.Find(Consts.FlatbedMovementPositions.EXIT_POSITION).transform;
    }
    private void Start()
    {
        InitializeFlatbed();
    }
    private void InitializeFlatbed()
    {
        movementPosition = standPosition;

        currentState = new FlatbedAcceleratingState();
        currentState.EnterState(this);
    }
    private void Update()
    {
        currentState.UpdateState(this);
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
    public void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movementPosition.position, currentSpeed * Time.deltaTime);
    }
    public void SetMovementPosition(Transform position)
    {
        movementPosition = position;
    }

}
