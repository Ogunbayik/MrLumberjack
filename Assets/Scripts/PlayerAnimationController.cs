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
            case PlayerStateController.States.Chopping:
                ChoppingAnimation(true);
                break;
            case PlayerStateController.States.Mining:
                MiningAnimation(true);
                break;
        }
    }

    public void MovementAnimation(bool isMove)
    {
        animator.SetBool(Consts.PlayerAnimationParameter.MOVING, isMove);
    }

    public void CarryAnimation(bool isCarry)
    {
        animator.SetBool(Consts.PlayerAnimationParameter.CARRYING, isCarry);
    }

    public void ChoppingAnimation(bool isChopping)
    {
        animator.SetBool("isChopping", isChopping);
    }

    public void MiningAnimation(bool isMining)
    {
        animator.SetBool("isMining", isMining);
    }

    public void ResetChoppingAnimation()
    {
        ChoppingAnimation(false);
        stateController.ChangeState(PlayerStateController.States.Idle);
    }
    public void ResetMiningAnimation()
    {
        MiningAnimation(false);
        stateController.ChangeState(PlayerStateController.States.Idle);
    }

    public Animator GetAnimator()
    {
        return animator;
    }
}
