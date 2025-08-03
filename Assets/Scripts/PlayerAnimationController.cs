using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerStateController stateController;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();

        stateController = GetComponentInParent<PlayerStateController>();
    }
    private void OnEnable()
    {
        stateController.OnStateChanged += StateController_OnStateChanged;
    }
    private void OnDisable()
    {
        stateController.OnStateChanged -= StateController_OnStateChanged;
    }

    private void StateController_OnStateChanged(PlayerStateController.States oldState, PlayerStateController.States newState)
    {
        switch(newState)
        {
            case PlayerStateController.States.Idle: 
                MovementAnimation(false);
                CarryAnimation(false);
                break;
            case PlayerStateController.States.Moving: 
                MovementAnimation(true);
                CarryAnimation(false);
                break;
            case PlayerStateController.States.CarryingIdle: 
                CarryAnimation(true);
                MovementAnimation(false);
                break;
            case PlayerStateController.States.CarryingMove: 
                CarryAnimation(true);
                MovementAnimation(true);
                break;
        }
    }

    public void MovementAnimation(bool isMove)
    {
        animator.SetBool("isMoving", isMove);
    }

    public void CarryAnimation(bool isCarry)
    {
        animator.SetBool("isCarrying", isCarry);
    }
}
