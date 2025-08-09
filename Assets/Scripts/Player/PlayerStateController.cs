using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStateController : MonoBehaviour
{
    public event Action<States, States> OnStateChanged;

    private PlayerController playerController;
    private PlayerCarryController playerCarryController;
    private PlayerToolController playerToolController;

    public enum States
    {
        Idle,
        Moving,
        CarryingIdle,
        CarryingMove,
        Chopping,
        Mining
    }

    [HideInInspector]
    public States currentState;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerToolController = GetComponent<PlayerToolController>();
        playerCarryController = GetComponent<PlayerCarryController>();

        currentState = States.Idle;
    }

    private void Update()
    {
        SetStates();
    }

    private void SetStates()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerToolController.IsChopping() && currentState == States.Idle)
        {
            ChangeState(States.Chopping);
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && playerToolController.IsMining() && currentState == States.Idle)
        {
            ChangeState(States.Mining);
            return;
        }

        if (currentState != States.Chopping && currentState != States.Mining)
        {
            if (playerController.IsMoving())
            {
                if (playerCarryController.IsCarrying())
                    ChangeState(States.CarryingMove);
                else
                    ChangeState(States.Moving);
            }
            else
            {
                if (playerCarryController.IsCarrying())
                    ChangeState(States.CarryingIdle);
                else
                    ChangeState(States.Idle);
            }
        }
    }

    public void ChangeState(States newState)
    {
        var previousState = currentState;

        if (previousState == newState)
            return;

        currentState = newState;
        OnStateChanged?.Invoke(previousState, newState);
    }

}
