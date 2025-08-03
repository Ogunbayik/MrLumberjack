using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStateController : MonoBehaviour
{
    public event Action<States, States> OnStateChanged;

    public enum States
    {
        Idle,
        Moving,
        CarryingIdle,
        CarryingMove
    }
    public States currentState;
    private void Awake()
    {
        currentState = States.Idle;
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ChangeState(States.Moving);
        else if (Input.GetKeyDown(KeyCode.E))
            ChangeState(States.Idle);
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
